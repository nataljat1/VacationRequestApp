using System.ComponentModel.DataAnnotations;

namespace VacationRequestApi.Data.Models
{
    public class VacationRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [Range(1, 365, ErrorMessage = "Vacation days must be between 1 and 365.")]
        public int VacationDays { get; set; }

        [MaxLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Comment { get; set; }
    }
}

