using Domain.Models;
using System.ComponentModel.DataAnnotations;
using Application.Infrastructure;

namespace Application.Cart
{
    public class AddCustomerInformation
    {
        private ISessionManager _sessionManager;
        public AddCustomerInformation(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        public class Request
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Required]
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            [Required]
            public string PostCode { get; set; }
            [Required]
            public string City { get; set; }
        }
        public void Do(Request request)
        {
            _sessionManager.AddCustomerInfo(
            new CustomerInformation
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.FirstName,
                PhoneNumber = request.PhoneNumber,
                PostCode = request.PostCode,
                City = request.City,
                Address1 = request.FirstName,
                Address2 = request.FirstName,
            });
        }
    }
}
