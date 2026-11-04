using System;
using System.ComponentModel;

namespace Lib.Core.Enums
{
    public enum OperationType
    {
        [Description("Insert")]
        Insert = 1,

        [Description("Update")]
        Update,

        [Description("Delete")]
        Delete,

        [Description("View")]
        View
    }
}

