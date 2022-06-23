﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
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
 
            var stringObject = JsonConvert.SerializeObject(request);
            _session.SetString("customer.info", stringObject);
        }
    }
}
