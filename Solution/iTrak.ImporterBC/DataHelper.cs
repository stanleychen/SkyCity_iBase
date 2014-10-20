using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Configuration;
namespace iTrak.Importer.Common
{
    public class DataHelper
    {
        public const int TestingRows = 1000;
        public static DateTime SQL_MIN_DATE = new DateTime(1753, 1, 1);

        #region Source Data Methods
        #region App Settings
        public static bool IsTesting()
        {
            string testing = ConfigurationManager.AppSettings["Testing"].ToString();
            if (testing.ToUpper() == "TRUE")
                return true;
            else
                return false;
        }
        #endregion

        #region Get Number
        public static string GetBlotterNumber(string sourceNumber)
        {
            return "~DL" + sourceNumber;
        }
        public static string GetIncidentNumber(string sourceNumber)
        {
            return "~IN" + sourceNumber;
        }
        #endregion

        #endregion

        #region ADO.NET Data Helper
        public static bool GetBool(object objValue)
        {
            if (objValue == System.DBNull.Value)
                return false;
            else
                return (bool)objValue;
        }
        public static DateTime GetDateTime(object objValue)
        {
            if (objValue == System.DBNull.Value)
                return DateTime.MinValue;
            else
                return (DateTime)objValue;
        }
        public static Guid GetGuid(object objValue)
        {
            Guid tempValue = Guid.Empty;
            if (objValue != System.DBNull.Value)
            {
                if(objValue.GetType() == typeof(Guid))
                    tempValue = (Guid)objValue;
                else
                    tempValue = new Guid(objValue.ToString());
            }
            return tempValue;
        }
        #endregion

        #region iTrak Data Methods
        public static string GetUserName(string sourceUserName)
        {
            if (string.IsNullOrEmpty(sourceUserName))
                return string.Empty;

            string tempString = sourceUserName;
            if (tempString.Length > 16)
                tempString = tempString.Substring(0, 16);

            return tempString;
        }

        #region GetMediaType
        public static string GetMediaType(string sourceMediaType)
        {
            string tempType = string.Empty;
            sourceMediaType = sourceMediaType.ToLower().Trim();
            if (sourceMediaType == "image/pjpeg")
                tempType = "jpg";
            else if (sourceMediaType == "image/bmp")
                tempType = "bmp";
            else if (sourceMediaType == "application/msword")
                tempType = "doc";
            else
                tempType = sourceMediaType;

            return tempType;
        }
        #endregion

        #region IsImage
        public static bool IsImage(string mediaType)
        {
            if (mediaType == "jpg" || mediaType == "gif" || mediaType == "bmp" || mediaType == "jpeg" || mediaType == "tif")
                return true;
            else
                return false;
        }
        #endregion

        #region GetAgeRanges
        public static void GetAgeRanges(DateTime dateOfBirth, out int lowerAge, out int upperAge)
        {

            int age = DateTime.Now.Year - dateOfBirth.Year;
            lowerAge = 0;
            upperAge = 0;
            if (age < 20)
            {
                lowerAge = 0;
                upperAge = 19;
            }
            else if (age < 30)
            {
                lowerAge = 20;
                upperAge = 29;
            }
            else if (age < 40)
            {
                lowerAge = 30;
                upperAge = 39;
            }
            else if (age < 50)
            {
                lowerAge = 40;
                upperAge = 49;
            }
            else if (age < 60)
            {
                lowerAge = 50;
                upperAge = 59;
            }
            else if (age < 70)
            {
                lowerAge = 60;
                upperAge = 69;
            }
            else if (age < 80)
            {
                lowerAge = 70;
                upperAge = 79;
            }
            else
            {
                lowerAge = 80;
                upperAge = 89;
            }

        }
        #endregion

        #endregion
    }
}
