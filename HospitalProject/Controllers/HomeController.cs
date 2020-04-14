using HospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace HospitalProject.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Return if current user is administrator
        /// </summary>
        public bool IsCurrentUserAdmin(ApplicationDbContext db)
        {
            string userId = User.Identity.GetUserId();
            return db.Roles.Any(x => x.Name == "Administrator" && x.Users.Any(y => y.UserId == userId));           
        }

        /// <summary>
        /// Reporting
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Reporting()
        {
            var model = new ReportingPageViewModel();
            using (var db = new ApplicationDbContext())
            {

                // administrators are allowed to edit information
                model.IsEditable = IsCurrentUserAdmin(db);

                model.Reports = db.Reports.Select(
                    x => new ReportViewModel { 
                        Id = x.Id, 
                        Title = x.Title, 
                        Description = x.Description }).
                    ToList();

            }

            return View(model);
        }


        /// <summary>
        /// Report page
        /// </summary>
        public ActionResult Report(int id)
        {
            return View();
        }

        /// <summary>
        /// Creates new publication 
        /// </summary>
        [HttpGet]
        public ActionResult Create(int? id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (!IsCurrentUserAdmin(db))
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }

                PublicationViewModel publication = new PublicationViewModel
                {
                    Id = id ?? 0
                };

                return View(publication);
            }                      
        }

        /// <summary>
        /// Creates new report
        /// </summary>
        [HttpGet]
        public ActionResult CreateReport()
        {
            using (var db = new ApplicationDbContext())
            {
                if (!IsCurrentUserAdmin(db))
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }
                
                return View();
            }
        }


        /// <summary>
        /// Creates new report
        /// </summary>
        [HttpPost]
        public ActionResult CreateReport(ReportViewModel model)
        {
            using (var db = new ApplicationDbContext())
            {
                if (!IsCurrentUserAdmin(db))
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }

                var report = new Report();
                report.Title = model.Title;
                report.Description = model.Description;
                db.Reports.Add(report);
                db.SaveChanges();

                return RedirectToAction("Reporting");
            }
        }

        /// <summary>
        /// Creates new publication
        /// </summary>
        [HttpPost]
        public ActionResult Create(PublicationViewModel model)
        {
            using (var db = new ApplicationDbContext())
            {
                if (!IsCurrentUserAdmin(db))
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }

                var publication = new Publication();
                publication.Title = model.Title;
                publication.Body = model.Body;
                publication.ParentId = (model.Id == 0 ? (int?)null : model.Id);
                db.Publications.Add(publication);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = publication.Id });
            }            
        }


        /// <summary>
        /// Delete publication
        /// </summary>
        [HttpGet]        
        public ActionResult Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (!IsCurrentUserAdmin(db))
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }

                var publication = db.Publications.FirstOrDefault(x => x.Id == id);
                if (publication == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                db.Publications.Remove(publication);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = publication.ParentId });
            }
        }

        /// <summary>
        /// Delete publication
        /// </summary>
        [HttpGet]
        public ActionResult DeleteReport(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (!IsCurrentUserAdmin(db))
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }

                var report = db.Reports.FirstOrDefault(x => x.Id == id);
                if (report == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                db.Reports.Remove(report);
                db.SaveChanges();

                return RedirectToAction("Reporting");
            }
        }

        /// <summary>
        /// Save publication
        /// </summary>
        [HttpPost]
        public ActionResult Edit(PublicationViewModel model)
        {
            using (var db = new ApplicationDbContext())
            {
                if (!IsCurrentUserAdmin(db))
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }

                var publication = db.Publications.FirstOrDefault(x => x.Id == model.Id);
                if (publication == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                publication.Title = model.Title;
                publication.Body = model.Body;
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { id = model.Id });
        }
        
        /// <summary>
        /// Edit publication
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int? id)
        {            
            using (var db = new ApplicationDbContext())
            {                
                // if user is not admin --> get error ! 
                if (!IsCurrentUserAdmin(db))
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }

                PublicationViewModel publication = db.Publications.
                        Where(x => x.Id == id).
                        Select(x => new PublicationViewModel
                        {
                            Id = x.Id,
                            Body = x.Body,
                            ParentId = x.ParentId,
                            Title = x.Title,
                            TitleImage = x.TitleImage
                        }).FirstOrDefault();

                if (publication == null)
                {
                    // if publication is not found 
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                return View(publication);
            }
        }

        /// <summary>
        /// Save report
        /// </summary>
        [HttpPost]
        public ActionResult EditReport(ReportViewModel model)
        {
            using (var db = new ApplicationDbContext())
            {
                if (!IsCurrentUserAdmin(db))
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }

                var report = db.Reports.FirstOrDefault(x => x.Id == model.Id);
                if (report == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                report.Title = model.Title;
                report.Description = model.Description;
                db.SaveChanges();
            }

            return RedirectToAction("Reporting");
        }

        /// <summary>
        /// Edit report
        /// </summary>
        [HttpGet]
        public ActionResult EditReport(int? id)
        {
            using (var db = new ApplicationDbContext())
            {
                // if user is not admin --> get error ! 
                if (!IsCurrentUserAdmin(db))
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
                }

                ReportViewModel report = db.Reports.
                        Where(x => x.Id == id).
                        Select(x => new ReportViewModel
                        {
                            Id = x.Id, 
                            Title = x.Title, 
                            Description = x.Description                            
                        }).FirstOrDefault();

                if (report == null)
                {
                    // if report is not found 
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                return View(report);
            }
        }

        /// <summary>
        /// Show publication
        /// </summary>
        public ActionResult Index(int? id)
        {
            var model = new PublicationPageViewModel();
            using (var db = new ApplicationDbContext())
            {

                // administrators are allowed to edit information
                model.IsEditable = IsCurrentUserAdmin(db);

                if (id != null)
                {
                    // get current publication detailed info
                    // for instance: 
                    //   "Root" page has no info at all (id == null), we will not get here
                    //   "No Visitor policy" has id and body (visitors are currently restricted...) 
                    model.Publication = db.Publications.
                        Where(x => x.Id == id).
                        Select(x => new PublicationViewModel { 
                            Id = x.Id, 
                            Body = x.Body, 
                            ParentId = x.ParentId, 
                            Title = x.Title, 
                            TitleImage = x.TitleImage }).First();
                }

                // get all nested publications.
                // for instance: 
                //   root page has children: Patients, Visitors
                //   "Patients" page has children: No Visitor Policy,  Safe Care, What to bring
                model.Children = db.Publications.
                        Where(x => x.ParentId == id).
                        Select(x => new PublicationShortViewModel
                        {
                            Id = x.Id,
                            Title = x.Title,
                            TitleImage = x.TitleImage
                        }).ToList();
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}