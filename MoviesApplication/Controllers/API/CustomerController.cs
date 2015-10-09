using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using AutoMapper;
using Data.Interfaces;
using Infrastructure;
using MoviesApplication.Models;

namespace MoviesApplication.Controllers.API
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/customer")]
    public class CustomerController : APIControllerBase
    {
        private readonly IEntityBaseRepo<Customer> customerRepository;

        public CustomerController(IEntityBaseRepo<Customer> customerRepository, IUnitOfWork unitOfWork, IEntityBaseRepo<Error> errorRepostiroy)
            : base(unitOfWork, errorRepostiroy)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("search/{page:int=0}/{pageSize:int=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentpageSize = pageSize.Value;

            return this.CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Customer> customerList = null;
                int totalCustNom = 0;

                if (string.IsNullOrEmpty(filter))
                {
                    customerList = customerRepository.GetAll().ToList();
                }
                else
                {
                    customerList = customerRepository.GetAll().OrderBy(customer => customer.Id)
                        .Where(customer => customer.LastName.ToLower().Contains(filter) ||
                                           customer.IdentityCard.ToLower().Contains(filter) ||
                                           customer.FirstName.ToLower().Contains(filter)).ToList();
                }

                totalCustNom = customerList.Count;
                customerList = customerList.Skip(currentPage * currentpageSize).Take(currentpageSize).ToList();

                IEnumerable<CustomerViewModel> customerVM =
                    Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(customerList);

                Pagination<CustomerViewModel> pagedVMSet = new Pagination<CustomerViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalCustNom,
                    TotalPages = (int)Math.Ceiling((decimal)totalCustNom / currentpageSize),
                    items = customerVM
                };

                response = request.CreateResponse<IEnumerable<CustomerViewModel>>(HttpStatusCode.OK, customerVM);

                return response;
            });
        }
    }
}