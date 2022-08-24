﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.UsersAdmin
{
    public class CreateUser
    {
        private UserManager<IdentityUser> _userManager;

        public CreateUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public class Request
        {
            public string UserName { get; set; }
        }
        public async Task<bool> Do(Request request)
        {
            var user = new IdentityUser()
            {
                UserName = request.UserName
            };

            
            await _userManager.CreateAsync(user, "password");

            var managerClaim = new Claim("Role", "Manager");
            await _userManager.AddClaimAsync(user, managerClaim);

            return true;
        }
    }
}
