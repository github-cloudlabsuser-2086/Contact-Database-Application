using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Web.Mvc;
using System.Linq;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Index_ReturnsView_WithAllUsers()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Index("") as ViewResult;
            var model = result.Model as System.Collections.Generic.List<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.Count); // Assuming no users are added by default
        }

        [TestMethod]
        public void Details_UserExists_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Add(new User { Id = 1, Name = "Test User", Email = "test@example.com" });

            // Act
            var result = controller.Details(1) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
        }

        [TestMethod]
        public void Details_UserDoesNotExist_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Details(99);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Create_Post_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var newUser = new User { Id = 1, Name = "New User", Email = "new@example.com" };

            // Act
            var result = controller.Create(newUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, UserController.userlist.Count); // Verify the user was added
        }

        [TestMethod]
        public void Edit_UserExists_UpdatesUserAndRedirects()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Add(new User { Id = 1, Name = "Original User", Email = "original@example.com" });
            var updatedUser = new User { Id = 1, Name = "Updated User", Email = "updated@example.com" };

            // Act
            var result = controller.Edit(1, updatedUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            var user = UserController.userlist.FirstOrDefault(u => u.Id == 1);
            Assert.AreEqual("Updated User", user.Name);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Delete_UserExists_RemovesUserAndRedirects()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Add(new User { Id = 1, Name = "User", Email = "user@example.com" });

            // Act
            var result = controller.Delete(1) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, UserController.userlist.Count); // Verify the user was removed
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        // Additional tests for other methods and edge cases can be added here
    }
}
