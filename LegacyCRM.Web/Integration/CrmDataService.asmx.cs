using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using LegacyCRM.Core;
using Newtonsoft.Json;

namespace LegacyCRM.Web.Integration
{
    [WebService(Namespace = "http://legacycrm.local/services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CrmDataService : WebService
    {
        [WebMethod(Description = "Gets customer summary data.")]
        public List<CustomerDto> GetCustomers()
        {
            using (var context = new CrmDbContext())
            {
                return context.Customers
                    .OrderBy(c => c.CompanyName)
                    .Take(20)
                    .Select(c => new CustomerDto
                    {
                        Id = c.Id,
                        CompanyName = c.CompanyName,
                        Email = c.Email,
                        Status = c.Status,
                        Country = c.Country
                    })
                    .ToList();
            }
        }

        [WebMethod(Description = "Gets a single customer as JSON.")]
        public string GetCustomerJson(int id)
        {
            using (var context = new CrmDbContext())
            {
                var customer = context.Customers.FirstOrDefault(c => c.Id == id);
                return JsonConvert.SerializeObject(customer, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
        }
    }

    public class CustomerDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Country { get; set; }
    }
}
