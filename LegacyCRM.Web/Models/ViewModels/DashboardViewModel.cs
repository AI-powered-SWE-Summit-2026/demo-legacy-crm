using System.Collections.Generic;
using LegacyCRM.Core.Models;

namespace LegacyCRM.Web.Models.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            RecentCustomers = new List<Customer>();
            RecentTickets = new List<Ticket>();
            PipelineSummary = new Dictionary<string, decimal>();
        }

        public string WelcomeMessage { get; set; }

        public int TotalCustomers { get; set; }

        public int OpenOpportunities { get; set; }

        public int OpenTickets { get; set; }

        public IList<Customer> RecentCustomers { get; set; }

        public IList<Ticket> RecentTickets { get; set; }

        public IDictionary<string, decimal> PipelineSummary { get; set; }
    }
}
