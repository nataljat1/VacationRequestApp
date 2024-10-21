using System.ComponentModel.DataAnnotations;

namespace VacationRequestApi.VacationRequestsDto
{
    /// <summary>
    /// DTO to view a vacation request.
    /// </summary>
    public class VacationRequestDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Start date of the vacation.
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// End date of the vacation.
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Number of vacation days.
        /// </summary>
        public int VacationDays { get; set; }
        /// <summary>
        /// Additional comments for the vacation request.
        /// </summary>
        public string Comment { get; set; }
    }


    /// <summary>
    /// DTO for creating a vacation request.
    /// </summary>
    public class CreateVacationRequestDto
    {
        /// <summary>
        /// Start date of the vacation.
        /// </summary>
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date of the vacation.
        /// </summary>
        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Number of vacation days.
        /// </summary>
        [Range(1, 365, ErrorMessage = "Vacation days must be between 1 and 365.")]
        public int VacationDays { get; set; }

        /// <summary>
        /// Additional comments for the vacation request.
        /// </summary>
        [MaxLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Comment { get; set; }
    }
}
