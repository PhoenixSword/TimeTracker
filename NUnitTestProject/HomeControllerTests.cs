using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TimeTracker.Controllers;
using TimeTracker.Models.Repositories.Abstract;
using TimeTracker.Models.Repositories.Concrete;

namespace NUnitTestProject
{
    [TestFixture]
    public class HomeControllerTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllTasksUnitTest1()
        {
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAllTasks())
                .ReturnsAsync(GetTest());
            var _controller = new HomeController(mockService.Object);

            //Act
            var result = await _controller.GetAllTasks();

            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetAllTasksUnitTest2()
        {
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAllTasks())
                .ReturnsAsync(GetTest());
            var _controller = new HomeController(mockService.Object);

            //Act
            var result = await _controller.GetAllTasks();

            Assert.IsInstanceOf<List<object>>(result);
        }
        [Test]
        public async Task GetAllTasksUnitTest3()
        {
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAllTasks())
                .ReturnsAsync(GetTest());
            var _controller = new HomeController(mockService.Object);

            //Act
            var result = await _controller.GetAllTasks();

            Assert.AreEqual(GetTest().Count, result.Count);
        }
    
        private List<object> GetTest()
        {
            var list = new List<object>
            {
                new { id = 1, title = "TaskName1", user = "qwe@qwe.qwe", description = "description1", downloadUrl = "downloadUrl1" },
                new { id = 2, title = "TaskName2", user = "qwe@qwe.com", description = "description2", downloadUrl = "downloadUrl2" },
                new { id = 3, title = "TaskName3", user = "123@qwe.qwe", description = "description3", downloadUrl = "" },
                new { id = 4, title = "TaskName4", user = "qwe@123.qwe", description = "description4", downloadUrl = "downloadUrl3" },
                new { id = 5, title = "TaskName5", user = "qwe@qwe.rwe", description = "description5", downloadUrl = "downloadUrl4" },
                new { id = 6, title = "TaskName6", user = "asde@qwe.wq", description = "description6", downloadUrl = "" },
                new { id = 7, title = "TaskName7", user = "qwe@fd.qwfe", description = "description7", downloadUrl = "" },
                new { id = 8, title = "TaskName8", user = "zxc@qweo.ru", description = "description8", downloadUrl = "downloadUrl5" },
            };
            return list;
        }

        private Dictionary<string, int> GetTest2()
        {
            var dictionary = new Dictionary<string, int>
            {
                {"22", 1},
                {"14", 5},
                {"10", 6},
                {"8", 19},
                {"2", 3}
            };

            return dictionary;
        }
    }
}