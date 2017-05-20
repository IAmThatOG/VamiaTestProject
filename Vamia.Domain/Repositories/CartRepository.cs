using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vamia.Domain.Entities;
using Vamia.Domain.Models;
using static System.Console;

namespace Vamia.Domain.Repositories
{
    class CartRepository
    {
        private DataContext _context;

        public CartRepository()
        {
            _context = new DataContext();
        }

        public CartModel GetCart(int userId)
        {
            var query = from cart in _context.CartItems
                        where cart.UserId == userId
                        group cart by cart.UserId into cartGroups
                        select new CartModel
                        {
                            UserId = cartGroups.Key,
                            Items = (from item in cartGroups
                                     select new ItemModel
                                     {
                                         Product = new ProductModel
                                         {
                                             Name = item.Product.Name,
                                             Description = item.Product.Description,
                                             Category = item.Product.Category,
                                             Price = item.Product.Amount
                                         }
                                     }).ToList()
                        };
            return query.FirstOrDefault();
        }

        public bool AddCartItem(CartModel cartModel)
        {
            foreach (var item in cartModel.Items)
            {
                var cartItem = new CartItem
                {
                    UserId = cartModel.UserId,
                    ProductId = item.ProductId
                };
                _context.CartItems.Add(cartItem);
            }
            var numOfEntries = _context.SaveChanges();
            WriteLine(numOfEntries);
            return numOfEntries > 0 ? true : false;
        }
    }
}
