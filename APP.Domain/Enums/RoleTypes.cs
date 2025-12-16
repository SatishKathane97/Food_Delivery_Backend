using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Domain.Enums
{
    public enum RoleTypes
    {
        [Description("admin")]
        admin = 1,

        [Description("user")]
        user = 2,
        [Description("deliveryBoy")]
        delivery = 3,
    }
}
