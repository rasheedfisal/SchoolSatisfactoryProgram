using Microsoft.AspNetCore.Mvc.Rendering;
using School.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL.Extensions
{
    public static class ConvertExtenstion
    {
        public static List<SelectListItem> ConvertSchoolToList<T>(this IEnumerable<T> collection, int selectedValue, string Lang) where T:ISchoolModel
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = Lang == "en" ? item.SchoolNameEn : item.SchoolNameAr,
                        Value = item.SchoolId.ToString(),
                        Selected = (item.SchoolId == selectedValue)
                    }
                ).ToList();
        }
        public static List<SelectListItem> ConvertSchoolAllToList<T>(this IEnumerable<T> collection, int selectedValue, string Lang) where T : ISchool
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = Lang == "en" ? item.NameEn : item.NameAr,
                        Value = item.id.ToString(),
                        Selected = (item.id == selectedValue)
                    }
                ).ToList();
        }
        public static List<SelectListItem> ConvertClassificationToList<T>(this IEnumerable<T> collection, int selectedValue, string Lang) where T : IClassificationModel
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = Lang == "en" ? item.ClassificationNameEn : item.ClassificationNameAr,
                        Value = item.id.ToString(),
                        Selected = (item.id == selectedValue)
                    }
                ).ToList();
        }
        public static List<SelectListItem> ConvertGenderToList<T>(this IEnumerable<T> collection, int selectedValue, string Lang) where T : IGenderModel
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = Lang == "en" ? item.GenderNameEn : item.GenderNameAr,
                        Value = item.id.ToString(),
                        Selected = (item.id == selectedValue)
                    }
                ).ToList();
        }

        public static List<SelectListItem> ConvertLevelToList<T>(this IEnumerable<T> collection, int selectedValue, string Lang) where T : ILevelModel
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = Lang == "en" ? item.LevelNameEn : item.LevelNameAr,
                        Value = item.id.ToString(),
                        Selected = (item.id == selectedValue)
                    }
                ).ToList();
        }
        public static List<SelectListItem> ConvertTerritoryToList<T>(this IEnumerable<T> collection, int selectedValue, string Lang) where T : ITerritoryModel
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = Lang == "en" ? item.TerritoryNameEn : item.TerritoryNameAr,
                        Value = item.id.ToString(),
                        Selected = (item.id == selectedValue)
                    }
                ).ToList();
        }
        public static List<SelectListItem> ConvertSatisfactoryToList<T>(this IEnumerable<T> collection, int selectedValue, string Lang) where T : ISatisfactoryRateModel
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = Lang == "en" ? item.RateNameEn : item.RateNameAr,
                        Value = item.id.ToString(),
                        Selected = (item.id == selectedValue)
                    }
                ).ToList();
        }
    }
}
