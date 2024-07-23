using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using TonsNeutros.Admin.Models;
using TonsNeutros.Store.Repositories;
using TonsNeutros.Store.Repositories.Interfaces;
using TonsNeutros.Store.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TonsNeutros.Store.Controllers;

public class BuyerController : Controller
{
    private readonly IBuyerRepository _buyerRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly UserManager<Buyer> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BuyerController(IBuyerRepository buyerRepository, IAddressRepository addressRepository, UserManager<Buyer> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _buyerRepository = buyerRepository;
        _addressRepository = addressRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult Edit()
    {
        var user = _httpContextAccessor.HttpContext.User;
        var buyerId = _userManager.GetUserId(user);
        
        var buyer = _buyerRepository.Buyers.FirstOrDefault(b => b.Id.Equals(buyerId));
        
        var address = _addressRepository.Addresses.FirstOrDefault(u => u.AddressId == buyer.AddressId);   

        if (buyer == null)
        {
            return RedirectToAction(nameof(Error), new { message = "User not found" });
        }

        var viewModel = new BuyerFormViewModel
        {
            Buyer = buyer,
            Address = address,

        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(BuyerFormViewModel viewModel)
    {
        var user = _httpContextAccessor.HttpContext.User;
        var buyerId = _userManager.GetUserId(user);

        var buyer = _buyerRepository.Buyers
            .FirstOrDefault(b => b.Id.Equals(buyerId));
        var address = _addressRepository.Addresses.FirstOrDefault(u => u.AddressId == buyer.AddressId);

        if (buyer == null)
        {
            return RedirectToAction(nameof(Error), new { message = "User not found" });
        }

        if (ModelState.IsValid)
        {
            _buyerRepository.Update(viewModel);
            return RedirectToAction("Index", "Home");

        }
        viewModel.Buyer = buyer;
        viewModel.Address = address;
        return View(viewModel);
    }
}