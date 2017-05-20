using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vamia.Domain.Models;
using Vamia.Domain.Repositories;

namespace Vamia.Domain.Managers
{
     public class CartManager
    {
        private CartRepository _cartRepository;

        public CartManager()
        {
            _cartRepository = new CartRepository();
        }

        public CartModel GetCart(int userId)
        {
            return _cartRepository.GetCart(userId);
        }

        public bool AddToCart(CartModel cartModel)
        {
            return _cartRepository.AddCartItem(cartModel);
        }
    }
}
