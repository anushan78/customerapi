using CustomerAPI.Controllers;
using CustomerAPI.DbModels;
using CustomerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CustomerAPI.Test
{
    public class CustomerControllerTests
    {
        CustomersController _controller;
        ICustomerService _service;

        public CustomerControllerTests()
        {
            _service = new CustomerServiceFake();
            _controller = new CustomersController(_service);
        }

        [Fact]
        public void GetById_WhenExistingIdPassed_ReturnsOkResult()
        {
            var id = 1;
            var okResult = _controller.GetbyId(id);

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            var okResult = _controller.GetAll().Result as OkObjectResult;
            var items = Assert.IsType<List<Customer>>(okResult.Value);

            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            int unKnownId = 25;
            var notFoundResult = _controller.GetbyId(unKnownId);

            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }


        [Fact]
        public void GetById_ExistinIdPassed_ReturnsRightItem()
        {
            var existingId = 2;
            var okResult = _controller.GetbyId(existingId).Result as OkObjectResult;

            Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(existingId, (okResult.Value as Customer).Id);
        }

        [Fact]
        public void Create_InvaliCustomerPassed_ReturnsBadRequest()
        {
            var dateOfBirthMissingCustomer = new Customer() 
            {
                FirstName = "Beth",
                LastName = "Miller"
            };
            _controller.ModelState.AddModelError("DateOfBirth", "Required");

            var badResponse = _controller.Create(dateOfBirthMissingCustomer);

            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }


        [Fact]
        public void Create_ValidCustomerPassed_ReturnsCreatedResponse()
        {
            Customer customer = new Customer()
            {
               FirstName = "Adam",
                LastName = "Peter",
                DateOfBirth = DateTime.Parse("03/04/1998")
            };

            var createdResponse = _controller.Create(customer);

            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);
        }


        [Fact]
        public void Create_ValidCustomerPassed_ReturnedResponseHasCreatedItem()
        {
            var testCustomer = new Customer()
            {
                FirstName = "Melissa",
                LastName = "Quinn",
                DateOfBirth = DateTime.Parse("03/04/1988")
            };

            var createdResponse = _controller.Create(testCustomer);

            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);
            Assert.NotNull(_controller.GetByName("Melissa"));
        }

        [Fact]
        public void GetByName_PartialNamePassed_ReturnsRightItem()
        {
            var partialName = "ndy";
            var lastName = "Ten";
            var okResult = _controller.GetByName(partialName).Result as OkObjectResult;

            Assert.IsAssignableFrom<IEnumerable<Customer>>(okResult.Value);
            Assert.True(((List<Customer>)okResult.Value).Exists(cust => cust.LastName == lastName));
        }

        [Fact]
        public void GetByName_UnknownPartialNamePassed_ReturnsNotFoundResult()
        {
            var unKnownName = "xyz";
            var notFoundResult = _controller.GetByName(unKnownName);

            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetByName_InvalidNameParameterPassed_ReturnsBadRequestResult()
        {
            var invalidName = "";
            var notFoundResult = _controller.GetByName(invalidName);

            Assert.IsType<BadRequestResult>(notFoundResult.Result);
        }

        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsBadRequestResult()
        {
            var notExistingId = 45;
            var badResponse = _controller.Delete(notExistingId);

            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingIdPassed_ReturnsNoContentResult()
        {
            var existingId = 1;
            var okResponse = _controller.Delete(existingId);

            // Assert
            Assert.IsType<NoContentResult>(okResponse);
        }

        [Fact]
        public void Remove_ExistingIdPassed_RemovesOneItem()
        {
            var existingId = 2;
            var okResponse = _controller.Delete(existingId);

            Assert.Equal(2, _service.GetAll().Count());
        }

        [Fact]
        public void Update_ValidCustomerPassed_ReturnedResponseHasCreatedItem()
        {
            var id = 1;
            var testCustomer = new Customer()
            {
                Id = 1,
                FirstName = "Andy",
                LastName = "Ten",
                DateOfBirth = DateTime.Parse("03/04/1980")
            };

            var updatedResponse = _controller.Update(id, testCustomer);
            Assert.IsType<NoContentResult>(updatedResponse);

            var updatedResult = _controller.GetByName("Andy").Result as OkObjectResult;
            Assert.True(((List<Customer>)updatedResult.Value).Exists(cust => cust.DateOfBirth == DateTime.Parse("03/04/1980") && cust.LastName == "Ten"));
        }
    }
}