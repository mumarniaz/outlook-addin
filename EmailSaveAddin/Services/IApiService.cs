using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailSaveAddin.Services
{
    public interface IApiService
    {
        Task<string> GetSelectedOrgainization(string organization);
        Task<List<string>> GetOrganizations(string searchTxt);
    }
}
