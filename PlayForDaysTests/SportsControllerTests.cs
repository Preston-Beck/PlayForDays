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
            List<Sport> sports = new List<Sport>();
            List<Player> players = new List<Player>();
            List<SportingEvent> sportingEvents = new List<SportingEvent>();
            var sport1 = new Sport();

            //var SportingEvent = new List<SportingEvent>();
            var sportingEvent = new SportingEvent
            {
                SportingEventId = 100,
                Name = "Test Sporting Event",
                StartTime = new DateTime(),
                EndTime = new DateTime(0001, 01, 02),
                Address = "1 Test Dr",
                City = "Test City",
                Province = "TP",
                SportId = 2,
                Sport = sport1,
                Players = players
            };

            sportingEvents.Add(sportingEvent);

            sports.Add(new Sport
            {
                SportId = 1,
                SportName = "Hockey",
                NumOfPlayers = 10,
                NumOfTeams = 2,
                Equipment = "test equipment",
                SportingEvents = sportingEvents
            });

            sports.Add(new Sport
            {
                SportId = 2,
                SportName = "Badminton",
                NumOfPlayers = 2,
                NumOfTeams = 2,
                Equipment = "test equipment",
                SportingEvents = sportingEvents
            });

            sports.Add(new Sport
            {
                SportId = 3,
                SportName = "Ultimate",
                NumOfPlayers = 14,
                NumOfTeams = 2,
                Equipment = "test equipment",
                SportingEvents = sportingEvents
            });

            foreach(var sport in sports)
            {
                _context.Sports.Add(sport);
            }
            _context.SaveChanges();

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
