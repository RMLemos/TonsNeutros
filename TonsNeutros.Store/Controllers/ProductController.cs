using Microsoft.AspNetCore.Mvc;
using TonsNeutros.Admin.Migrations;
using TonsNeutros.Admin.Models;
using TonsNeutros.Store.Repositories.Interfaces;
using TonsNeutros.Store.ViewModels;

namespace TonsNeutros.Store.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IActionResult List(string category)
    {
        IEnumerable<Product> products;
        string currentCategory = string.Empty;

        if (string.IsNullOrEmpty(category))
        {
            products = _productRepository.Products.OrderBy(p => p.ProductId);
            currentCategory = "Products";
        }
        else
        {
            products = _productRepository.Products
                .Where(c => c.Category.CategoryName.Equals(category))
                .OrderBy(c => c.ProductName);
            currentCategory = category;
        }
        var productsListViewModel = new ProductListViewModel()
        {
            Products = products,
            CurrentCategory = currentCategory,
        };
        return View(productsListViewModel);
    }

    public IActionResult Details(int productid)
    {
        var product = _productRepository.Products.FirstOrDefault(p => p.ProductId == productid);
        return View(product);
    }

    public ViewResult Search(string searchString)
    {
        IEnumerable<Product> products;
        string currentCategory = string.Empty;

        if (string.IsNullOrEmpty(searchString))
        {
            products = _productRepository.Products.OrderBy(p =>p.ProductId);
            currentCategory = "Products";
        }
        else
        {
            products = _productRepository.Products.Where(p=>p.ProductName.ToLower().Contains(searchString.ToLower()));

            if(products.Any())
                currentCategory = "Products";
            else
                currentCategory = "Nenhum produto foi encontrado";
        }
        return View("~/Views/Product/List.cshtml", new ProductListViewModel
        {
            Products = products,
            CurrentCategory = currentCategory,

        });
    }
}
