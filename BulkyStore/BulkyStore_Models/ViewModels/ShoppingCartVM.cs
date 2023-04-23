using BulkyStore_Models.Models;

namespace BulkyStore_Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        //public OrderHeader OrderHeader { get; set; }
        public double OrderTotal { get; set; }
    }
}
