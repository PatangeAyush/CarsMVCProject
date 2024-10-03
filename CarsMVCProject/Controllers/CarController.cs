using CarsMVCProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsMVCProject.Controllers
{
   
    public class CarController : Controller
    {
        Logic logicc = new Logic();
        // GET: Car
        public ActionResult Index(string search, string searchBy)
        {
            var data = logicc.Getcar();

            if (!string.IsNullOrEmpty(search) && searchBy == "CarName")
            {
                data = data.Where(book => book.CarName?.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            var count = data.Count;
            ViewBag.Count = count;
            return View(data);
        }

        // GET: Car/Details/5
        public ActionResult Details(int id)
        {
            var row = logicc.Getcar().Find(model => model.ID == id);

            return View(row);
        }

        // GET: Car/Create
        [Route("Car/Add")]
        public ActionResult Create()
        {
            var obj = logicc.GetColours();  

            ViewBag.colour = new SelectList(obj, "CID", "Colours");
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(InsertCarModel insert, HttpPostedFileBase file)
        {

            var CheckEmailExist = logicc.EMAIL(insert.Email, 0, "INSERT");
            if (CheckEmailExist == true)
            {
                var obj = logicc.GetColours();
                ViewBag.Drop = new SelectList(obj, "CID", "Colours");
                ViewBag.ErrorMessage = "This Email is Already in use.";
                return View(insert);
            }
            else
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/UserImages/"), fileName);
                    insert.UploadImage = "~/UserImages/" + fileName;
                    file.SaveAs(path);
                }
                logicc.AddCar(insert);
                string subject = "New Car Added";
                string body = $"Dear{insert.CusName},<br>" +
                           "Your Car Was Successfully Added.<br> " +
                           "$Car: { insert.CarName}<br>" +
                           "$Price : {insert.Carprice}<br>" +
                           "Thank You !!";

                logicc.SendEMAIL(insert.Email, subject, body);

                TempData["InsertMessege"] = "Data Inserted Successfully";

                return RedirectToAction("Index");

            }
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            var colours = logicc.GetColours();
            ViewBag.col = new SelectList(colours, "CID", "Colours");
            var row = logicc.updateGetcar().Find(model => model.ID == id);
            return View(row);
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UpdateCarModel update)
        {
            try
            {
                //var checkEmail = logicc.EMAIL(update.Email, id, "UPDATE");
                //if(checkEmail == true)
                //{
                //    var obj = logicc.GetColours();
                //    ViewBag.dropdown = new SelectList(obj, "id", "Colours");
                //    ViewBag.ErrorMessege = "This Email is Already Exists.";
                //    return View();
                //}
                //else
                //{
                    if (ModelState.IsValid == true)
                    {
                        bool check = logicc.UpdateCar(update);
                        if (check == true)
                        {
                            TempData["UpdateMessege"] = "Data Updated Successfully";
                            ModelState.Clear();
                           
                        }
                    }
                    return RedirectToAction("Index");
                //}
            }
            catch
            {
                return View();
            }
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            var row = logicc.Getcar().Find(model => model.ID == id);
            return View(row);

        }

        // POST: Car/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, GetCarModel delete)
        {
            try
            {
                bool check = logicc.DeleteCar(id);
                if (check == true)
                {
                    TempData["DeleteCar"] = "Car Deleted Successfully";

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}