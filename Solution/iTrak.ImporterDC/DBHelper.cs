using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace iTrak.Importer.Data
{
    public class DbHelper
    {
        private static string _iTrakConnectionString = string.Empty;
        public static string iTrakConnectionString
        {
            get
            {
                if (_iTrakConnectionString != string.Empty)
                    return _iTrakConnectionString;
                else
                {

                    if (ConfigurationManager.ConnectionStrings["iTrak"] != null)
                        _iTrakConnectionString = ConfigurationManager.ConnectionStrings["iTrak"].ToString();
                    else
                        _iTrakConnectionString = iView.iTrak.iTrakCommon.Database.ConnectionString;
                   return _iTrakConnectionString;
                }
            }
        }
    }
}
