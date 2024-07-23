using TonsNeutros.Admin.Models;
using TonsNeutros.Store.ViewModels;

namespace TonsNeutros.Store.Repositories.Interfaces;

public interface IBuyerRepository
{
    IEnumerable<Buyer> Buyers { get; /*set;*/ }
    void Update(BuyerFormViewModel viewModel);
}
