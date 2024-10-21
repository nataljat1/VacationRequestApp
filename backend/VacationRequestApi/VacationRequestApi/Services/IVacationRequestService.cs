using VacationRequestApi.Data.Models;

namespace VacationRequestApi.Services
{
    public interface IVacationRequestService
    {
        Task<IEnumerable<VacationRequest>> GetVacationRequests();
        Task<VacationRequest> CreateVacationRequest(VacationRequest vacationRequest);
    }
}
