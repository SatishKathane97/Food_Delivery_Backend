using System;
using System.Security.Cryptography;

namespace Lib.Core.Helper
{
    public static class OtpHelper
    {
        /// <summary>
        /// Generates a numeric OTP of given length (default: 6 digits).
        /// </summary>
        /// <param name="length">Number of digits in the OTP.</param>
        /// <returns>OTP as string.</returns>
        public static string GenerateNumericOtp(int length = 6)
        {
            if (length <= 0)
                throw new ArgumentException("OTP length must be greater than zero.", nameof(length));

            // Use cryptographically secure random number generator
            using (var rng = RandomNumberGenerator.Create())
            {
                // Max value is 10^length - 1 (for 6 digits: 999999)
                var maxValue = (int)Math.Pow(10, length) - 1;

                // Create a 4-byte buffer
                var randomNumber = new byte[4];
                rng.GetBytes(randomNumber);

                // Convert to positive int
                int value = Math.Abs(BitConverter.ToInt32(randomNumber, 0));

                // Fit into our range
                int otpNumber = value % (maxValue + 1);

                // Pad with leading zeros (e.g., 123 → "000123" for length 6)
                return otpNumber.ToString(new string('0', length));
            }
        }
    }
}
