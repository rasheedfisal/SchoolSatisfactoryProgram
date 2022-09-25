using Microsoft.AspNetCore.Mvc;
using School.BLL.Repos;
using School.Model;
using SchoolSatisfactory.UI.Models;
using System.Diagnostics;
using School.BLL.Extensions;

namespace SchoolSatisfactory.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISettingsRepo _settings;
        private readonly IQuestionAnswerRepo _question;

        public HomeController(ILogger<HomeController> logger, ISettingsRepo settings, IQuestionAnswerRepo question)
        {
            _logger = logger;
            _settings = settings;
            _question = question;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult ClientInfo()
        {
            return View();
        }
        public IActionResult AboutSchool()
        {
            return View();
        }
        public IActionResult QuestionSurvay()
        {
            return View();
        }
        public IActionResult QuestionsNotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<JsonResult> GetTerritoryList()
        {
            List<TerritoryModel> result = await _settings.GetAllTerritories();
            return Json(result.ConvertTerritoryToList(0, "ar"));
        }
        public async Task<JsonResult> GetClassificationList()
        {
            List<ClassificationModel> result = await _settings.GetAllClassifications();
            return Json(result.ConvertClassificationToList(0, "ar"));
        }

        public async Task<JsonResult> GetGenderList()
        {
            List<GenderModel> result = await _settings.GetAllGenders();
            return Json(result.ConvertGenderToList(0, "ar"));
        }
        public async Task<JsonResult> GetLevelList()
        {
            List<LevelModel> result = await _settings.GetAllLevels();
            return Json(result.ConvertLevelToList(0, "ar"));
        }

        
        public async Task<JsonResult> GetSchoolList(int terrId, int levelId, int classId, int genderId)
        {
            List<SchoolFullDetailsModel> result = await _settings.GetSchoolFullDetailsByFilter(terrId, levelId, classId, genderId);
            return Json(result.ConvertSchoolToList(0, "ar"));
        }
        public async Task<JsonResult> GetAllSchoolList(int schoolId)
        {
            List<SchoolModel> result = await _settings.GetAllSchoolByFilter();
            return Json(result.ConvertSchoolAllToList(schoolId, "ar"));
        }

        public async Task<JsonResult> GetSchoolListWithQuestions(int schoolId, int terrId, int levelId, int classId, int genderId, int rateId)
        {
            List<SchoolFullDetailsModel> Schoolresult = await _settings.GetSchoolFullDetailsByFilter(terrId, levelId, classId, genderId);
            List<QuestionFullDetailsModel> result = await _settings.GetQuestionFullDetails(schoolId, terrId, levelId, classId, genderId, rateId);
            return Json(new { school = Schoolresult.ConvertSchoolToList(0, "ar"), gridResult = result });
        }

        public async Task<JsonResult> GetRateList()
        {
            List<SatisfactoryRateModel> result = await _settings.GetAllRate();
            return Json(result.ConvertSatisfactoryToList(0, "ar"));
        }

        public async Task<JsonResult> GetQuestions(int SchoolNo, int RateNo)
        {
            List<SchoolQuestionModel> result = await _question.GetSchoolQuestion(SchoolNo, RateNo);
            return Json(result);
        }

        public async Task<JsonResult> PostSurvay(List<ClientAnswerModel> Survaylist, int SchoolNo, int RateNo, string ClientName, string Qualification, string Others)
        {
            SchoolSatisfactoryModel schoolSatisfactoryInfo = new SchoolSatisfactoryModel
            {
                SchoolNo = SchoolNo,
                SatisfactoryRateNo = RateNo,
                ClientName = ClientName,
                ClientQualification = Qualification,
                Others = Others
            };
            bool result = await _question.InsertClientSatisfactoryAnswer(Survaylist, schoolSatisfactoryInfo);
            return Json(new { success = result });
        }
    }
}