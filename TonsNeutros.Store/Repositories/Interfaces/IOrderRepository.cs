using TonsNeutros.Admin.Models;

namespace TonsNeutros.Store.Repositories.Interfaces;

public interface IOrderRepository
{
    void CreateOrder(Order order);
}
