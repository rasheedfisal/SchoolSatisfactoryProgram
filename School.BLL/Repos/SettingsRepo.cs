using School.DAL;
using School.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL.Repos
{
    public class SettingsRepo : ISettingsRepo
    {
        private readonly ISqlDataAccess _sqlData;

        public SettingsRepo(ISqlDataAccess sqlData)
        {
            _sqlData = sqlData;
        }

        public async Task<List<TerritoryModel>> GetAllTerritories()
        {
            string sql = string.Format("select * from tbl_Territory");
            return await _sqlData.LoadDataQuery<TerritoryModel>(sql);
        }
        public async Task<List<LevelModel>> GetAllLevels()
        {
            string sql = string.Format("select * from tbl_Level");
            return await _sqlData.LoadDataQuery<LevelModel>(sql);
        }
        public async Task<List<ClassificationModel>> GetAllClassifications()
        {
            string sql = string.Format("select * from tbl_Classification");
            return await _sqlData.LoadDataQuery<ClassificationModel>(sql);
        }
        public async Task<List<GenderModel>> GetAllGenders()
        {
            string sql = string.Format("select * from tbl_Gender");
            return await _sqlData.LoadDataQuery<GenderModel>(sql);
        }
        public async Task<List<SatisfactoryRateModel>> GetAllRate()
        {
            string sql = string.Format("select * from tbl_SatisfactoryRate");
            return await _sqlData.LoadDataQuery<SatisfactoryRateModel>(sql);
        }
        public async Task<List<SchoolModel>> GetAllSchoolByFilter()
        {
            string sql = string.Format("select * from tbl_School");
            return await _sqlData.LoadDataQuery<SchoolModel>(sql);
        }
        public async Task<List<SchoolFullDetailsModel>> GetSchoolFullDetailsByFilter(int territoryId = 0, int levelId = 0, int classificationId = 0,
                                                                    int genderId = 0, int satisfactoryId = 0)
        {
            string sql = string.Format("select * from vwSchoolFullDetails");
            string Condition = " where ", ConditionDetails = null;
            if (territoryId != 0)
            {
                if (!string.IsNullOrEmpty(ConditionDetails))
                    ConditionDetails += " and ";
                ConditionDetails += string.Format(" TerritoryNo = {0}", territoryId);
            }
            if (levelId != 0)
            {
                if (!string.IsNullOrEmpty(ConditionDetails))
                    ConditionDetails += " and ";
                ConditionDetails += string.Format(" LevelNo = {0}", levelId);
            }
            if (classificationId != 0)
            {
                if (!string.IsNullOrEmpty(ConditionDetails))
                    ConditionDetails += " and ";
                ConditionDetails += string.Format(" ClassificationNo = {0}", classificationId);
            }
            if (genderId != 0)
            {
                if (!string.IsNullOrEmpty(ConditionDetails))
                    ConditionDetails += " and ";
                ConditionDetails += string.Format(" GenderNo = {0}", genderId);
            }
            if (satisfactoryId != 0)
            {
                if (!string.IsNullOrEmpty(ConditionDetails))
                    ConditionDetails += " and ";
                ConditionDetails += string.Format(" SatisfactoryRateId = {0}", satisfactoryId);
            }
            if (!string.IsNullOrEmpty(ConditionDetails))
                sql += Condition + ConditionDetails;
            sql += " order by SchoolNameAr asc";

            return await _sqlData.LoadDataQuery<SchoolFullDetailsModel>(sql);
        }
        public async Task<List<QuestionFullDetailsModel>> GetQuestionFullDetails(int SchoolId, int territoryId = 0, int levelId = 0, int classificationId = 0,
                                                                    int genderId = 0, int satisfactoryId = 0)
        {
            string sql = string.Format("select * from vwQuestions");
            string Condition = " where ", ConditionDetails = null;
            if (SchoolId != 0)
            {
                if (!string.IsNullOrEmpty(ConditionDetails))
                    ConditionDetails += " and ";
                ConditionDetails += string.Format(" SchoolNo = {0}", SchoolId);
            }
            if (territoryId != 0)
            {
                if (!string.IsNullOrEmpty(ConditionDetails))
                    ConditionDetails += " and ";
                ConditionDetails += string.Format(" TerritoryNo = {0}", territoryId);
            }
            if (levelId != 0)
            {
                if (!string.IsNullOrEmpty(ConditionDetails))
                    ConditionDetails += " and ";
                ConditionDetails += string.Format(" LevelNo = {0}", levelId);
            }
            if (classificationId != 0)
            {
                if (!string.IsNullOrEmpty(ConditionDetails))
                    ConditionDetails += " and ";
                ConditionDetails += string.Format(" ClassificationNo = {0}", classificationId);
            }
            if (genderId != 0)
            {
                if (!string.IsNullOrEmpty(ConditionDetails))
                    ConditionDetails += " and ";
                ConditionDetails += string.Format(" GenderNo = {0}", genderId);
            }
            if (satisfactoryId != 0)
            {
                if (!string.IsNullOrEmpty(ConditionDetails))
                    ConditionDetails += " and ";
                ConditionDetails += string.Format(" SchoolSatisfactoryNo = {0}", satisfactoryId);
            }
            if (!string.IsNullOrEmpty(ConditionDetails))
                sql += Condition + ConditionDetails;
            sql += " order by SchoolNameAr asc";

            return await _sqlData.LoadDataQuery<QuestionFullDetailsModel>(sql);
        }

    }
}
