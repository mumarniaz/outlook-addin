﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSaveAddin.Services
{
    public class DummyApiService : IApiService
    {
        public async Task<List<string>> GetOrganizations(string searchTxt)
        {
            List<string> organizations = new List<string>()
            {
                "Netsoft Holding LLC", "Microsoft", "Facebook", "NetSole Pvt Ltd", "Eurosoft Ltm",
                "Meta Pvt Ltd", "Shell Holding", "Total Petroleum", "Attock Holding LLC"
            };

            await Task.Delay(10);

            if (string.IsNullOrEmpty(searchTxt))
            {
                return new List<string>();
            }

            return organizations.Where(t => t.Contains(searchTxt)).ToList();
        }

        public async Task<string> GetSelectedOrgainization(string organization)
        {
            List<string> organizations = new List<string>()
            {
                "Netsoft Holding LLC", "Microsoft", "Facebook", "NetSole Pvt Ltd", "Eurosoft Ltm",
                "Meta Pvt Ltd", "Shell Holding", "Total Petroleum", "Attock Holding LLC"
            };

            await Task.Delay(10);

            return organizations.FirstOrDefault(t => t == organization);
        }
    }
}
