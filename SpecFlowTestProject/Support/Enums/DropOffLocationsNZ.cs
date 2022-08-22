using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace SpecFlowTestProject.Support.Enums
{
    public enum DropOffLocationsNZ
    {
        [Description("AKL")]
        Auckland,
        [Description("CHC")]
        Christchurch,
        [Description("ZQN")]
        Queenstown
    }
}
