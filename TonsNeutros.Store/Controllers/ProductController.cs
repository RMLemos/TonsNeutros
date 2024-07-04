using Microsoft.AspNetCore.Mvc;
using TonsNeutros.Store.Repositories.Interfaces;
using TonsNeutros.Store.ViewModels;

namespace TonsNeutros.Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult List()
        {
            var productsListViewModel = new ProductListViewModel();
            productsListViewModel.Products = _productRepository.Products;
            productsListViewModel.CurrentCategory = "Categoria Atual";
            return View(productsListViewModel);
        }
    }
}
