
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tranning.DataDBContext;
using Tranning.Models;

namespace Tranning.Controllers
{
    public class Trainner_TopicController : Controller
    {
        private readonly TranningDBContext _dbContext;
        private readonly ILogger<Trainner_TopicController> _logger;

        public Trainner_TopicController(TranningDBContext context, ILogger<Trainner_TopicController> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            Trainner_TopicModel trainner_topicModel = new Trainner_TopicModel
            {
                Trainner_TopicDetailLists = new List<Trainner_TopicDetail>(),
            };

            try
            {
                var data = from m in _dbContext.Trainner_Topics
                           select m;

                // Your existing code for filtering and populating the model
                // ...

                return View(trainner_topicModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Trainer Topics.");
                return View(trainner_topicModel); // Handle the error appropriately in your view
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                var trainnerList = _dbContext.Users
                    .Where(u => u.deleted_at == null && u.role_id == 3)
                    .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.full_name })
                    .ToList();

                var topicList = _dbContext.Topics
                    .Where(m => m.deleted_at == null)
                    .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                    .ToList();

                ViewBag.Users = trainnerList;  // Corrected ViewBag name
                ViewBag.Topics = topicList;

                Trainner_TopicDetail trainner_topic = new Trainner_TopicDetail();
                return View(trainner_topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving data for Trainer Topic form.");
                // Handle the error appropriately, maybe redirect to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Trainner_TopicDetail trainner_topic)
        {
            try
            {
                if (ModelState.IsValid && trainner_topic != null)
                {
                    var trainner_topicData = new Trainner_Topic()
                    {
                        trainner_id = trainner_topic.trainner_id,
                        topic_id = trainner_topic.topic_id,
                        created_at = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    };

                    _dbContext.Trainner_Topics.Add(trainner_topicData);
                    _dbContext.SaveChanges(true);
                    TempData["saveStatus"] = true;

                    return RedirectToAction(nameof(Index));
                }
                else // Log model state errors
                {
                    foreach (var modelStateKey in ModelState.Keys)
                    {
                        var errors = ModelState[modelStateKey].Errors;
                        foreach (var error in errors)
                        {
                            _logger.LogError($"Model state error in key {modelStateKey}: {error.ErrorMessage}");
                        }
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle database-related exceptions
                // ...
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // ...
            }

            // Populate dropdown lists in case of failure
            var trainner_topicList = _dbContext.Topics
                .Where(m => m.deleted_at == null)
                .Select(m => new SelectListItem { Value = m.id.ToString(), Text = m.name })
                .ToList();

            var userList = _dbContext.Users
                .Where(u => u.deleted_at == null && u.role_id == 3)
                .Select(u => new SelectListItem { Value = u.id.ToString(), Text = u.full_name })
                .ToList();

            ViewBag.Topics = trainner_topicList;  // Corrected ViewBag name
            ViewBag.Users = userList;

            return View(trainner_topic);
        }


        // Add other actions as needed...

        // Example Delete action
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var trainner_topicData = _dbContext.Trainner_Topics.Find(id);

            if (trainner_topicData == null)
            {
                return NotFound();
            }

            var trainner_topic = new Trainner_TopicDetail
            {
                trainner_id = trainner_topicData.trainner_id,
                topic_id = trainner_topicData.topic_id,
                // Map other properties as needed
            };

            return View(trainner_topic);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainner_topicData = await _dbContext.Trainner_Topics.FindAsync(id);

            if (trainner_topicData == null)
            {
                return NotFound();
            }

            _dbContext.Trainner_Topics.Remove(trainner_topicData);
            await _dbContext.SaveChangesAsync();

            TempData["deleteStatus"] = true;

            return RedirectToAction(nameof(Index));
        }
    }
}
