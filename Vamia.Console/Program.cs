using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vamia.Domain;
using Vamia.Domain.Entities;
using Vamia.Domain.Managers;
using Vamia.Domain.Models;
using Vamia.Domain.Repositories;
using static System.Console;

namespace Vamia.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var inventoryManager = new InventoryManager();
            var cartManager = new CartManager();

            var keyPressed = default(ConsoleKeyInfo);
            do
            {
                WriteLine("1 - List Products");
                WriteLine("2 - Display Cart Items");
                WriteLine("3 - Add Item to Cart");
                WriteLine("4 - Place Order");
                WriteLine("Press Command.. Q to exit");

                keyPressed = ReadKey();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                        var products = inventoryManager.GetProducts();
                        if (products.Count <= 0)
                            WriteLine("No record available");
                        else
                        {
                            WriteLine();
                            foreach (var product in products)
                            {
                                WriteLine($"{product.Name} {product.Price}");
                            }
                        }
                        break;
                    case ConsoleKey.D2:
                        WriteLine("\nInput UserId");
                        int userId;
                        var inputFlag = int.TryParse(ReadLine(), out userId);
                        if (inputFlag == false)
                            WriteLine("Input is not an integer, Pls Provide an integer value");
                        else
                        {
                            var cart = cartManager.GetCart(userId);
                            if (cart != null)
                            {
                                WriteLine($"cart for userId: {userId}");
                                foreach (var item in cart.Items)
                                {
                                    WriteLine($"{item.Product.Name} - {item.Product.Price} - {item.Quantity}");
                                }
                            }
                            else
                                WriteLine($"No cart item for user: {userId}, Pls add item to cart");
                        }
                        break;
                    case ConsoleKey.D3:
                        WriteLine("\nInput the UserId");
                        inputFlag = int.TryParse(ReadLine(), out userId);
                        if (inputFlag == false)
                            WriteLine("Input is not an integer, Pls Provide an integer value");
                        else
                        {
                            WriteLine("Input ProductId");
                            int productId;
                            inputFlag = int.TryParse(ReadLine(), out productId);
                            if (inputFlag == false)
                                WriteLine("Input is not an integer, Pls Provide an integer value");
                            else
                            {
                                var cartModel = new CartModel();
                                cartModel.UserId = userId;
                                cartModel.Items.Add(new ItemModel { ProductId = productId });
                                if (cartManager.AddToCart(cartModel))
                                    WriteLine("Item added successfully");
                                else
                                    WriteLine("Failed to add item to cart");
                            }
                        }
                        break;
                }
                WriteLine(Environment.NewLine);
            } while (keyPressed.Key != ConsoleKey.Q);
        }
    }
}
