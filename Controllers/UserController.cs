using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        public ActionResult Index(string searchString)
        {
            if (!userlist.Any())
            {
                userlist.Add(new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" });
                userlist.Add(new User { Id = 2, Name = "Jane Doe", Email = "jane.doe@example.com" });
                userlist.Add(new User { Id = 3, Name = "Jim Smith", Email = "jim.smith@example.com" });
                userlist.Add(new User { Id = 4, Name = "Jake Long", Email = "jake.long@example.com" });
                userlist.Add(new User { Id = 5, Name = "Jill Scott", Email = "jill.scott@example.com" });
            }

            var filteredList = userlist;

            // Check if the searchString is not null or empty
            if (!string.IsNullOrEmpty(searchString))
            {
                // Use LINQ to filter the userlist based on the searchString
                // This example filters users where the searchString is found in the Name or Email
                filteredList = userlist.Where(u => u.Name.Contains(searchString) || u.Email.Contains(searchString)).ToList();
            }

            // Return the view with the filtered list of users
            return View(filteredList);
        }
 
      // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Use LINQ to find the user in the userlist with the matching ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                // If no user is found with the provided ID, return a NotFound result
                return HttpNotFound();
            }

            // If a user is found, return the Details view with the user model
            return View(user);
        }
 
        // GET: User/Create
        public ActionResult Create()
        {
            // Create an empty user object
            User newUser = new User();

            // Insert it into the userList
            userlist.Add(newUser);

            // Typically, you would redirect to a view that displays the list of users or the details of the newly created user
            // For demonstration, redirecting to the Index view which could display all users
            return RedirectToAction("Index");
        }
 
        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            // Add the user to the userList
            userlist.Add(user);

            // Redirect to the Index action/view to display all users, including the newly added user
            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // Use LINQ to find the user in the userlist with the matching ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                // If no user is found with the provided ID, return a NotFound result
                return HttpNotFound();
            }

            // If a user is found, return the Edit view with the user model
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User updatedUser)
        {
            // Use LINQ to find the user in the userlist with the matching ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                // If no user is found with the provided ID, return a NotFound result
                return HttpNotFound();
            }

            // Update the user's information with the data received from the form submission
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            // Update other fields as necessary

            // Redirect to the Index action/view to display the updated list of users
            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Use LINQ to find the user in the userlist with the matching ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                // If no user is found with the provided ID, return a NotFound result
                return HttpNotFound();
            }

            // Remove the user from the userlist
            userlist.Remove(user);

            // Redirect to the Index action/view to display the remaining users
            return RedirectToAction("Index");
        }
 
        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Use LINQ to find the user in the userlist with the matching ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                // If a user is found, remove the user from the userlist
                userlist.Remove(user);
            }

            // Redirect to the Index action/view to display the remaining users
            return RedirectToAction("Index");
        }
    }
}
