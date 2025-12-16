using Lib.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Domain.Entities.OTPDto
{
    public class OTPLog : EntityBase
    {
        public string? Phone {  get; set; }
     
        public string?OTPToken { get; set; }

        public string? CountryCode { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public bool IsVerified { get; set; } = false;

    }
}
