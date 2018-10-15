using Otc.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Opala.Web.ViewModels
{
    public class ClientViewModel
    {
        public ICollection<SubscriptionViewModel> Subscriptions { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorKey = "400.001")]
        [MinLength(3, ErrorKey = "400.002")]
        public string Name { get; set; }

        [EmailAddress(ErrorKey = "400.003")]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public AddressViewModel Address { get; set; }

        [Required(ErrorKey = "400.004")]
        public string PhoneNumber { get; set; }
    }
}
