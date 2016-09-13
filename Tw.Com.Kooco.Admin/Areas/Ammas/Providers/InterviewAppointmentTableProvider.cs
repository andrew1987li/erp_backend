using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using jIAnSoft.Framework.Database;
using Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters;
using Tw.Com.Kooco.Admin.Misc.Definition;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Providers {
    internal static class InterviewAppointmentTableProvider {
        public static DataSet List(InterviewAppointmentParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.DataSet(
                    CommandType.StoredProcedure,
                    "[dbo].[sp_FetchInterviewAppointmentList_Sel]",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.KeyWord,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@argStrKeyWord",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = string.IsNullOrEmpty(param.StartUtcDateTime) ? null : param.StartUtcDateTime,
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@argDteStart",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = string.IsNullOrEmpty(param.EndUtcDateTime) ? null : param.EndUtcDateTime,
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@argDteEnd",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Page,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@argIntPage",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.PageSize,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@argIntPageSize",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static int Delete(InterviewAppointmentParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.Write(
                    CommandType.Text,
                    "DELETE FROM [dbo].[InterviewAppointment] WHERE [InterviewAppointmentId] = @InterviewAppointmentId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.InterviewAppointmentId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@InterviewAppointmentId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }
    }
}
