using AspNetCoreCRUD.Models.ViewModels;
using AspNetCoreCRUD.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreCRUD.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetStudentViewModelAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentRegistrationDto studentData)
        {
            if (!ModelState.IsValid)
            {
                if (studentData.Dob == DateTime.MinValue)
                {
                    ModelState.Remove("Dob");
                    ModelState.AddModelError("Dob", "Date of birth is required.");
                }
                if (studentData.Fee == 0)
                {
                    ModelState.Remove("Fee");
                    ModelState.AddModelError("Fee", "Please enter Student Fee");
                }
                return View("Index", await GetStudentViewModelAsync());
            }

            var isSuccess = await _studentRepository.CreateOrUpdateAsync(studentData);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, "Unable to save student details.");
                return View("Index", await GetStudentViewModelAsync());
            }

            TempData["Message"] = "Student saved successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                TempData["Message"] = "Student not found.";
                return RedirectToAction("Index");
            }

            return View("EditStudent", student);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentRegistrationDto model)
        {
            var result = await _studentRepository.CreateOrUpdateAsync(model);
            TempData["Message"] = result ? "Student updated successfully!" : "Update failed.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveStudent(int id)
        {
            var result = await _studentRepository.RemoveByIdAsync(id);
            TempData["Message"] = result ? "Student deleted successfully!" : "Failed to delete student.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewStudent(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                TempData["Message"] = "Student not found.";
                return RedirectToAction("Index");
            }

            return View("ViewStudent", student);
        }

        #region Private Helpers

        private async Task<StudentRegistrationDto> GetStudentViewModelAsync()
        {
            return new StudentRegistrationDto
            {
                studentRegistrationList = await _studentRepository.GetListAsync()
            };
        }

        #endregion
    }
}
