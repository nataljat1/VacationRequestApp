using Microsoft.EntityFrameworkCore;
using VacationRequestApi.Data.Models;

namespace VacationRequestApi.Data
{
    public class VacationRequestContext : DbContext
    {
        public VacationRequestContext(DbContextOptions<VacationRequestContext> options) : base(options)
        {
        }

        public DbSet<VacationRequest> VacationRequests { get; set; } = null!;
    }
}
