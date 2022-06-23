using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Application.Cart
{
    public class GetCustomerInformation
    {
        private ISession _session;
        public GetCustomerInformation(ISession session)
        {
            _session = session;
        }
        public class Request
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string PostCode { get; set; }
            public string City { get; set; }
        }
        public Request Do()
        {
           var stringObject = _session.GetString("customer.info");

           if (string.IsNullOrEmpty(stringObject))
                return null;

           var response= JsonConvert.DeserializeObject<Request>(stringObject);
           return response;

        }
    }
}
