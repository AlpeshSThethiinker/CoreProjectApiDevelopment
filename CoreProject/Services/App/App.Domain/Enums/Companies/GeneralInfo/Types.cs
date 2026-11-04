using System.ComponentModel;

namespace App.Domain.Enums.Companies.GeneralInfo
{
    public enum DefaultTypes
    {
        [Description("GOV")]
        GOV = 1,
        [Description("ODM")]
        ODM = 2,
        [Description("EMS")]
        EMS = 3,
        [Description("OEM")]
        OEM = 4,
        [Description("CEM")]
        CEM = 5,
    }
}
