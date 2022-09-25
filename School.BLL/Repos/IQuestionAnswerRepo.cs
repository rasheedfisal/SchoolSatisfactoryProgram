using School.Model;

namespace School.BLL.Repos
{
    public interface IQuestionAnswerRepo
    {
        Task<bool> InsertQuestionWithAnswer(SchoolQuestionModel questionModel);
        Task<bool> UpdateQuestionWithAnswer(SchoolQuestionModel questionModel);
        Task<bool> DeleteQuestion(int questionId);
        Task<bool> DeleteQuestionAnswer(int answerId);
        Task<int> InsertQuestion(SchoolQuestionModel questionModel);
        Task<int> InsertQuestionAnswer(SchoolQuestionAnswerModel answerModel);
        Task<bool> UpdateQuestion(SchoolQuestionModel questionModel);
        Task<bool> UpdateQuestionAnswer(SchoolQuestionAnswerModel answerModel);
        Task<List<SchoolQuestionModel>> GetSchoolQuestion(int SchoolNo, int RateNo);
        Task<bool> InsertClientSatisfactoryAnswer(List<ClientAnswerModel> answerModel, SchoolSatisfactoryModel school);
        Task<List<QuestionAnswerFullDetails>> GetSelectedQuestionDetails(int qId);
        Task<List<QuestionAnswerFullDetails>> GetSchoolRateStatistic(int SchoolId, string dateFrom = null, string dateTo = null);
        Task<List<ClientSchoolSurvayModel>> GetClientSchoolSurvay(int SchoolId, int territoryId = 0, int levelId = 0, int classificationId = 0,
                                                                    int genderId = 0, int satisfactoryId = 0, string dateFrom = null, string dateTo = null);
    }
}