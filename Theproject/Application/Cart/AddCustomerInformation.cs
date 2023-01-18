using Domain.Models;
using Domain.Infrastructure;

namespace Application.Cart
{
    [Service]
    public class AddCustomerInformation
    {
        private ISessionManager _sessionManager;
        public AddCustomerInformation(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
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
