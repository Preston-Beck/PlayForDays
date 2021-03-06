using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayForDays.Controllers;
using PlayForDays.Data;
using PlayForDays.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayForDaysTests
{
    [TestClass]
    public class SportsControllerTests
    {
        private ApplicationDbContext _context;
        private SportsController controller;
        private List<Sport> sports = new List<Sport>();
        private List<Player> players = new List<Player>();
        private List<SportingEvent> sportingEvents = new List<SportingEvent>();


    //Sets up in memory database and new controller before every test
    [TestInitialize]
        public void TestInitialize()
        {
            //Setup in memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);

            //Create mock data for the controller
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
                SportId = 5,
                Sport = sport1,
                Players = players
            };

            sportingEvents.Add(sportingEvent);

            sports.Add(new Sport
            {
                SportId = 10,
                SportName = "Hockey",
                NumOfPlayers = 12,
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

        #region Index
        [TestMethod]
        public void IndexViewLoadsCorrectView()
        {
            //Act
            var result = (ViewResult)controller.Index().Result;

            //Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexLoadsSports()
        {
            //Act
            var result = (ViewResult)controller.Index().Result;
            List<Sport> model = (List<Sport>)result.Model;

            //Assert
            CollectionAssert.AreEqual(sports, model);
        }
        #endregion

        #region Details
        [TestMethod]
        public void DetailsWithNoIdLoads404()
        {
            //Act
            var result = (ViewResult)controller.Details(null).Result;

            //Assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsWithInvalidIdLoads404()
        {
            //Act
            var result = (ViewResult)controller.Details(200).Result;

            //Assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsWithValidIdLoadsDetails()
        {
            //Act
            var result = (ViewResult)controller.Details(2).Result;

            //Assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void DetailsWithValidIdLoadsSport()
        {
            //Act
            var result = (ViewResult)controller.Details(2).Result;
            Sport sport = (Sport)result.Model;

            //Assert
            Assert.AreEqual(sports[1], sport);
        }
        #endregion

        #region Create
        [TestMethod]
        public void CreateViewLoadsCorrectView()
        {
            //Act
            var result = (ViewResult)controller.Create();

            //Assert
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void CreatePostWithInvalidModelDataLoadsCreate()
        {
            controller.ModelState.AddModelError("Invalid Sport", "Name field is empty");

            //Act
            var result = (ViewResult)controller.Create(sports[0]).Result;

            //Assert
            Assert.AreEqual("Create", result.ViewName);
        }

        //Couldn't figure out syntax for ThrowsException
        //[TestMethod]
        //public void CreatePostWithDuplicateModelDataLoadsCreate()
        //{
        //    //Act
        //    var result = (ViewResult)controller.Create(sports[0]).Result;

        //    //Assert
        //    Assert.ThrowsException<AggregateException>(() => result.Model);
        //}

        [TestMethod]
        public void CreatePostValidDataSavesToDatabase()
        {
            //Arrange: Set up a new sport to be "created"
            Sport sport = new Sport {
                SportId = 16,
                SportName = "Soccer",
                NumOfPlayers = 22,
                NumOfTeams = 2,
                Equipment = "test equipment",
                SportingEvents = sportingEvents
            };

            //Act: Add the sport to the database and save
            _context.Sports.Add(sport);
            _context.SaveChanges();

            //Assert
            Assert.AreEqual(sport, _context.Sports.ToArray()[3]);
        }
        #endregion

        #region Edit
        [TestMethod]
        public void EditWithNoIdLoads404()
        {
            //Act
            var result = (ViewResult)controller.Edit(null).Result;

            //Assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditWithValidIdLoadsEdit()
        {
            //Act
            var result = (ViewResult)controller.Edit(2).Result;

            //Assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditWithInvalidIdLoads404()
        {
            //Act
            var result = (ViewResult)controller.Edit(1000).Result;

            //Assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditWithValidIdLoadsModel()
        {
            //Act
            var result = (ViewResult)controller.Edit(2).Result;
            Sport model = (Sport)result.Model;

            //Assert
            Assert.AreEqual(_context.Sports.Find(2), model);
        }
        #endregion

        #region Delete

        [TestMethod]
        public void DeleteWithNoIdLoads404()
        {
            //Act
            var result = (ViewResult)controller.Delete(null).Result;

            //Assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteWithInvalidIdLoads404()
        {
            //Act
            var result = (ViewResult)controller.Delete(600).Result;

            //Assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteWithValidIdLoadsDelete()
        {
            //Act
            var result = (ViewResult)controller.Delete(10).Result;

            //Assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteSportCorrectly()
        {
            //Act
            var result = (ViewResult)controller.Delete(2).Result;
            Sport sport = (Sport)result.Model;

            //Asserts
            Assert.AreEqual(sports[1], sport);
        }
        #endregion
    }
}
