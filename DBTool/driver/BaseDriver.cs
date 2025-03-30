using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTool.driver
{
    public abstract class BaseDriver
    {
        protected void WriteLog(string message, string tag = "LOG")
        {
            Debug.WriteLine(message, tag);
        }

        public abstract bool Exists(string strSql);

        public abstract int Execute(string SQLString);

        public abstract void ExecuteTran(List<string> SQLStringList);

        public abstract object? GetSingle(string SQLString);

        public abstract DataTable Query(string SQLString);
    }
}
