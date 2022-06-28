using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Cart
{
    public class AddCustomerInformation
    {
        private ISession _session;
        public AddCustomerInformation(ISession session)
        {
            _session = session;
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

            var customerInformation = new CustomerInformation
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.FirstName,
                PhoneNumber = request.PhoneNumber,
                PostCode = request.PostCode,
                City = request.City,
                Address1 = request.FirstName,
                Address2 = request.FirstName,

            };

            var stringObject = JsonConvert.SerializeObject(customerInformation);

            _session.SetString("customer.info", stringObject);
        }
    }
}
