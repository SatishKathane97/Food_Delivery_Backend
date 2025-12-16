using App.Persistance.Repositories;
using APP.Domain.Entities.OTPDto;
using APP.Persistance.DbContexts;
using Lib.Core.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Service.OTPLogServiceImp
{
    public interface IOTPService
    {
        Task<OTPLog> SaveOtpAsync(string phone, string countryCode, string otp);
        Task<OTPLog?> GetLatestOtpAsync(string phone);
        Task<bool> VerifyOtpAsync(string phone);
        Task<OTPLog> ResendOtpAsync(string phone, string countryCode);
    }

    public class OTPService : IOTPService
    {
        private readonly IBaseRepository<OTPLog> _otpRepository;
        private readonly AppDbContext _appDbContext;

        public OTPService(IBaseRepository<OTPLog> otpRepository, AppDbContext appDbContext)
        {
            _otpRepository = otpRepository ?? throw new ArgumentNullException(nameof(otpRepository));
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<OTPLog> SaveOtpAsync(string phone, string countryCode, string otp)
        {
            OTPLog log = new()
            {
                Phone = phone,
                CountryCode = countryCode,
                OTPToken = otp,
                GeneratedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                IsVerified = false
            };

            await _otpRepository.Add(log);
            return log;
        }

        public async Task<OTPLog?> GetLatestOtpAsync(string phone)
        {
            return await _appDbContext.OTPLogs
                .Where(x => x.Phone == phone)
                .OrderByDescending(x => x.GeneratedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> VerifyOtpAsync(string phone)
        {
            var latestOtp = await GetLatestOtpAsync(phone);

            if (latestOtp == null)
                return false;

            if (latestOtp.IsVerified)
                return true;

            if (DateTime.UtcNow > latestOtp.ExpiresAt)
                return false;

            latestOtp.IsVerified = true;
            await _otpRepository.UpdateAsync(latestOtp);
            return true;
        }

        public async Task<OTPLog> ResendOtpAsync(string phone, string countryCode)
        {
            string newOtp = OtpHelper.GenerateNumericOtp();
            return await SaveOtpAsync(phone, countryCode, newOtp);
        }

    
}
}
