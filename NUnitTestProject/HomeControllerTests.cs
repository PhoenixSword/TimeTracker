using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TimeTracker.Controllers;
using TimeTracker.Models.Repositories.Abstract;
using TimeTracker.ViewModels;

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
        [TestCaseSource(nameof(_list))]
        public async Task GetAllTasksUnitTest1(List<object> list)
        {
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAllTasks())
                .ReturnsAsync(list);
            var controller = new HomeController(mockService.Object);

            //Act
            var result = await controller.GetAllTasks();

            Assert.IsNotNull(result);
        }

        [Test]
        [TestCaseSource(nameof(_list))]
        public async Task GetAllTasksUnitTest2(List<object> list)
        {
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAllTasks())
                .ReturnsAsync(list);
            var controller = new HomeController(mockService.Object);

            //Act
            var result = await controller.GetAllTasks();

            Assert.IsInstanceOf<List<object>>(result);
        }

        [Test]
        [TestCaseSource(nameof(_list))]
        public async Task GetAllTasksUnitTest3(List<object> list)
        {
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAllTasks())
                .ReturnsAsync(list);

            var controller = new HomeController(mockService.Object);

            //Act
            var result = await controller.GetAllTasks();

            Assert.AreEqual(list.Count, result.Count);
        }

        [Test]
        [TestCaseSource(nameof(_list))]
        public async Task GetAllTasksUnitTest4(List<object> list)
        {
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAllTasks())
                .ReturnsAsync(list);
            //Act
            var result = await mockService.Object.GetAllTasks();

            Assert.IsNotNull(result);
        }

        [Test]
        [TestCaseSource(nameof(_list))]
        public async Task GetAllTasksUnitTest5(List<object> list)
        {
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAllTasks())
                .ReturnsAsync(list);

            //Act
            var result = await mockService.Object.GetAllTasks();

            Assert.IsInstanceOf<List<object>>(result);
        }

        [Test]
        [TestCaseSource(nameof(_list))]
        public async Task GetAllTasksUnitTest6(List<object> list)
        {
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAllTasks())
                .ReturnsAsync(list);

            //Act
            var result = await mockService.Object.GetAllTasks();

            Assert.AreEqual(list.Count, result.Count);
        }

        [Test]
        [TestCaseSource(nameof(_list2))]
        public async Task GetAllUnitTest1(Dictionary<string, int> dictionary)
        {
            var date = DateTime.Now;
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAll("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date))
                .ReturnsAsync(dictionary);

            //Act
            var result = await mockService.Object.GetAll("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date);

            Assert.AreEqual(dictionary.Count, result.Count);
        }

        [Test]
        [TestCaseSource(nameof(_list2))]
        public async Task GetAllUnitTest2(Dictionary<string, int> dictionary)
        {
            var date = DateTime.Now;
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAll("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date))
                .ReturnsAsync(dictionary);

            //Act
            var result = await mockService.Object.GetAll("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date);

            Assert.IsInstanceOf<Dictionary<string, int>>(result);
        }

        [Test]
        [TestCaseSource(nameof(_list2))]
        public async Task GetAllUnitTest3(Dictionary<string, int> dictionary)
        {
            var date = DateTime.Now;
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetAll("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date))
                .ReturnsAsync(dictionary);

            //Act
            var result = await mockService.Object.GetAll("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date);

            Assert.IsNotNull(result);
        }

        [Test]
        [TestCaseSource(nameof(_list3))]
        public async Task GetInfoUnitTest1(Dictionary<string, List<object>> dictionary)
        {
            var date = DateTime.Now;
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetInfo("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date))
                .ReturnsAsync(dictionary);

            //Act
            var result = await mockService.Object.GetInfo("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date);

            Assert.AreEqual(dictionary.Count, result.Count);
        }

        [Test]
        [TestCaseSource(nameof(_list3))]
        public async Task GetInfoUnitTest2(Dictionary<string, List<object>> dictionary)
        {
            var date = DateTime.Now;
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetInfo("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date))
                .ReturnsAsync(dictionary);

            //Act
            var result = await mockService.Object.GetInfo("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date);

            Assert.IsInstanceOf<Dictionary<string, List<object>>>(result);
        }

        [Test]
        [TestCaseSource(nameof(_list3))]
        public async Task GetInfoUnitTest3(Dictionary<string, List<object>> dictionary)
        {
            var date = DateTime.Now;
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetInfo("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date))
                .ReturnsAsync(dictionary);

            //Act
            var result = await mockService.Object.GetInfo("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date);

            Assert.IsNotNull(result);
        }

        [Test]
        [TestCaseSource(nameof(_list4))]
        public async Task GetTasksUnitTest1(IEnumerable<TaskViewModel> listModels)
        {
            var date = DateTime.Now;
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetTasks("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date))
                .ReturnsAsync(listModels);

            //Act
            var result = await mockService.Object.GetTasks("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date);

            Assert.AreEqual(listModels, result);
        }

        [Test]
        [TestCaseSource(nameof(_list4))]
        public async Task GetTasksUnitTest2(IEnumerable<TaskViewModel> listModels)
        {
            var date = DateTime.Now;
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetTasks("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date))
                .ReturnsAsync(listModels);

            //Act
            var result = await mockService.Object.GetTasks("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date);

            Assert.IsInstanceOf<IEnumerable<TaskViewModel>> (result);
        }

        [Test]
        [TestCaseSource(nameof(_list4))]
        public async Task GetTasksUnitTest3(IEnumerable<TaskViewModel> listModels)
        {
            var date = DateTime.Now;
            var mockService = new Mock<ICalendarRepo>();
            mockService
                .Setup(m => m.GetTasks("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date))
                .ReturnsAsync(listModels);

            //Act
            var result = await mockService.Object.GetTasks("dnyCifOPf8VvZPQjf1Ti9iKvNcY2", date);

            Assert.IsNotNull(result);
        }


        static object[] _list =
        {
            new List<object>
            {
                new
                {
                    id = 1, title = "TaskName1", user = "qwe@qwe.qwe", description = "description1",
                    downloadUrl = "downloadUrl1"
                },
                new
                {
                    id = 2, title = "TaskName2", user = "qwe@qwe.com", description = "description2",
                    downloadUrl = "downloadUrl2"
                }
            },

            new List<object>
            {
                new {id = 3, title = "TaskName3", user = "123@qwe.qwe", description = "description3", downloadUrl = ""},
                new
                {
                    id = 4, title = "TaskName4", user = "qwe@123.qwe", description = "description4",
                    downloadUrl = "downloadUrl3"
                },
                new
                {
                    id = 5, title = "TaskName5", user = "qwe@qwe.rwe", description = "description5",
                    downloadUrl = "downloadUrl4"
                }
            },

            new List<object>
            {
                new {id = 6, title = "TaskName6", user = "asde@qwe.wq", description = "description6", downloadUrl = ""},
                new {id = 7, title = "TaskName7", user = "qwe@fd.qwfe", description = "description7", downloadUrl = ""},
                new
                {
                    id = 8, title = "TaskName8", user = "zxc@qweo.ru", description = "description8",
                    downloadUrl = "downloadUrl5"
                }
            }
        };

        static object[] _list2 =
        {
            new Dictionary<string, int>
            {
                {"22", 1},
                {"14", 5}
            },

            new Dictionary<string, int>
            {
                {"10", 6},
                {"8", 19}
            },

            new Dictionary<string, int>
            {
                {"2", 3}
            }
        };

        static object[] _list3 =
        {
            new Dictionary<string, List<object>>
            {
                {
                    "22", new List<object>
                    {
                        new {name = "asd", value = 4}
                    }
                },
                {
                    "03", new List<object>
                    {
                        new {name = "qwe", value = 5}
                    }
                },
                {
                    "14", new List<object>
                    {
                        new {name = "zxc", value = 6}
                    }
                }

            },

            new Dictionary<string, List<object>>
            {
                {
                    "22", new List<object>
                    {
                        new {name = "asd", value = 1}
                    }
                },
                {
                    "03", new List<object>
                    {
                        new {name = "qwe", value = 2}
                    }
                },
                {
                    "14", new List<object>
                    {
                        new {name = "zxc", value = 3}
                    }
                }

            }
        };

        static object[] _list4 =
        {
            new List<TaskViewModel>
            {
                new TaskViewModel
                {
                    Description = "descr", Hours = 4
                }
            },

            new List<TaskViewModel>
            {
                new TaskViewModel
                {
                    Description = "descr", Hours = 5, Name = "qwe"
                }
            },

            new List<TaskViewModel>
            {
                new TaskViewModel
                {
                    Description = "descr", Hours = 7, Id = "123123"
                }
            }
        };
    }
}