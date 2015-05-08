using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class DBHelper
    {

        public static string GetEntityValidationExceptionMessage(DbEntityValidationException ex)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var errors in ex.EntityValidationErrors)
            {
                sb.AppendFormat("{0} failed validation\n", errors.Entry.Entity.GetType());
                foreach (var error in errors.ValidationErrors)
                {
                    sb.AppendLine("PropertyName=" + error.PropertyName + "; ErrMessage=" + error.ErrorMessage);
                }
            }

            return sb.ToString();
        }

        public static string GetSqlExceptionMessage(DbUpdateException ex)
        {
            UpdateException updateException = (UpdateException)ex.InnerException;
            SqlException sqlException = (SqlException)updateException.InnerException;
            StringBuilder sb = new StringBuilder();
            foreach (SqlError error in sqlException.Errors)
            {
                sb.AppendLine("ErrMessage.=" + error.Message + "; ErrNumber.=" + error.Number.ToString());
            }

            return sb.ToString();
        }




    }
}
