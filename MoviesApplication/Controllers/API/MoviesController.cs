using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Data.Interfaces;
using Infrastructure;
using MoviesApplication.Models;

namespace MoviesApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/movies")]
    public class MoviesController : APIControllerBase
    {
        private readonly IEntityBaseRepo<Movie> moviesRepository;

        public MoviesController(IEntityBaseRepo<Movie> moviesRepository, IEntityBaseRepo<Error> errorRepository, IUnitOfWork unitOfWork) 
            : base(unitOfWork, errorRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        [AllowAnonymous]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var movies = moviesRepository.GetAll().OrderByDescending(x => x.ReleaseDate).Take(6).ToList();
                var moviesVM = Mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(movies);

                response = request.CreateResponse(HttpStatusCode.OK, moviesVM);
                return response;
            });
        }
    }
}
