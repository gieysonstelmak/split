using Split.Model;
using System.Threading.Tasks;

namespace Split.Domain.Interfaces
{
    public interface ISplitService
    {
        Task<CustomerScope> GetScopesAsync();
    }
}