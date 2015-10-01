using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data;
using Data.Interfaces;
using Infrastructure;
using Membership.Interfaces;

namespace MoviesApplication.Controllers.API
{
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/Account")]
    public class AuthenticationController : APIControllerBase
    {
        private readonly IMembershipService membershipService;

        public AuthenticationController(IMembershipService membershipService, IEntityBaseRepo<Error> errorRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork, errorRepository)
        {
            this.membershipService = membershipService;
        }


    }
}
