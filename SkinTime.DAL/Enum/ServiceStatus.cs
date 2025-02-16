using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SkinTime.DAL.Enum
{
    public enum ServiceStatus
    {
        [EnumMember(Value = "available")]
        Available,
        [EnumMember(Value = "unavailable")]
        Unavailable,
        [EnumMember(Value = "deleted")]
        Deleted,
    }
}
