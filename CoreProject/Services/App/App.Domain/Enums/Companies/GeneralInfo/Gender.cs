using System.ComponentModel;

namespace App.Domain.Enums.Companies.GeneralInfo;

public enum DefaultGender
{
    [Description("Male")]
    Male = 1,

    [Description("Female")]
    Female = 2,

    [Description("Others")]
    Others = 3,

}
