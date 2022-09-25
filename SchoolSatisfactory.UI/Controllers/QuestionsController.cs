using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.BLL.Repos;
using School.Model;

namespace SchoolSatisfactory.UI.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly IQuestionAnswerRepo _question;

        public QuestionsController(IQuestionAnswerRepo question)
        {
            _question = question;
        }
        [Authorize("Authorization")]
        public IActionResult CreateQuestion()
        {
            return View();
        }

        [Authorize("Authorization")]
        public IActionResult QuestionOperation()
        {
            return View();
        }

        public async Task<JsonResult> PostQuestion(int rateNo, int schoolNo, string qNameAr, List<SchoolQuestionAnswerModel> answerList, int qId)
        {
            SchoolQuestionModel questionModel = new SchoolQuestionModel();
            questionModel.id = qId;
            questionModel.SchoolNo = schoolNo;
            questionModel.SchoolSatisfactoryNo = rateNo;
            questionModel.QuestionNameAr = qNameAr;
            questionModel.QuestionAnswers = answerList;
            if (qId != 0)
            {
                bool IsSuccess = await _question.UpdateQuestionWithAnswer(questionModel);
                return Json(new { isValid = IsSuccess });
            }
            else
            {
                bool IsSuccess = await _question.InsertQuestionWithAnswer(questionModel);
                return Json(new { isValid = IsSuccess });
            }
            
        }

        public async Task<JsonResult> GetSelectedQuestion(int qId)
        {
            var question = await _question.GetSelectedQuestionDetails(qId);
            return Json(question);
        }
        public async Task<JsonResult> DeleteQuestion(int qId)
        {
            try
            {
                var question = await _question.DeleteQuestion(qId);
                return Json(question);
            }
            catch (Exception ex)
            {

                return Json(false);
            }
            
        }
    }
}
