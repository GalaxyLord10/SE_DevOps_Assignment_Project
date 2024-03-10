using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SE_DevOps_Assignment_Project.Controllers;
using SE_DevOps_Assignment_Project.Models;
using SE_DevOps_DataLayer.Interfaces;
using SE_DevOps_DataLayer.Services;
using System.Security.Claims;
using Xunit;

namespace SE_DevOps_Assignment_Project.Tests
{
    public class TaskControllerTests
    {
        private readonly Mock<ITaskService> _mockTaskService = new Mock<ITaskService>();
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager = MockUserManager();
        private readonly TasksController _controller;

        public TaskControllerTests()
        {
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());

            var controllerContext = new ControllerContext
            {
                HttpContext = httpContextAccessorMock.Object.HttpContext
            };

            _controller = new TasksController(_mockTaskService.Object, _mockUserManager.Object)
            {
                ControllerContext = controllerContext,
                TempData = new TempDataDictionary(controllerContext.HttpContext, Mock.Of<ITempDataProvider>())
            };

            SetupCurrentUser("userId", "username");
        }

        private static Mock<UserManager<IdentityUser>> MockUserManager()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            var mgr = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<IdentityUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<IdentityUser>());

            return mgr;
        }

        private void SetupCurrentUser(string userId, string username)
        {
            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new IdentityUser { Id = userId, UserName = username });
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, username)
                }, "TestAuthentication"))
                }
            };
        }

        [Fact]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            var taskViewModel = new TaskViewModel
            {
                Title = "Test Task",
                Description = "Test Description",
                DueDate = System.DateTime.Now,
                IsCompleted = false,
                Category = SE_DevOps_DataLayer.Common.Category.Personal
            };

            _mockTaskService.Setup(s => s.AddTaskAsync(It.IsAny<SE_DevOps_DataLayer.Entities.Task>(), It.IsAny<IdentityUser>())).ReturnsAsync(new SE_DevOps_DataLayer.Entities.Task());

            var result = await _controller.Create(taskViewModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ActionName.Should().Be("Index");
        }

        [Fact]
        public async Task Edit_Post_ValidModel_RedirectsToIndex()
        {
            var taskViewModel = new TaskViewModel
            {
                TaskId = 1,
                Title = "Updated Test Task",
                Description = "Updated Test Description",
                DueDate = System.DateTime.Now,
                IsCompleted = true,
                Category = SE_DevOps_DataLayer.Common.Category.Personal
            };

            var taskEntity = new SE_DevOps_DataLayer.Entities.Task
            {
                TaskId = 1,
                Title = "Existing Test Task",
                Description = "Existing Test Description",
                DueDate = System.DateTime.Now,
                IsCompleted = false,
                Category = SE_DevOps_DataLayer.Common.Category.Personal
            };

            _mockTaskService.Setup(s => s.GetTaskByIdAsync(It.IsAny<int>(), It.IsAny<IdentityUser>()))
                .ReturnsAsync(taskEntity);

            _mockTaskService.Setup(s => s.UpdateTaskAsync(It.IsAny<SE_DevOps_DataLayer.Entities.Task>(), It.IsAny<IdentityUser>()))
                .ReturnsAsync(taskEntity);

            var result = await _controller.Edit(taskViewModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ActionName.Should().Be("Index");
        }
    }

}
