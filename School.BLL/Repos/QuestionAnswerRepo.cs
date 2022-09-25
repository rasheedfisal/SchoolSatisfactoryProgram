using Dapper;
using School.DAL;
using School.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL.Repos
{
    public class QuestionAnswerRepo : IQuestionAnswerRepo
    {
        private readonly ISqlDataAccess _sqlData;

        public QuestionAnswerRepo(ISqlDataAccess sqlData)
        {
            _sqlData = sqlData;
        }

        public async Task<bool> InsertQuestionWithAnswer(SchoolQuestionModel questionModel)
        {
            try
            {
                _sqlData.StartTransaction();
                var parameters = new DynamicParameters();
                parameters.Add("@QuestionNameEn", questionModel.QuestionNameEn, DbType.String);
                parameters.Add("@QuestionNameAr", questionModel.QuestionNameAr, DbType.String);
                parameters.Add("@SchoolNo", questionModel.SchoolNo, DbType.Int32);
                parameters.Add("@SchoolSatisfactoryNo", questionModel.SchoolSatisfactoryNo, DbType.Int32);

                string sql = "insert into tbl_SchoolQuestion(QuestionNameEn,QuestionNameAr,SchoolNo,SchoolSatisfactoryNo) " +
                    "values(@QuestionNameEn,@QuestionNameAr,@SchoolNo,@SchoolSatisfactoryNo)" +
                    "select SCOPE_IDENTITY()";
                int questionId = await _sqlData.SaveDataInTransactionQuery<int, dynamic>(sql, parameters);
                if (questionModel.QuestionAnswers != null)
                {
                    if (questionId != 0)
                    {
                        foreach (var item in questionModel.QuestionAnswers)
                        {
                            var Qparameters = new DynamicParameters();
                            Qparameters.Add("@SchoolQuestionNo", questionId, DbType.Int32);
                            Qparameters.Add("@AnswerNameEn", item.AnswerNameEn, DbType.String);
                            Qparameters.Add("@AnswerNameAr", item.AnswerNameAr, DbType.String);

                            string Qsql = "insert into tbl_SchoolQuestionAnswers(SchoolQuestionNo,AnswerNameEn,AnswerNameAr) " +
                                "values(@SchoolQuestionNo,@AnswerNameEn,@AnswerNameAr)";
                            await _sqlData.SaveDataInTransactionWithoutReturnQuery<int, dynamic>(Qsql, Qparameters);

                        }
                    }

                }
                _sqlData.CommitTransaction();
                return true;
            }
            catch
            {
                _sqlData.RollBackTransaction();
                return false;
            }

        }

        public async Task<bool> UpdateQuestionWithAnswer(SchoolQuestionModel questionModel)
        {
            try
            {
                _sqlData.StartTransaction();
                var parameters = new DynamicParameters();
                parameters.Add("@id", questionModel.id, DbType.Int32);
                parameters.Add("@QuestionNameEn", questionModel.QuestionNameEn, DbType.String);
                parameters.Add("@QuestionNameAr", questionModel.QuestionNameAr, DbType.String);
                parameters.Add("@SchoolNo", questionModel.SchoolNo, DbType.Int32);
                parameters.Add("@SchoolSatisfactoryNo", questionModel.SchoolSatisfactoryNo, DbType.Int32);

                string sql = "update tbl_SchoolQuestion set QuestionNameEn=@QuestionNameEn,QuestionNameAr=@QuestionNameAr,SchoolNo=@SchoolNo,SchoolSatisfactoryNo=@SchoolSatisfactoryNo " +
                    "where id=@id";
                await _sqlData.SaveDataInTransactionWithoutReturnQuery<int, dynamic>(sql, parameters);
                if (questionModel.QuestionAnswers != null)
                {
                    var Dparameters = new DynamicParameters();
                    Dparameters.Add("@SchoolQuestionNo", questionModel.id, DbType.Int32);
                    string sqldelete = "delete from tbl_SchoolQuestionAnswers where SchoolQuestionNo=@SchoolQuestionNo";
                    await _sqlData.SaveDataInTransactionWithoutReturnQuery<int, dynamic>(sqldelete, Dparameters);

                    foreach (var item in questionModel.QuestionAnswers)
                    {
                        var Qparameters = new DynamicParameters();
                        Qparameters.Add("@SchoolQuestionNo", questionModel.id, DbType.Int32);
                        Qparameters.Add("@AnswerNameEn", item.AnswerNameEn, DbType.String);
                        Qparameters.Add("@AnswerNameAr", item.AnswerNameAr, DbType.String);

                        string Qsql = "insert into tbl_SchoolQuestionAnswers(SchoolQuestionNo,AnswerNameEn,AnswerNameAr) " +
                            "values(@SchoolQuestionNo,@AnswerNameEn,@AnswerNameAr)";
                        await _sqlData.SaveDataInTransactionWithoutReturnQuery<int, dynamic>(Qsql, Qparameters);
                    }
                }
                _sqlData.CommitTransaction();
                return true;
            }
            catch
            {
                _sqlData.RollBackTransaction();
                return false;
            }

        }

        public async Task<int> InsertQuestion(SchoolQuestionModel questionModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@QuestionNameEn", questionModel.QuestionNameEn, DbType.String);
            parameters.Add("@QuestionNameAr", questionModel.QuestionNameAr, DbType.String);
            parameters.Add("@SchoolNo", questionModel.SchoolNo, DbType.Int32);
            parameters.Add("@SchoolSatisfactoryNo", questionModel.SchoolSatisfactoryNo, DbType.Int32);

            string sql = "insert into tbl_SchoolQuestion(QuestionNameEn,QuestionNameAr,SchoolNo,SchoolSatisfactoryNo) " +
                "output inserted.id" +
                "values(@QuestionNameEn,@QuestionNameAr,@SchoolNo,@SchoolSatisfactoryNo)";
            int questionId = await _sqlData.SaveDataQuery<int, dynamic>(sql, parameters);
            return questionId;
        }

        public async Task<bool> UpdateQuestion(SchoolQuestionModel questionModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", questionModel.id, DbType.Int32);
            parameters.Add("@QuestionNameEn", questionModel.QuestionNameEn, DbType.String);
            parameters.Add("@QuestionNameAr", questionModel.QuestionNameAr, DbType.String);
            parameters.Add("@SchoolNo", questionModel.SchoolNo, DbType.Int32);
            parameters.Add("@SchoolSatisfactoryNo", questionModel.SchoolSatisfactoryNo, DbType.Int32);

            string sql = "update tbl_SchoolQuestion set QuestionNameEn=@QuestionNameEn,QuestionNameAr=@QuestionNameAr,SchoolNo=@SchoolNo,SchoolSatisfactoryNo=@SchoolSatisfactoryNo where id=@id";
            return await _sqlData.SaveDataQuery<dynamic>(sql, parameters);
        }
        public async Task<bool> DeleteQuestion(int questionId)
        {
            string sql = "delete from tbl_SchoolQuestion where id=@id";
            return await _sqlData.SaveDataQuery<dynamic>(sql, new { id = questionId });
        }

        public async Task<int> InsertQuestionAnswer(SchoolQuestionAnswerModel answerModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SchoolQuestionNo", answerModel.SchoolQuestionNo, DbType.Int32);
            parameters.Add("@AnswerNameEn", answerModel.AnswerNameEn, DbType.String);
            parameters.Add("@AnswerNameAr", answerModel.AnswerNameAr, DbType.String);

            string sql = "insert into tbl_SchoolQuestionAnswers(SchoolQuestionNo,AnswerNameEn,AnswerNameAr) " +
                "values(@SchoolQuestionNo,@AnswerNameEn,@AnswerNameAr)" +
            "select SCOPE_IDENTITY()";
            int questionId = await _sqlData.SaveDataQuery<int, dynamic>(sql, parameters);
            return questionId;
        }
        public async Task<bool> UpdateQuestionAnswer(SchoolQuestionAnswerModel answerModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", answerModel.id, DbType.Int32);
            parameters.Add("@SchoolQuestionNo", answerModel.SchoolQuestionNo, DbType.Int32);
            parameters.Add("@AnswerNameEn", answerModel.AnswerNameEn, DbType.String);
            parameters.Add("@AnswerNameAr", answerModel.AnswerNameAr, DbType.String);

            string sql = "update tbl_SchoolQuestionAnswers set SchoolQuestionNo=@SchoolQuestionNo,AnswerNameEn=@AnswerNameEn,AnswerNameAr=@AnswerNameAr where id=@id";
            return await _sqlData.SaveDataQuery<dynamic>(sql, parameters);
        }
        public async Task<bool> DeleteQuestionAnswer(int answerId)
        {
            string sql = "delete from tbl_SchoolQuestionAnswers where id=@id";
            return await _sqlData.SaveDataQuery<dynamic>(sql, new { });
        }

        public async Task<List<SchoolQuestionModel>> GetSchoolQuestion(int SchoolNo, int RateNo)
        {
            //List<SchoolQuestionModel> schoolQuestions = new List<SchoolQuestionModel>();
            List<SchoolQuestionModel> schoolQuestions;
            string sql = $"select * from tbl_SchoolQuestion where SchoolNo={SchoolNo} and SchoolSatisfactoryNo={RateNo}";
            schoolQuestions = await _sqlData.LoadDataQuery<SchoolQuestionModel>(sql);
            if (schoolQuestions != null && schoolQuestions.Count > 0)
            {
                foreach (var item in schoolQuestions)
                {
                    var sql2 = $"select * from tbl_SchoolQuestionAnswers where SchoolQuestionNo={item.id}";
                    var answers = await _sqlData.LoadDataQuery<SchoolQuestionAnswerModel>(sql2);
                    item.QuestionAnswers = answers;
                }
                return schoolQuestions;
            }
            return new List<SchoolQuestionModel>();
        }

        public async Task<List<QuestionAnswerFullDetails>> GetSelectedQuestionDetails(int qId)
        {
            string sql = $"select * from vwSchoolQuestionAnswers where SchoolQuestionNo={qId}";
            var schoolQuestion = await _sqlData.LoadDataQuery<QuestionAnswerFullDetails>(sql);

            return schoolQuestion;
        }

        public async Task<bool> InsertClientSatisfactoryAnswer(List<ClientAnswerModel> answerModel, SchoolSatisfactoryModel school)
        {
            try
            {
                _sqlData.StartTransaction();

                var Qparameters = new DynamicParameters();
                Qparameters.Add("@SchoolNo", school.SchoolNo, DbType.Int32);
                Qparameters.Add("@SatisfactoryRateNo", school.SatisfactoryRateNo, DbType.Int32);
                Qparameters.Add("@ClientName", school.ClientName, DbType.String);
                Qparameters.Add("@ClientQualification", school.ClientQualification, DbType.String);
                Qparameters.Add("@Others", school.Others, DbType.String);
                string Qsql = "insert into tbl_SchoolSatisfactoryRate(SchoolNo,SatisfactoryRateNo,ClientName,ClientQualification,Others) " +
                    "values(@SchoolNo,@SatisfactoryRateNo,@ClientName,@ClientQualification,@Others) " +
                    "select SCOPE_IDENTITY()";
                int schoolSatisfactoryNo = await _sqlData.SaveDataInTransactionQuery<int, dynamic>(Qsql, Qparameters);

                foreach (var item in answerModel)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@QuestionNo", item.QuestionNo, DbType.Int32);
                    parameters.Add("@AnswerNo", item.AnswerNo, DbType.Int32);
                    parameters.Add("@SchoolSatisfactoryRateNo", schoolSatisfactoryNo, DbType.Int32);


                    string sql = "insert into tbl_ClientSatisfactoryAnswers(QuestionNo,AnswerNo,SchoolSatisfactoryRateNo) " +
                        "values(@QuestionNo,@AnswerNo,@SchoolSatisfactoryRateNo)";
                    await _sqlData.SaveDataInTransactionWithoutReturnQuery<ClientAnswerModel, dynamic>(sql, parameters);
                }

                _sqlData.CommitTransaction();
                return true;
            }
            catch
            {
                _sqlData.RollBackTransaction();
                throw;
            }
            finally
            {
                _sqlData.Dispose();
            }

        }

        public async Task<List<QuestionAnswerFullDetails>> GetSchoolRateStatistic(int SchoolId, string dateFrom = null, string dateTo = null)
        {
            try
            {
                string sql = string.Format("select count(SatisfactoryRateNo) as SatisfactoryRateId, RateNameAr from vwSchoolRating");
                string Condition = " where ";
                string? ConditionDetails = null;
                if (SchoolId != 0)
                {
                    if (!string.IsNullOrEmpty(ConditionDetails))
                        ConditionDetails += " and ";
                    ConditionDetails += string.Format(" SchoolNo = {0}", SchoolId);
                }

                if (!string.IsNullOrEmpty(dateFrom))
                {
                    if (!string.IsNullOrEmpty(ConditionDetails))
                        ConditionDetails += " and ";
                    ConditionDetails += string.Format(" DateAdded >= '{0}'", dateFrom);
                }
                if (!string.IsNullOrEmpty(dateTo))
                {
                    if (!string.IsNullOrEmpty(ConditionDetails))
                        ConditionDetails += " and ";

                    ConditionDetails += string.Format(" DateAdded <= '{0}'", dateTo);
                }
                if (!string.IsNullOrEmpty(ConditionDetails))
                    sql += Condition + ConditionDetails;
                sql += " group by SatisfactoryRateNo,RateNameAr";
                return await _sqlData.LoadDataQuery<QuestionAnswerFullDetails>(sql);
            }
            catch
            {

                throw;
            }

        }

        public async Task<List<ClientSchoolSurvayModel>> GetClientSchoolSurvay(int SchoolId, int territoryId = 0, int levelId = 0, int classificationId = 0,
                                                                    int genderId = 0, int satisfactoryId = 0, string dateFrom = null, string dateTo = null)
        {
            try
            {
                string sql = string.Format("select count(id) as id, SchoolNameAr  from vwClientSchoolsSurvay");
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
                    ConditionDetails += string.Format(" SatisfactoryRateNo = {0}", satisfactoryId);
                }
                if (!string.IsNullOrEmpty(dateFrom))
                {
                    if (!string.IsNullOrEmpty(ConditionDetails))
                        ConditionDetails += " and ";
                    ConditionDetails += string.Format(" DateAdded >= '{0}'", dateFrom);
                }
                if (!string.IsNullOrEmpty(dateTo))
                {
                    if (!string.IsNullOrEmpty(ConditionDetails))
                        ConditionDetails += " and ";
                    ConditionDetails += string.Format(" DateAdded <= '{0}'", dateTo);
                }
                if (!string.IsNullOrEmpty(ConditionDetails))
                    sql += Condition + ConditionDetails;
                sql += " group by SchoolNameAr";
                return await _sqlData.LoadDataQuery<ClientSchoolSurvayModel>(sql);
            }
            catch
            {

                throw;
            }

        }
    }
}
