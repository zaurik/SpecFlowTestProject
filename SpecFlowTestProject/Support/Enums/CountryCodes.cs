using System.ComponentModel;
using System.Reflection;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace SpecFlowTestProject.Support.Enums
{
    public enum CountryCodes
    {
        [Description("AU")]
        Australia,
        [Description("NZ")]
        NewZealand

    }
}
