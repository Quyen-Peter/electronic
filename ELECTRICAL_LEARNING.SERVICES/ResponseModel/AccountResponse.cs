using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalLearning.Services.ResponseModel
{
    public static class AccountResponse
    {
        //DTO: data transfer object: che phu đi những thứ k cần thiết, chỉ lấy thứ cần thiết
        public record GetAccountsModel(
        
            int Id,
            string FullName,
            string Email,
            string PasswordHash,
            string Role,
            DateTimeOffset CreatedAt,
            DateTimeOffset UpdatedAt
        );

        public class Login
        {
            public string Token { get; set; } = default!;
            public DateTime Expiration { get; set; }
            public string Role { get; set; } = default!;
            public string FullName { get; set; } = default!;
        }

    }
}
