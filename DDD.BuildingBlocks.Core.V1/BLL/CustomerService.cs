using DDD.BuildingBlocks.Core.V1.DAL;
using DDD.BuildingBlocks.Core.V1.Models;

namespace DDD.BuildingBlocks.Core.V1.BLL;

public class CustomerService
{
    public IReadOnlyCollection<Customer> GetAllCustomers() => NaiveRepository.GetAllCustomers();
    public Customer GetCustomerById(Guid customerId) => NaiveRepository.GetCustomerById(customerId);
}