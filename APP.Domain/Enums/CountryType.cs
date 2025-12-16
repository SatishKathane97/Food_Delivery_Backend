using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Domain.Enums
{
    public enum CountryType
    {
        [Description("India +91")]
        India = 1,

        [Description("United States +1")]
        UnitedStates = 2,

        [Description("United Kingdom +44")]
        UnitedKingdom = 3,

        [Description("Canada +1")]
        Canada = 4,

        [Description("Australia +61")]
        Australia = 5,

        [Description("Germany +49")]
        Germany = 6,

        [Description("United Arab Emirates +971")]
        UnitedArabEmirates = 7,

        [Description("Saudi Arabia +966")]
        SaudiArabia = 8,

        [Description("Singapore +65")]
        Singapore = 9,

        [Description("Sri Lanka +94")]
        SriLanka = 10
    }
}
