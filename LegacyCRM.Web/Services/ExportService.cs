using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LegacyCRM.Core.Models;

namespace LegacyCRM.Web.Services
{
    public class ExportService
    {
        public byte[] ExportCustomers(IEnumerable<Customer> customers)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Id,Name,CompanyName,Email,Status,Country");

            foreach (var customer in customers)
            {
                builder.AppendLine(string.Join(",",
                    customer.Id,
                    Escape(customer.Name),
                    Escape(customer.CompanyName),
                    Escape(customer.Email),
                    Escape(customer.Status),
                    Escape(customer.Country)));
            }

            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        public string GetDocumentFolder(int customerId)
        {
            return Path.Combine("C:\\CRMData", customerId.ToString(), "documents");
        }

        private static string Escape(string value)
        {
            value = value ?? string.Empty;
            if (value.Contains(",") || value.Contains("\""))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }

            return value;
        }
    }
}
