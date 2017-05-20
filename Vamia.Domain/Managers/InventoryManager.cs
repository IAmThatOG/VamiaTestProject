using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vamia.Domain.Models;
using Vamia.Domain.Repositories;

namespace Vamia.Domain.Managers
{
    public class InventoryManager
    {
        private InventoryRepository _inventoryRepository;
        
        public InventoryManager()
        {
            _inventoryRepository = new InventoryRepository();
        }

        public List<ProductModel> GetProducts()
        {
            return _inventoryRepository.GetProducts();
        }
    }
}
