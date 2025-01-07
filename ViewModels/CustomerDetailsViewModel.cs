using System.ComponentModel.DataAnnotations;

namespace Group13_GoodRoots.ViewModels
{
    public class CustomerDetailsViewModel
    {
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "First name can only contain letters, numbers, and hyphens.")]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "Last name can only contain letters, numbers, and hyphens.")]
        public string? LastName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s.,'/-]+$", ErrorMessage = "Street can only contain letters, numbers, spaces, and special characters like . , ' / -.")]
        public string? Street { get; set; }

        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "City can only contain letters and hyphens.")]
        public string? City { get; set; }

        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Province can only contain letters and hyphens.")]
        public string? Province { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]{3}-[a-zA-Z0-9]{3}$", ErrorMessage = "Please enter the postcode format: q2w-3e4")]
        public string? PostCode { get; set; }

        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Country can only contain letters and hyphens.")]
        public string? Country { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number.")]
        public string PhoneNumber { get; set; }
    }
}
