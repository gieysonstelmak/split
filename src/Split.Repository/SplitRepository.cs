using Microsoft.Extensions.Logging;
using Split.Domain.Interfaces;
using Split.Model;
using Splitio.Services.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Split.Repository
{
    public class SplitRepository : ICustomerScopeRepository
    {
        private readonly ILogger<SplitRepository> _logger;
        private readonly ISplitFactory _splitFactory;

        public SplitRepository(ISplitFactory splitFactory, ILogger<SplitRepository> logger)
        {
            _splitFactory = splitFactory;
            _logger = logger;
        }

        public async Task<CustomerScope> GetCustomerScopeAsync(string customerId)
        {
            var scopes = new List<Scope>();
            var customer = new CustomerScope
            {
                CustomerId = customerId,
                Scopes = scopes
            };

            //Fake call to feature management service
            var isEnabled = await Task.Run(async () =>
            {
                await Task.Delay(millisecondsDelay: 10);
                return true;
            });

            _logger.LogInformation("Splitio integration enabled:{Enabled}", isEnabled);

            _logger.LogInformation("Retrieving customer scopes from Splitio for {Customer}", customerId);

            var splitManager = _splitFactory.Manager();
            var sdk = _splitFactory.Client();

            try
            {
                sdk.BlockUntilReady(blockMilisecondsUntilReady: 10000);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting Split sdk");
                return customer;
            }

            var splits = splitManager.SplitNames();
            var treatments = sdk.GetTreatmentsWithConfig(customerId, splits);

            foreach (var item in treatments) scopes.Add(new Scope {Code = item.Key, IsActive = item.Value.Treatment == "on"});

            return customer;
        }
    }
}