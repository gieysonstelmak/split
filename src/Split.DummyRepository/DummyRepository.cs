using Split.Domain.Interfaces;
using Split.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Split.DummyRepository
{
    public class DummyRepository : ICustomerScopeRepository
    {
        public Task<CustomerScope> GetCustomerScopeAsync(string customerId) =>
            Task.FromResult(new CustomerScope(customerId, new List<Scope>
            {
                new Scope("SPLIT", isActive: true)
            }));
    }
}