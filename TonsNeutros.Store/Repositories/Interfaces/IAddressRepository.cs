using TonsNeutros.Admin.Models;

namespace TonsNeutros.Store.Repositories.Interfaces;

public interface IAddressRepository
{
    IEnumerable<Address> Addresses { get; }
}
