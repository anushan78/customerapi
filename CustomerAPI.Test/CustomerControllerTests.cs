﻿using CustomerAPI.Controllers;
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
            Assert.NotNull(_controller.GetbyName("Melissa"));
        }

        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            var notExistingId = 45;
            var badResponse = _controller.Delete(notExistingId);

            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingIdPassed_ReturnsOkResult()
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
    }
}