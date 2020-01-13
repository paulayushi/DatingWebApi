using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DatingWebApi.Helper
{
    public static class PagingExtension
    {
        public static void AddPaging(this HttpResponse response, int currentPage, int itemsPerPage, int totalPages, int totalCount)
        {
            var pageHeader = new PagingHeader(currentPage, itemsPerPage, totalPages, totalCount);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
            response.Headers.Add("Access-Control-Expose-Headers", "Paging");            
            response.Headers.Add("Paging", JsonConvert.SerializeObject(pageHeader, camelCaseFormatter));
        }
    }
}
