using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prog_P2_claims.Models
{
    public class Claims
    {//employyee - submit 
     //id fn ln data ranges - start and end 
     //hrs worked & rate per hour -- total amt 
     //desc

        public int Id { get; set; }

        [Required]
        public string LecturerID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime ClaimsPeriodsStart { get; set; }
        [Required]
        public DateTime ClaimsPeriodsEnd { get; set; }
        [Required]
        public double HoursWorked { get; set; }
        [Required]
        public double RateHour { get; set; }
        [Required]
        public double TotalHours { get; set; }

        public string DescriptionOfWork { get; set; }

        // Mark this property as NotMapped so EF doesn't try to map it to the database
        //remove iform file 
        [NotMapped]
        public List<IFormFile> SupportingDocuments { get; set; }

        public string Status { get; set; } = "Pending";// prop tab tab --> add = pending 

        //  public string? SupportingDocuments { get; set; }
        // public string? DocumentFileName get set 

        //create a view model potentially 
    }

}