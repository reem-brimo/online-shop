﻿using Application.Infrastructure;

namespace Application.Cart
{
    public class GetCustomerInformation
    {
        private ISessionManager _sessionManager;
        public GetCustomerInformation(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
        public class Response
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
        public Response Do()
        {

            var customerInformation = _sessionManager.GetCustomerInformation();

            if (customerInformation == null)
                return null;

            return new Response
            {
                FirstName = customerInformation.FirstName,
                LastName = customerInformation.LastName,
                Email = customerInformation.FirstName,
                PhoneNumber = customerInformation.PhoneNumber,
                PostCode = customerInformation.PostCode,
                City = customerInformation.City,
                Address1 = customerInformation.FirstName,
                Address2 = customerInformation.FirstName,
            };

        }
    }
}
