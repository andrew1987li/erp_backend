using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using jIAnSoft.Framework.Database;
using Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters;
using Tw.Com.Kooco.Admin.Misc.Definition;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Providers {
    internal static class LiteratureTableProvider {
        public static DataSet List(LiteratureParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.DataSet(
                    CommandType.StoredProcedure,
                    "[dbo].[sp_FetchLiteratureList_Sel]",
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

        public static int Delete(LiteratureParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.Write(
                    CommandType.Text,
                    "DELETE FROM [dbo].[Literature] WHERE [LiteratureId] = @LiteratureId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.LiteratureId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@LiteratureId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static int Create(LiteratureParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.Write(
                    CommandType.Text,
                    "INSERT INTO [dbo].[Literature]([TypesOfCancer],[LiteratureTitel],[HyperLink],[PublishDate],[CreateTime])VALUES(@TypesOfCancer,@LiteratureTitel,@HyperLink,@PublishDate,GETUTCDATE());",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.TypesOfCancer??"",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@TypesOfCancer",
                            Size = 256,
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.LiteratureTitel??"",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@LiteratureTitel",
                            Size = 1024,
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.HyperLink??"",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@HyperLink",
                            Size = 512,
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.PublishDate,
                            SqlDbType = SqlDbType.Date,
                            ParameterName = "@PublishDate",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static int Update(LiteratureParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.Write(
                    CommandType.Text,
                    "UPDATE [dbo].[Literature] SET [TypesOfCancer]=@TypesOfCancer,[LiteratureTitel]=@LiteratureTitel,[HyperLink]=@HyperLink,[PublishDate]=@PublishDate,[UpdateTime]=GETUTCDATE() WHERE [LiteratureId] = @LiteratureId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.LiteratureId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@LiteratureId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.TypesOfCancer ?? "",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@TypesOfCancer",
                            Size = 256,
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.LiteratureTitel ?? "",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@LiteratureTitel",
                            Size = 1024,
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.HyperLink ?? "",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@HyperLink",
                            Size = 512,
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.PublishDate,
                            SqlDbType = SqlDbType.Date,
                            ParameterName = "@PublishDate",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static StringDictionary Detail(LiteratureParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.First(
                    CommandType.Text,
                    "SELECT [LiteratureId],[TypesOfCancer],[literatureTitel],[publishDate],[HyperLink],[CreateTime] FROM [dbo].[Literature] WITH(READUNCOMMITTED) WHERE [LiteratureId] = @LiteratureId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.LiteratureId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@LiteratureId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }
    }
}
