using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowTestProject.POCO
{
    public class PricingRequest
    {
        public string SessionId { get; set; }
        public string CountryCode { get; set; }
        public string CheckoutLocationCode { get; set; }
        public string CheckoutDateTime { get; set; }
        public string CheckinLocationCode { get; set; }
        public string CheckinDateTime { get; set; }
        public string CountryOfResidence { get; set; }
        public string NumberOfAdults { get; set; }
        public string NumberOfChildren { get; set; }
        public string AgentCode { get; set; }
        public string IsVan { get; set; }
        public string IsBestBuy { get; set; }
        public string IsGross { get; set; }
        public string IsInclusiveProduct { get; set; }
        public string PromoCode { get; set; }
    }
}
