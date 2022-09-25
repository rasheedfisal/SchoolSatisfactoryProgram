using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.BLL.Repos;

namespace SchoolSatisfactory.UI.Controllers
{
    [Authorize]
    public class SurvayController : Controller
    {
        private readonly IQuestionAnswerRepo _question;

        public SurvayController(IQuestionAnswerRepo question)
        {
            _question = question;
        }
        [Authorize("Authorization")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetSchoolRateStatistics(int schoolNo, string datefrom, string dateto)
        {
            var result = await _question.GetSchoolRateStatistic(schoolNo, datefrom, dateto);
            return Json(result);
        }

        public async Task<JsonResult> GetClientSchoolSurvay(int schoolNo, int terrId, int levelId, int classId, int genderId, int rateId, string datefrom, string dateto)
        {
            var result = await _question.GetClientSchoolSurvay(schoolNo, terrId, levelId, classId, genderId, rateId, datefrom, dateto);
            return Json(result);
        }
    }
}
