using ElectricalLearning.Repositories.Abstraction;
using ElectricalLearning.Services.Common;
using ElectricalLearning.Services.RequestModel;
using ElectricalLearning.Services.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ElectricalLearning.Services.RequestModel.AccountRequest;
using static ElectricalLearning.Services.ResponseModel.AccountResponse;

namespace ElectricalLearning.Services.IServices
{
    public interface IAccountService
    {
        Task<Result<PagedResult<AccountResponse.GetAccountsModel>>> GetAccounts(string? searchTerm, int pageIndex, int pageSize);
        Task<Result<AccountResponse.GetAccountsModel>> GetById(int id);

        Task<Result> CreateAccount(AccountRequest.CreateAccountModel acc);
        Task<Result> UpdateAccount(int id, AccountRequest.UpdateAccountModel acc);
        Task<Result> DeleteAccount(int id);
        Task<Result<AccountResponse.Login>> Login(AccountRequest.Login request);

    }

}
