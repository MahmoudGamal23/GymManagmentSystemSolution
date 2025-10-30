﻿using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



    namespace GymManagementPL.Controllers
    {
      
        public class PlanController : Controller
        {

            public IPlanService _planService { get; }

            public PlanController(IPlanService planService)
            {
                _planService = planService;
            }

            public ActionResult Index()
            {
                var Plan = _planService.GetAllPlans();
                return View(Plan);
            }

            public ActionResult Details(int id)
            {
                if (id <= 0)
                {
                    TempData["ErrorMessage"] = "Invalid Plan ID ";
                    return RedirectToAction(nameof(Index));
                }
                var Plan = _planService.GetPlanDetails(id);
                if (Plan == null)
                {
                    TempData["ErrorMessage"] = "Plan Not Found";
                    return RedirectToAction(nameof(Index));
                }
                return View(Plan);
            }

            public ActionResult Edit(int id)
            {
                if (id <= 0)
                {
                    TempData["ErrorMessage"] = "Invalid Plan ID ";
                    return RedirectToAction(nameof(Index));
                }
                var Plan = _planService.GetPlanToUpdate(id);
                if (Plan == null)
                {
                    TempData["ErrorMessage"] = "Plan Not Found";
                    return RedirectToAction(nameof(Index));
                }
                return View(Plan);

            }
            [HttpPost]
            public ActionResult Edit([FromRoute] int id, UpdatedPlanViewModel updatePlan)
            {

                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("WrongData", "Chack Data Validation");
                    return View(updatePlan);
                }

                var Result = _planService.UpdatePlan(id, updatePlan);
                if (Result)
                {
                    TempData["SuccessMessage"] = "Plan Is Update Successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Plan Failed to Update";

                }
                return RedirectToAction(nameof(Index));

            }
            [HttpPost]
            public ActionResult Activate(int id)
            {
                var Result = _planService.ToggleStatus(id);
                if (Result)
                {
                    TempData["SuccessMessage"] = "Plan Status Changed";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to Plan Status Changed ";
                }

                return RedirectToAction(nameof(Index));
            }


        }
    }
