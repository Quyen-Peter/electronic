using ElectricalLearning.Repositories.Abstraction;
using ElectricalLearning.Services.Common;
using ElectricalLearning.Services.RequestModel;
using ElectricalLearning.Services.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalLearning.Services.IServices
{
    public interface ICircuitModelService
    {
        Task<Result<PagedResult<CircuitModelResponse.GetModel>>> GetAll(string? search, int pageIndex, int pageSize);
        Task<Result<CircuitModelResponse.GetModel>> GetById(int id);
        Task<Result> Create(CircuitModelRequest.CreateModel request);
        Task<Result> Update(int id, CircuitModelRequest.UpdateModel request);
        Task<Result> Delete(int id);
    }
}
