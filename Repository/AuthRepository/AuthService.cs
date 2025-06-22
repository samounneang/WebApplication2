using WebApplication2.Data;
using WebApplication2.DTO;
using WebApplication2.Enum;
using WebApplication2.Model;
using WebApplication2.Repository.EmailRepository;
using WebApplication2.Repository.SmsRepository;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebApplication2.Repository.AuthRepository
{
    public class AuthService(UserDbContext context, IEmailService emailService, ISmsService smsService) : IAuthService
    {
        private readonly UserDbContext _context = context;
        private readonly IEmailService _emailService = emailService;
        private readonly ISmsService _smsService = smsService;

        public async Task RegisterEmailAsync(RegisterWithEmailDto dto)
        {
            var existingUser = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (existingUser)
                throw new Exception("User already registered");


            var failed = await _context.FailedUsers.FirstOrDefaultAsync(f => f.Email == dto.Email);
            if (failed == null)
            {
                _context.FailedUsers.Add(new FailedUser
                {
                    Email = dto.Email,
                    IsOtpVerified = false,

                });
            }

            await _context.SaveChangesAsync();

        }



        public async Task SendOtpAsync(string email)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            _context.Otpcodes.Add(new Otpcode
            {
                Email = email,
                Code = otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(8),
                IsUsed = false,
                IsVerified = false,

            });

            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(new EmailDto
            {
                ToEmail = email,
                Subject = "Your OTP Code",
                Body = $"<p>Your OTP is: <strong>{otp}</strong>.</p>"
            });
        }



        public async Task VerifyOtpAsync(VerifyDto verifyDto)
        {
            var otpRecord = await _context.Otpcodes
         .FirstOrDefaultAsync(x => x.Email == verifyDto.Email && x.Code == verifyDto.OtpCode && !x.IsUsed);

            if (otpRecord == null || otpRecord.ExpiryTime < DateTime.UtcNow)
                throw new Exception("Invalid or expired OTP");

            otpRecord.IsUsed = true;
            otpRecord.IsVerified = true;

            var failed = await _context.FailedUsers.FirstOrDefaultAsync(f => f.Email == verifyDto.Email);
            if (failed == null)
            {
                _context.FailedUsers.Add(new FailedUser
                {
                    Email = verifyDto.Email,
                    IsOtpVerified = true,
                    OtpVerifiedAt = DateTime.UtcNow
                });
            }
            else
            {
                failed.IsOtpVerified = true;
                failed.OtpVerifiedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }



        public async Task CompleteRegister(CompleteRegisterDto registerDto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Email == registerDto.Email);
            if (userExists)
                throw new Exception("User already registered");

            var verified = await _context.FailedUsers
                .FirstOrDefaultAsync(f => f.Email == registerDto.Email && f.IsOtpVerified);

            if (verified == null)
                throw new Exception("OTP not verified for this email");

            var newUser = new User
            {
                Email = registerDto.Email,
                Firstname = registerDto.Firstname,
                Lastname = registerDto.Lastname,
                Phonenumber = registerDto.Phonenumber,
                Gender = registerDto.Gender,
                DateofBirth = registerDto.DateofBirth,
                LoginProvider = LoginProvider.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            _context.FailedUsers.Remove(verified);

            await _context.SaveChangesAsync();
        }

        public async Task RegisterWithPhonenumberAsync(string phoneNumber)
        {
            // 1. Check if phone number already exists
            var exists = await _context.Users.AnyAsync(u => u.Phonenumber == phoneNumber);
            if (exists)
                throw new Exception("Phone number already exists");

            var failed = await _context.FailedUsers.FirstOrDefaultAsync(f => f.Phonenumber == phoneNumber);
            if (failed == null)
            {
                _context.FailedUsers.Add(new FailedUser
                {
                    Phonenumber = phoneNumber,
                    IsOtpVerified = false,

                });
            }

            await _context.SaveChangesAsync();

            
            
        }

        public async Task SendOtpPhoneAsync(string phonenumber)
        {
            var otp = new Random().Next(100000, 999999).ToString();

            _context.Otpcodes.Add(new Otpcode
            {
                Phonenumber = phonenumber,
                Code = otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(8),
                IsUsed = false,
                IsVerified = false,
                Email = "",

            });

            await _context.SaveChangesAsync();

            await _smsService.SendSmsAsync(new SmsDto
            {
                to = phonenumber,
                content = $"Your OTP is: {otp}"
            });
        }

        public async Task VerifyPhoneOtpAsync(VerifyPhoneDto verifyPhoneDto)
        {
            var otpRecord = await _context.Otpcodes
        .FirstOrDefaultAsync(x => x.Phonenumber == verifyPhoneDto.Phonenumber && x.Code == verifyPhoneDto.Otpcode && !x.IsUsed);

            if (otpRecord == null || otpRecord.ExpiryTime < DateTime.UtcNow)
                throw new Exception("Invalid or expired OTP");

            otpRecord.IsUsed = true;
            otpRecord.IsVerified = true;

            var failed = await _context.FailedUsers.FirstOrDefaultAsync(f => f.Phonenumber == verifyPhoneDto.Phonenumber);
            if (failed == null)
            {
                _context.FailedUsers.Add(new FailedUser
                {
                    Phonenumber = verifyPhoneDto.Phonenumber,
                    IsOtpVerified = true,
                    OtpVerifiedAt = DateTime.UtcNow
                });
            }
            else
            {
                failed.IsOtpVerified = true;
                failed.OtpVerifiedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        public async Task CompleteRegisterPhoneAsync(CompleteRegisterDto completeRegisterDto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Phonenumber == completeRegisterDto.Phonenumber);
            if (userExists)
                throw new Exception("User already registered");

            var verified = await _context.FailedUsers
                .FirstOrDefaultAsync(f => f.Phonenumber ==completeRegisterDto.Phonenumber && f.IsOtpVerified);

            if (verified == null)
                throw new Exception("OTP not verified for this email");

            var newUser = new User
            {
                Email =completeRegisterDto.Email,
                Firstname =completeRegisterDto.Firstname,
                Lastname = completeRegisterDto.Lastname,
                Phonenumber = completeRegisterDto.Phonenumber,
                Gender = completeRegisterDto.Gender,
                DateofBirth = completeRegisterDto.DateofBirth,
                LoginProvider = LoginProvider.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(completeRegisterDto.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            _context.FailedUsers.Remove(verified);

            await _context.SaveChangesAsync();
        }

        public async Task ForgotPasswordAsync(ForgetPasswordDto forgetPasswordDto)
        {
            var isEmail = forgetPasswordDto.Identifier.Contains("@");
            var user = isEmail
                ? await _context.Users.FirstOrDefaultAsync(u => u.Email == forgetPasswordDto.Identifier)
                : await _context.Users.FirstOrDefaultAsync(u => u.Phonenumber == forgetPasswordDto.Identifier);

            if (user == null)
                throw new Exception("User not found");

            var otp = new Random().Next(100000, 999999).ToString();

            _context.Otpcodes.Add(new Otpcode
            {
                Email = isEmail ? forgetPasswordDto.Identifier : "",
                Phonenumber = !isEmail ? forgetPasswordDto.Identifier : "",
                Code = otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false,
                IsVerified = false
            });

            await _context.SaveChangesAsync();

            if (isEmail)
            {
                await _emailService.SendEmailAsync(new EmailDto
                {
                    ToEmail = forgetPasswordDto.Identifier,
                    Subject = "Password Reset OTP",
                    Body = $"<p>Your OTP for password reset is: <strong>{otp}</strong></p>"
                });
            }
            else
            {
                await _smsService.SendSmsAsync(new SmsDto
                {
                    to = forgetPasswordDto.Identifier,
                    content = $"Your OTP for password reset is: {otp}"
                });
            }
        }

        public async Task ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
                throw new Exception("Passwords do not match");

            var isEmail = resetPasswordDto.Identifier.Contains("@");

            var otpRecord = await _context.Otpcodes.FirstOrDefaultAsync(o =>
                (isEmail ? o.Email == resetPasswordDto.Identifier : o.Phonenumber == resetPasswordDto.Identifier)
                && o.Code == resetPasswordDto.OtpCode
                && !o.IsUsed);

            if (otpRecord == null || otpRecord.ExpiryTime < DateTime.UtcNow)
                throw new Exception("Invalid or expired OTP");

            otpRecord.IsUsed = true;
            otpRecord.IsVerified = true;

            var user = isEmail
                ? await _context.Users.FirstOrDefaultAsync(u => u.Email == resetPasswordDto.Identifier)
                : await _context.Users.FirstOrDefaultAsync(u => u.Phonenumber == resetPasswordDto.Identifier);

            if (user == null)
                throw new Exception("User not found");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);

            await _context.SaveChangesAsync();
        }
    }
}


