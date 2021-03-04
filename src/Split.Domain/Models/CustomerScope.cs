using System.Collections.Generic;

namespace Split.Model
{
    public class CustomerScope
    {
        public CustomerScope(string customerId, IEnumerable<Scope> scopes)
        {
            CustomerId = customerId;
            Scopes = scopes;
        }

        public CustomerScope()
        {
        }

        public string CustomerId { get; set; }
        public IEnumerable<Scope> Scopes { get; set; }
    }
}