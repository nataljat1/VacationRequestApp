using Microsoft.EntityFrameworkCore;
using VacationRequestApi.Data;
using VacationRequestApi.Data.Models;

namespace VacationRequestApi.Services
{
    public class VacationRequestService : IVacationRequestService
    {
        private readonly VacationRequestContext _context;

        public VacationRequestService(VacationRequestContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VacationRequest>> GetVacationRequests()
        {
            return await _context.VacationRequests.ToListAsync();
        }

        public async Task<VacationRequest> CreateVacationRequest(VacationRequest vacationRequest)
        {
            _context.VacationRequests.Add(vacationRequest);
            await _context.SaveChangesAsync();
            return vacationRequest;
        }
    }
}
