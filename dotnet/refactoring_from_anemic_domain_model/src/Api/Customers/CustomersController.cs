using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Logic.Dtos;
using Logic.Entities;
using Logic.Repositories;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : BaseController
    {
        private readonly MovieRepository _movieRepository;
        private readonly CustomerRepository _customerRepository;

        public CustomersController(
            UnitOfWork unitOfWork,
            MovieRepository movieRepository,
            CustomerRepository customerRepository)
            : base(unitOfWork)
        {
            _customerRepository = customerRepository;
            _movieRepository = movieRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            Customer customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            var dto = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name.Value,
                Email = customer.Email.Value,
                MoneySpent = customer.MoneySpent,
                Status = customer.Status.Type.ToString(),
                StatusExpirationDate = customer.Status.ExpirationDate,
                PurchasedMovies = customer.PurchasedMovies.Select(x =>
                    new PurchasedMovieDto
                    {
                        Price = x.Price,
                        ExpirationDate = x.ExpirationDate,
                        PurchaseDate = x.PurchaseDate,
                        Movie = new MovieDto
                        {
                            Id = x.Movie.Id,
                            Name = x.Movie.Name
                        }
                    }).ToList()
            };

            return Ok(dto);
        }

        [HttpGet]
        public IActionResult GetList()
        {
            IReadOnlyList<Customer> customers = _customerRepository.GetList();

            var dtos = customers.Select(x => new CustomerInListDto
            {
                Id = x.Id,
                Name = x.Name.Value,
                Email = x.Email.Value,
                MoneySpent = x.MoneySpent,
                Status = x.Status.Type.ToString(),
                StatusExpirationDate = x.Status.ExpirationDate
            }).ToList();

            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerDto item)
        {
            var customerNameOrError = CustomerName.Create(item.Name);
            var emailOrError = Email.Create(item.Email);

            var result = Result.Combine(customerNameOrError, emailOrError);

            if (result.IsFailure)
            {
                return Error(result.Error);
            }

            if (_customerRepository.GetByEmail(emailOrError.Value) != null)
            {
                return Error("Email is already in use: " + item.Email);
            }

            var customer = new Customer(customerNameOrError.Value, emailOrError.Value);
            _customerRepository.Add(customer);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(long id, [FromBody] UpdateCustomerDto item)
        {
            var customerNameOrError = CustomerName.Create(item.Name);

            if (customerNameOrError.IsFailure)
            {
                return Error(customerNameOrError.Error);
            }

            Customer customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return Error("Invalid customer id: " + id);
            }

            customer.Name = customerNameOrError.Value;

            return Ok();
        }

        [HttpPost]
        [Route("{id}/movies")]
        public IActionResult PurchaseMovie(long id, [FromBody] long movieId)
        {
            Movie movie = _movieRepository.GetById(movieId);
            if (movie == null)
            {
                return Error("Invalid movie id: " + movieId);
            }

            Customer customer = _customerRepository.GetById(id);
            if (customer == null)
                return Error("Invalid customer id: " + id);

            if (customer.HasPurchasedMovie(movie))
                return Error("The movie is already purchased: " + movie.Name);

            customer.PurchasedMovie(movie);

            return Ok();
        }

        [HttpPost]
        [Route("{id}/promotion")]
        public IActionResult PromoteCustomer(long id)
        {
            Customer customer = _customerRepository.GetById(id);
            if (customer == null)
                return Error("Invalid customer id: " + id);

            var promotionCheck = customer.CanPromote();
            if (promotionCheck.IsFailure)
                return Error(promotionCheck.Error);

            customer.Promote();

            return Ok();
        }
    }
}