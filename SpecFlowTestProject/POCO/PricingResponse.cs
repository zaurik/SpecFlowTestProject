using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowTestProject.POCO
{
    public class PricingResponse
    {
        [JsonProperty("isUpgraded")]
        public string IsUpgraded { get; set; }

        [JsonProperty("canUpgrade")]
        public string CanUpgrade { get; set; }

        [JsonProperty("isAvailable")]
        public string isAvailable { get; set; }
    }
}
