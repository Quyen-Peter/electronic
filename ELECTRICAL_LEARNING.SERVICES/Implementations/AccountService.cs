using Azure.Core;
using ElectricalLearning.Repositories.Abstraction;
using ElectricalLearning.Repositories.Entities;
using ElectricalLearning.Services.Common;
using ElectricalLearning.Services.IServices;
using ElectricalLearning.Services.RequestModel;
using ElectricalLearning.Services.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ElectricalLearning.Services.RequestModel.AccountRequest;
using static ElectricalLearning.Services.ResponseModel.AccountResponse;

namespace ElectricalLearning.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<Account, int> _accountRepository;
        private readonly IJwtService _jwtService;
        public AccountService(IBaseRepository<Account, int> accountRepository, IJwtService jwtService)
        {
            _accountRepository = accountRepository;
            _jwtService = jwtService;
        }

        public async Task<Result> CreateAccount(AccountRequest.CreateAccountModel request)
        {
            var emailExists = _accountRepository.GetAll().Any(a => a.Email == request.Email);
            if (emailExists)
                return Result.Failure("Email đã tồn tại", 400);
            var account = new Account
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
            await _accountRepository.Add(account);
            await _accountRepository.SaveChangesAsync();

            return Result.Success("Tạo tài khoản thành công");
        }

        public async Task<Result> DeleteAccount(int id)
        {
            var account = await _accountRepository.GetById(id);
            if (account == null)
                return Result.Failure("Tài khoản không tồn tại", 404);

            await _accountRepository.Remove(account);
            await _accountRepository.SaveChangesAsync();

            return Result.Success("Xoá tài khoản thành công");
        }

        public async Task<Result<PagedResult<AccountResponse.GetAccountsModel>>> GetAccounts(string? searchTerm, int pageIndex, int pageSize)
        {

            var raw = _accountRepository.GetAll();

            raw = raw.AsNoTracking();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var cleanSearchTerm = searchTerm.Trim().ToLower();
                raw = raw.Where(x => x.FullName.ToLower().Contains(cleanSearchTerm) || x.Email.ToLower().Contains(cleanSearchTerm));
            }

            var query = raw.Select(x => new AccountResponse.GetAccountsModel(
                    x.Id,
                    x.FullName,
                    x.Email,
                    x.PasswordHash,
                    x.Role.ToString(),
                    x.CreatedAt,
                    x.UpdatedAt
                ));

            var pagedResult = await PagedResult<AccountResponse.GetAccountsModel>.CreateAsync
                (query, pageIndex, pageSize)!;

            var result = Result<PagedResult<AccountResponse.GetAccountsModel>>.Success(pagedResult, "Get list account successfully !");

            return result;
        }

        public async Task<Result<AccountResponse.GetAccountsModel>> GetById(int id)
        {
            var raw = await _accountRepository.GetById(id);

            if(raw == null)
            {
                throw new Exception("User not found !");
            }

            var result = new AccountResponse.GetAccountsModel(
                raw.Id,
                raw.FullName,
                raw.Email,
                raw.PasswordHash,
                raw.Role,
                raw.CreatedAt,
                raw.UpdatedAt
            );

            return Result<AccountResponse.GetAccountsModel>.Success(result, "Get account successfully!");
        }

        public async Task<Result<AccountResponse.Login>> Login(AccountRequest.Login request)
        {
            var account = await _accountRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Email == request.Email);
            if (account == null || !BCrypt.Net.BCrypt.Verify(request.Password, account.PasswordHash))
            {
                return Result<AccountResponse.Login>.Failure("Email hoặc mật khẩu không đúng", 401);
            }
            var token = _jwtService.GenerateToken(account);
            var response = new AccountResponse.Login
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60),
                Role = account.Role,
                FullName = account.FullName
            };

            return Result<AccountResponse.Login>.Success(response, "Đăng nhập thành công");
        }

        public async Task<Result> UpdateAccount(int id, AccountRequest.UpdateAccountModel request)
        {
            var account = await _accountRepository.GetById(id);
            if (account == null)
                return Result.Failure("Tài khoản không tồn tại", 404);

            account.FullName = request.FullName ?? account.FullName;
            account.Email = request.Email ?? account.Email;
            if (!string.IsNullOrWhiteSpace(request.Password))
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            if (!string.IsNullOrWhiteSpace(request.Role))
                account.Role = request.Role;

            account.UpdatedAt = DateTimeOffset.UtcNow;

            await _accountRepository.Update(account);
            await _accountRepository.SaveChangesAsync();

            return Result.Success("Cập nhật tài khoản thành công");
        }
    }
}
