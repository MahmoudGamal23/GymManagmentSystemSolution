using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }


        #region Get All Trainers
        public ActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        }

        #endregion

        #region Create Trainer

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Create), model);
            }


            var result = _trainerService.CreateTrainer(model);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer created successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Trainer with this email or phone already exists.");
                return View(nameof(Create), model);
            }

        }
        #endregion

        #region Trainer Details
        // GET: Trainer/Details/5
        public ActionResult Details(int id)
        {
            var trainer = _trainerService.GetTrainerDetails(id);

            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }
        #endregion

        #region Edit Trainer
        public ActionResult Edit(int id)
        {
            var trainer = _trainerService.GetTrainerToUpdate(id);

            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        // POST: Trainer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TrainerToUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _trainerService.UpdateTrainerDetails(model, id);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update trainer.";
            }

            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Delete Trainer
        // GET: Trainer/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerDetails(id);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Fount";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = id;
            return View();
        }

        // POST: Trainer/DeleteConfirmed
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _trainerService.RemoveTrainer(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete trainer";
            }


            return RedirectToAction(nameof(Index));
        }
        #endregion



    }
}