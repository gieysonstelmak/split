using Microsoft.Extensions.Logging;
using Split.Domain.Interfaces;
using Split.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Split
{
    public class SplitService : ISplitService
    {
        private readonly IEnumerable<ICustomerScopeRepository> _customerScopeRepositories;
        private readonly ILogger<SplitService> _logger;

        public SplitService(IEnumerable<ICustomerScopeRepository> customerScopeRepositories, ILogger<SplitService> logger)
        {
            _customerScopeRepositories = customerScopeRepositories;
            _logger = logger;
        }

        public async Task<CustomerScope> GetScopesAsync()
        {
            var customerId = "bd605969928d35518b8ff312a30e48af5844fe867e2a4577c3b9b9194189840d";

            var customerScopesTasks = _customerScopeRepositories.Select(x => x.GetCustomerScopeAsync(customerId)).ToArray();

            await Task.WhenAll(customerScopesTasks);

            var scopesResults = new List<Scope>();

            var customerScopeResults = customerScopesTasks.Select(x => x.Result).Where(x => x != null);

            scopesResults.AddRange(customerScopeResults.SelectMany(x => x.Scopes));

            //Defining scopes results
            var scopeList = scopesResults.GroupBy(c => c.Code).Select(c => new Scope
            {
                Code = c.Key,
                IsActive = c.Count() > 1 ? c.All(value => value.IsActive) : c.First().IsActive
            });

            var customerScope = new CustomerScope(customerId, scopeList);

            _logger.LogInformation("Found {Count} customer scopes", customerScope.Scopes.Count());

            return customerScope;
        }
    }
}