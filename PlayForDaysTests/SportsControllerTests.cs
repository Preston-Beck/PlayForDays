using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayForDays.Controllers;
using PlayForDays.Data;
using PlayForDays.Models;
using System;
using System.Collections.Generic;

namespace PlayForDaysTests
{
    [TestClass]
    public class SportsControllerTests
    {
        private ApplicationDbContext _context;
        private SportsController controller;

        //Sets up in memory database and new controller before every test
        [TestInitialize]
        public void TestInitialize()
        {
            //Setup in memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);

            //Create mock data for the controller
            List<Player> players = new List<Player>();

            //var SportingEvent = new List<SportingEvent>();
            var SportingEvent = new SportingEvent(
                SportingEventId = 100,
                Name = "Test Sporting Event",
                StartTime = 01 / 01 / 2021,
                EndTime = 01 / 02 / 2021,
                Address = "1 Test Dr",
                City = "Test City",
                Province = "TP",
                SportId = 2,
                Sport = "Hockey",
                Players = players));

            var Sport = new Sport();





            //Arrange: Create a controller object for all tests
            controller = new SportsController(_context);
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Act
            var result = (ViewResult)controller.Index().Result;

            //Assert
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
