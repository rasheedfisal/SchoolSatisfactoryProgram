using School.Model;

namespace School.BLL.Repos
{
    public interface ISettingsRepo
    {
        Task<List<ClassificationModel>> GetAllClassifications();
        Task<List<GenderModel>> GetAllGenders();
        Task<List<LevelModel>> GetAllLevels();
        Task<List<SatisfactoryRateModel>> GetAllRate();
        Task<List<SchoolModel>> GetAllSchoolByFilter();
        Task<List<TerritoryModel>> GetAllTerritories();
        Task<List<SchoolFullDetailsModel>> GetSchoolFullDetailsByFilter(int territoryId = 0, int levelId = 0, int classificationId = 0, int genderId = 0, int satisfactoryId = 0);
        Task<List<QuestionFullDetailsModel>> GetQuestionFullDetails(int SchoolId, int territoryId = 0, int levelId = 0, int classificationId = 0,
                                                                    int genderId = 0, int satisfactoryId = 0);
    }
}