using BulkyStore_DataAccess.Repository.IRepository;
using BulkyStore_Models.Models;
using BulkyStore_Models.ViewModels;
using BulkyStore_Utility.GetLists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BulkyStore_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //List<Product> data = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                ProductVM data = new();
                data.CategoryList = CategoryList.GetCategoryList(_unitOfWork);
                return View(data);
            }
            else
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var product = _unitOfWork.Product.Get(c => c.Id == id, includeProperties: "Category");
                if (product == null)
                {
                    return NotFound();
                }

                ProductVM data = new()
                {
                    Product = product,
                    CategoryList = CategoryList.GetCategoryList(_unitOfWork, product.CategoryId)
                };

                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(int? id, ProductVM productVM, IFormFile? file)
        {
            productVM.CategoryList = CategoryList.GetCategoryList(_unitOfWork, categoryId: productVM.Product.CategoryId);
            if (ModelState.IsValid)
            {
                
                if (id == null || id == 0)
                {
                    var isExists = _unitOfWork.Product.Get(x => x.Title.ToLower() == productVM.Product.Title.ToLower());
                    if (isExists != null)
                    {
                        ModelState.AddModelError("Product.Title", "Title must be unique.");
                        return View(productVM);
                    }
                    productVM.Product.ImageUrl = productVM.Product.ImageUrl == null ? "" : productVM.Product.ImageUrl;
                    _unitOfWork.Product.Add(productVM.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product create successfully";
                    
                }
                else
                {
                    var isExists = _unitOfWork.Product
                            .Get(x => x.Title.ToLower() == productVM.Product.Title.ToLower() && x.Id != productVM.Product.Id);
                    if (isExists != null)
                    {
                        ModelState.AddModelError("Product.Title", "Title must be unique.");
                        return View(productVM);
                    }
                    productVM.Product.ImageUrl = _unitOfWork.Product.Get(x => x.Id == id).ImageUrl;
                    _unitOfWork.Product.Update(productVM.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product update successfully";

                    
                }

                if (file != null)
                {

                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = @"images\products\product-" + productVM.Product.Id;
                    string finalPath = Path.Combine(wwwRootPath, productPath);

                    if (!Directory.Exists(finalPath))
                        Directory.CreateDirectory(finalPath);

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\" + productPath + @"\" + fileName;

                    _unitOfWork.Product.Update(productVM.Product);
                    _unitOfWork.Save();

                }
                return RedirectToAction("Index");
            }
            return View(productVM);

        }



        public IActionResult Create()
        {
            ProductVM data = new();
            data.CategoryList = CategoryList.GetCategoryList(_unitOfWork);
            //ViewBag.Categories = categories;
            //ViewData["Categories"] = categories;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {
            productVM.CategoryList = CategoryList.GetCategoryList(_unitOfWork,categoryId:productVM.Product.CategoryId);
            if (ModelState.IsValid)
            {
                var isExists = _unitOfWork.Product.Get(x => x.Title.ToLower() == productVM.Product.Title.ToLower());
                
                if (isExists != null)
                {
                    ModelState.AddModelError("Product.Title", "Title must be unique.");                    
                    return View(productVM);
                }
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product create successfully";
                return RedirectToAction("Index");
            }
            return View(productVM);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = _unitOfWork.Product.Get(c => c.Id == id, includeProperties: "Category");
            if (product == null)
            {
                return NotFound();
            }
            
            ProductVM data = new()
            {
                Product = product,
                CategoryList = CategoryList.GetCategoryList(_unitOfWork,product.CategoryId)
            };

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM productVM)
        {
            productVM.CategoryList = CategoryList.GetCategoryList(_unitOfWork, productVM.Product.CategoryId);
            if (ModelState.IsValid)
            {
                var isExists = _unitOfWork.Product
                    .Get(x => x.Title.ToLower() == productVM.Product.Title.ToLower() && x.Id != productVM.Product.Id);
                if (isExists != null)
                {
                    ModelState.AddModelError("Product.Title", "Title must be unique.");
                    return View(productVM);
                }
                _unitOfWork.Product.Update(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product update successfully";
                return RedirectToAction("Index");
            }
            return View(productVM);
        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var product = _unitOfWork.Product.Get(c => c.Id == id, includeProperties: "Category");
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    ProductVM data = new()
        //    {
        //        Product = product,
        //        CategoryList = CategoryList.GetCategoryList(_unitOfWork, product.CategoryId)
        //    };
        //    return View(data);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id)
        //{
        //    string wwwRootPath = _webHostEnvironment.WebRootPath;
        //    var obj = _unitOfWork.Product.Get(c => c.Id == id);
        //    if (!string.IsNullOrEmpty(obj.ImageUrl))
        //    {
        //        var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
        //        if (System.IO.File.Exists(oldImagePath))
        //        {
        //            System.IO.File.Delete(oldImagePath);
        //        }
        //    }
        //    _unitOfWork.Product.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Product delete successfully";
        //    return RedirectToAction("Index");
        //}


        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }

                Directory.Delete(finalPath);
            }


            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
