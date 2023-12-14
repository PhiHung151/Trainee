using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tranning.DataDBContext;
using Tranning.Models;

namespace Tranning.Controllers
{
    public class Trainee_courseController : Controller
    {
        private readonly TranningDBContext _dbContext;
        private readonly ILogger<Trainee_courseController> _logger;

        public Trainee_courseController(TranningDBContext context, ILogger<Trainee_courseController> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            Trainee_courseModel trainee_courseModel = new Trainee_courseModel
            {
                Trainee_CourseDetailLists = new List<Trainee_courseDetail>(),
            };

            try
            {
                var data = from m in _dbContext.Trainee_Courses
                           select m;

                // Your existing code for filtering and populating the model
                // ...

                return View(trainee_courseModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Trainee Courses.");
                return View(trainee_courseModel); // Handle the error appropriately in your view
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                var traineeList = _dbContext.Users
                    .Where(u => u.deleted_at == null && u.role_id == 3)
                    .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.full_name })
                    .ToList();

                var courseList = _dbContext.Courses
                    .Where(m => m.deleted_at == null)
                    .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                    .ToList();

                ViewBag.Users = traineeList;  // Corrected ViewBag name
                ViewBag.Courses = courseList;

                Trainee_courseDetail trainee_course = new Trainee_courseDetail();
                return View(trainee_course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving data for Trainee Course form.");
                // Handle the error appropriately, maybe redirect to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Trainee_courseDetail trainee_course)
        {
            try
            {
                if (ModelState.IsValid && trainee_course != null)
                {
                    var trainee_courseData = new Trainee_Course()
                    {
                        trainee_id = trainee_course.trainee_id,
                        course_id = trainee_course.course_id,
                        created_at = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    };

                    _dbContext.Trainee_Courses.Add(trainee_courseData);
                    _dbContext.SaveChanges(true);
                    TempData["saveStatus"] = true;

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding Trainee Course.");
                TempData["saveStatus"] = false;
                TempData["errorMessage"] = ex.Message; // Add this line to display the error message
            }

            var courseList = _dbContext.Courses
                .Where(m => m.deleted_at == null)
                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                .ToList();

            var userList = _dbContext.Users
                .Where(u => u.deleted_at == null && u.role_id == 3)
                .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.full_name })
                .ToList();

            ViewBag.Courses = courseList;
            ViewBag.Users = userList;

            return View(trainee_course);
        }

        // Add other actions as needed...

        // Example Delete action
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var trainee_courseData = _dbContext.Trainee_Courses.Find(id);

            if (trainee_courseData == null)
            {
                return NotFound();
            }

            var trainee_course = new Trainee_courseDetail
            {
                trainee_id = trainee_courseData.trainee_id,
                course_id = trainee_courseData.course_id,
                // Map other properties as needed
            };

            return View(trainee_course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainee_courseData = await _dbContext.Trainee_Courses.FindAsync(id);

            if (trainee_courseData == null)
            {
                return NotFound();
            }

            _dbContext.Trainee_Courses.Remove(trainee_courseData);
            await _dbContext.SaveChangesAsync();

            TempData["deleteStatus"] = true;

            return RedirectToAction(nameof(Index));
        }
    }
}
