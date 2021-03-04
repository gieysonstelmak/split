using Split.Model;
using System.Threading.Tasks;

namespace Split.Domain.Interfaces
{
    public interface ICustomerScopeRepository
    {
        Task<CustomerScope> GetCustomerScopeAsync(string customerId);
    }
}