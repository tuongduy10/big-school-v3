using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        // GET: Courses
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Heading = "Add Course"
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        //public ActionResult Attending()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var courses = _dbContext.Attendances
        //        .Where(c => c.AttendeeId == userId)
        //        .Select(a => a.Course)
        //        .Include(c => c.Lecturer)
        //        .Include(c => c.Category)
        //        .ToList();

        //    var viewModel = new CoursesViewModel
        //    {
        //        UpcommingCourses = courses,
        //        ShowAction = User.Identity.IsAuthenticated
        //    };

        //    return View(viewModel);
        //}

        //[Authorize]
        //public ActionResult Mine()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var courses = _dbContext.Courses
        //        .Where(c => c.LecturerId == userId && c.DateTime > DateTime.Now && !c.IsCanceled)
        //        .Include(l => l.Lecturer)
        //        .Include(c => c.Category)
        //        .ToList();

        //    return View(courses);
        //}

        //[Authorize]
        //public ActionResult Edit(int id)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var course = _dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);

        //    var viewModel = new CourseViewModel
        //    {
        //        Categories = _dbContext.Categories.ToList(),
        //        Date = course.DateTime.ToString("dd/MM/yyyy"),
        //        Time = course.DateTime.ToString("HH:mm"),
        //        Category = (byte)course.CategoryId,
        //        Place = course.Place,
        //        Heading = "Edit Course",
        //        Id = course.Id
        //    };

        //    return View("Create", viewModel);
        //}

        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Update(CourseViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        viewModel.Categories = _dbContext.Categories.ToList();
        //        return View("Create", viewModel);
        //    }
        //    var userId = User.Identity.GetUserId();
        //    var course = _dbContext.Courses.Single(c => c.Id == viewModel.Id && c.LecturerId == userId);

        //    course.Place = viewModel.Place;
        //    course.DateTime = viewModel.GetDateTime();
        //    course.CategoryId = viewModel.Category;

        //    _dbContext.SaveChanges();

        //    return RedirectToAction("Index", "Home");
        //}
    }
}