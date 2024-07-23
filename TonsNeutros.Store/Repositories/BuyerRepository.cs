using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TonsNeutros.Admin.Context;
using TonsNeutros.Admin.Models;
using TonsNeutros.Store.Repositories.Interfaces;
using TonsNeutros.Store.ViewModels;

namespace TonsNeutros.Store.Repositories;

public class BuyerRepository : IBuyerRepository
{
    private readonly AppDbContext _context;
    private readonly UserManager<Buyer> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BuyerRepository(AppDbContext context, UserManager<Buyer> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public IEnumerable<Buyer> Buyers => _context.Users;

    public void Update(BuyerFormViewModel viewModel)
    {
        var user = _httpContextAccessor.HttpContext.User;
        var buyerId = _userManager.GetUserId(user);
        var buyer = Buyers
            .FirstOrDefault(b => b.Id.Equals(buyerId));

        if (buyer != null && viewModel != null)
        {
            if(viewModel.Buyer != null)
            {
                buyer.Name = viewModel.Buyer.Name;
                buyer.Nif = viewModel.Buyer.Nif;
                buyer.PhoneNumber = viewModel.Buyer.PhoneNumber;
            }

            // Atualiza as propriedades do endereço se o Address no viewModel não for nulo
            if (viewModel.Address != null)
            {
                if (buyer.Address == null)
                {
                    buyer.Address = new Address();
                }
                buyer.Address.Country = viewModel.Address.Country;
                buyer.Address.Street = viewModel.Address.Street;
                buyer.Address.Place = viewModel.Address.Place;
                buyer.Address.ZipCode = viewModel.Address.ZipCode;
                buyer.Address.City = viewModel.Address.City;
            }


            _context.Users.Update(buyer);
            _context.SaveChanges();
        }       
    }
}
