using ElectricalLearning.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalLearning.Services.IServices
{
    public interface IJwtService
    {
        string GenerateToken(Account account);
    }
}
