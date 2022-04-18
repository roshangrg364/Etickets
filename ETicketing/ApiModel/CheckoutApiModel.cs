using System.ComponentModel.DataAnnotations;

namespace ETicketing.ApiModel
{
    public class CheckoutApiModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$",
         ErrorMessage = "Invalid mobile format")]
        public string Number { get; set; }
        [Required]
        public decimal PaidAmount { get; set; }
    }

   
}
