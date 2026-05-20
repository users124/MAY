using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAY.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        [BindNever]
        [ValidateNever]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }

        public string? SessionId { get; set; }

        //public string SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        // Shipping Information

        [Required]
        public string PhoneNumber { get; set; }
        [Required]

        public string StreetAddress { get; set; }
        [Required]

        public string City { get; set; }

        public string? State { get; set; } // State is for possible future use, as we are not using it for now,so we will keep it here for now.
        [Required]

        public string PostalCode { get; set; }
        [Required]

        public string Name { get; set; }
    }
}
