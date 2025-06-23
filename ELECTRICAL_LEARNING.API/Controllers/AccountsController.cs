using ElectricalLearning.Services.Implementations;
using ElectricalLearning.Services.IServices;
using ElectricalLearning.Services.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ElectricalLearning.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAccounts(string? searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var result = await _accountService.GetAccounts(searchTerm, pageIndex, pageSize);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _accountService.GetById(id);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountRequest.CreateAccountModel request)
        {
            var result = await _accountService.CreateAccount(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AccountRequest.UpdateAccountModel request)
        {
            var result = await _accountService.UpdateAccount(id, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _accountService.DeleteAccount(id);
            return StatusCode(result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountRequest.Login request)
        {
            var result = await _accountService.Login(request);
            return StatusCode(result.StatusCode, result);
        }
    }

}
