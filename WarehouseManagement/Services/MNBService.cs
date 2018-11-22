using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WarehouseManagement.Services
{
    public class MNBService
    {
        ServiceReference.MNBArfolyamServiceSoapClient client = new ServiceReference.MNBArfolyamServiceSoapClient();
        public async Task<decimal> GetEuroRate()
        {
            var ratesResponseBody = await client.GetCurrentExchangeRatesAsync(null);
            var resultAsString = ratesResponseBody.GetCurrentExchangeRatesResponse1.GetCurrentExchangeRatesResult;
            XDocument xdoc = XDocument.Parse(resultAsString);
            var rates = xdoc.Descendants("Rate");
            var rate = rates.Where(m => m.Attribute("curr").Value == "EUR").FirstOrDefault().Value;
            decimal convertedRate = Convert.ToDecimal(rate.Replace(",", "."));
            return convertedRate;
        }
    }
}
