using TonsNeutros.Admin.Context;
using TonsNeutros.Admin.Models;
using TonsNeutros.Store.Repositories.Interfaces;

namespace TonsNeutros.Store.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Address> Addresses => _context.Addressess;
}
