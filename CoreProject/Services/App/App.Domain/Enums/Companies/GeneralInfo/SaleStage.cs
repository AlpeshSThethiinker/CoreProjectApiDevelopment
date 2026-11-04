using System.ComponentModel;

namespace App.Domain.Enums.Companies.GeneralInfo
{
    public enum DefaultStage
    {
        [Description("Lead")]
        Lead = 1,

        [Description("Prospect")]
        Prospect = 2,

        [Description("Vendor")]
        Vendor = 3,
    }
}
