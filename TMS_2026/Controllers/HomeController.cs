using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using TMS_2026.Models;
using TMS_Models.Entities;
using TMS_Repository.Data;

namespace TMS_2026.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger,AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult _TaskModal()
        {
            return View();
        }
        public IActionResult TaskTable(TaskListVM vm)
        {
            var query = _context.Tasks.AsQueryable();
            if (!string.IsNullOrWhiteSpace(vm.SearchTitle))
                query = query.Where(x => x.TaskTitle.Contains(vm.SearchTitle));

            if (!string.IsNullOrWhiteSpace(vm.Status))
                query = query.Where(x => x.TaskStatus == vm.Status);

            if (!string.IsNullOrWhiteSpace(vm.CreatedBy))
                query = query.Where(x => x.CreatedByUserName == vm.CreatedBy);

            vm.TotalRecords = query.Count();
            vm.Tasks = query
                .OrderByDescending(x => x.TaskId).AsNoTracking()
                .Skip((vm.Page - 1) * vm.PageSize)
                .Take(vm.PageSize)
                .ToList();

            return PartialView("_TaskTable", vm);
        }

        public IActionResult _TaskModalPartial(int id, string mode)
        {
            TaskEntity model;

            if (id == 0)
            {
                model = new TaskEntity(); // Create
            }
            else
            {
                model = _context.Tasks.FirstOrDefault(x => x.TaskId == id);
                if (model == null) return NotFound();
            }

            ViewBag.Mode = mode; // Create / Edit / View

            return PartialView("_TaskModalPartial", model);
        }

        public IActionResult TaskList(string searchTitle, string status, string createdBy, int page = 1, int pageSize = 10)
        {
            var query = _context.Tasks.AsQueryable();
            if (!string.IsNullOrEmpty(searchTitle))
                query = query.Where(x => x.TaskTitle.Contains(searchTitle));

            if (!string.IsNullOrEmpty(status))
                query = query.Where(x => x.TaskStatus == status);

            if (!string.IsNullOrEmpty(createdBy))
                query = query.Where(x => x.CreatedByUserName == createdBy);

            int total = query.Count();

            var tasks = query
                .OrderByDescending(x => x.TaskId).AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new TaskListVM
            {
                Tasks = tasks,
                Page = page,
                PageSize = pageSize,
                TotalRecords = total,
                SearchTitle = searchTitle,
                Status = status,
                CreatedBy = createdBy
            };

            return View(vm);
        }




        public IActionResult OpenTaskModal(int id, string mode)
        {
            TaskEntity model;

            if (mode == "Create")
            {
                model = new TaskEntity();
            }
            else
            {
                model = _context.Tasks.FirstOrDefault(x => x.TaskId == id);
                if (model == null)
                    return NotFound();
            }
            ViewBag.Modal = model;
            ViewBag.Mode = mode; // View / Edit / Create
            return PartialView("_TaskModal");
        }



        public IActionResult TaskModal(int? id, string mode)
        {
            TaskEntity model = null;

            if (id.HasValue)
            {
                model = _context.Tasks.FirstOrDefault(t => t.TaskId == id.Value);
                if (model == null) return NotFound();
            }

            ViewBag.Mode = mode; // create, edit, view
            return PartialView("_TaskModalPartial", model ?? new TaskEntity());
        }
        [HttpPost]
        public IActionResult SaveTask(TaskEntity model, int page, int pageSize)
        {
            bool isNew = model.TaskId == 0;
            TaskEntity task;

            if (isNew)
            {
                task = new TaskEntity
                {
                    TaskTitle = model.TaskTitle,
                    TaskDescription = model.TaskDescription,
                    TaskStatus = model.TaskStatus,
                    TaskDueDate = model.TaskDueDate,
                    TaskRemarks = model.TaskRemarks,
                    CreatedOn = DateTime.Now,
                    CreatedByUserName = "Admin"
                };

                _context.Tasks.Add(task);
                _context.SaveChanges();

                // 🔁 New record → go to first page
                page = 1;
            }
            else
            {
                task = _context.Tasks.FirstOrDefault(x => x.TaskId == model.TaskId);
                if (task == null)
                    return NotFound();

                task.TaskTitle = model.TaskTitle;
                task.TaskDescription = model.TaskDescription;
                task.TaskStatus = model.TaskStatus;
                task.TaskDueDate = model.TaskDueDate;
                task.TaskRemarks = model.TaskRemarks;
                task.LastUpdatedOn = DateTime.Now;

                _context.SaveChanges();
                // 🔁 Update → same page
            }

            // 🔁 Fresh data for correct page
            int totalRecords = _context.Tasks.Count();

            var tasks = _context.Tasks
                .OrderByDescending(x => x.TaskId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new TaskListVM
            {
                Tasks = tasks,
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
            return PartialView("~/Views/Home/_TaskTable.cshtml", vm);
            
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }




        [HttpPost]
        public IActionResult DeleteTask(int id, int page, int pageSize)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.TaskId == id);
            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            // 🔁 Total records
            int totalRecords = _context.Tasks.Count();

            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (page > totalPages && totalPages > 0)
                page = totalPages;

            var tasks = _context.Tasks
                .OrderByDescending(x => x.TaskId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new TaskListVM
            {
                Tasks = tasks,
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };

            return PartialView("~/Views/Home/_TaskTable.cshtml", model);
        }




    }
}
