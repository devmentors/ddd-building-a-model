using DDD.BuildingBlocks.Core.V2.Encapsulated.DAL;
using DDD.BuildingBlocks.Core.V2.Encapsulated.Models;

namespace DDD.BuildingBlocks.Core.V2.Encapsulated.App;

public class CustomerService
{
    public IReadOnlyCollection<Customer> GetAllCustomers() => NaiveRepository.GetAllCustomers();
    public Customer GetCustomerById(Guid customerId) => NaiveRepository.GetCustomerById(customerId);
}