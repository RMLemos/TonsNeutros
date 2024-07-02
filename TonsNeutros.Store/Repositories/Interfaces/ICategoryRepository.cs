using TonsNeutros.Admin.Models;

namespace TonsNeutros.Store.Repositories.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> Categories { get; }
}
