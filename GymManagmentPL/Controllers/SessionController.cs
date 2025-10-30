using GymManagementBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{

    public class SessionController : Controller
    {
        public ISessionService _sessionService { get; }

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }


        public ActionResult Index()
        {

            var Session = _sessionService.GetAllSessions();
            return View(Session);
        }

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session ID Can Not be 0 or Naigative Numder";
                return RedirectToAction(nameof(Index));
            }
            var Session = _sessionService.GetSessionById(id);
            if (Session == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(Session);
        }
        public ActionResult Create()
        {
            LoadTrainersDropDowns();
            LoadCategoriesDropDowns();
            return View();
        }


        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createSession)
        {
            if (!ModelState.IsValid)
            {
                LoadTrainersDropDowns();
                LoadCategoriesDropDowns();
                ModelState.AddModelError("DataInvalid", "Check The Data You Enterd");
                return View(nameof(Create), createSession);
            }
            bool Result = _sessionService.CreateSession(createSession);
            if (Result)
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Create Session";
                LoadTrainersDropDowns();
                LoadCategoriesDropDowns();
                return View(nameof(Create), createSession);
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session ID Can Not be 0 or Naigative Numder";
                return RedirectToAction(nameof(Index));
            }
            var Session = _sessionService.GetSessionForUpdate(id);
            if (Session == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            LoadTrainersDropDowns();
            return View(Session);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdateSessionViewModel updateSession)
        {
            if (!ModelState.IsValid)
            {
                LoadTrainersDropDowns();
                return View(updateSession);
            }
            var Result = _sessionService.UpdateSession(updateSession, id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Session Update Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Update Session";

            }
            return RedirectToAction(nameof(Index));


        }
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session ID ";
                return RedirectToAction(nameof(Index));
            }
            var Session = _sessionService.GetSessionById(id);

            if (Session == null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = Session.Id;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var Result = _sessionService.RemoveSession(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Session  Deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Deleted Session";

            }
            return RedirectToAction(nameof(Index));


        }


        #region mht
        private void LoadCategoriesDropDowns()
        {

            var categories = _sessionService.GetAllCategoryForDropDown();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");


        }
        private void LoadTrainersDropDowns()
        {


            var Trainers = _sessionService.GetAllTrainersForDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
        }
        #endregion

    }
}