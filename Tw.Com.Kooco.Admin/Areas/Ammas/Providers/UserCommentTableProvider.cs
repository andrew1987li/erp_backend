using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using jIAnSoft.Framework.Database;
using Tw.Com.Kooco.Admin.Areas.Ammas.Entitys;
using Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters;
using Tw.Com.Kooco.Admin.Misc.Definition;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Providers {
    internal static class UserCommentTableProvider {
        public static DataSet List(UserCommentParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.DataSet(
                    CommandType.StoredProcedure,
                    "[dbo].[sp_FrontFetchUserCommentList_Sel]",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.KeyWord,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@argStrKeyWord",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = string.IsNullOrEmpty(param.StartUtcDateTime)?null:param.StartUtcDateTime,
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@argDteStart",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = string.IsNullOrEmpty(param.EndUtcDateTime)?null:param.EndUtcDateTime,
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

        public static UserComment Detail(UserCommentParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                var data = new UserComment();
                data.Fill(
                    db.First(
                        CommandType.Text,
                        "SELECT [UserCommentId],[Name],[Title],[Comment],[ImgPath],[CreateTime] FROM [dbo].[UserComment] WITH(READUNCOMMITTED) WHERE [UserCommentId] = @UserCommentId;",
                        new DbParameter[] {
                            new SqlParameter {
                                Value = param.Entity.UserCommentId,
                                SqlDbType = SqlDbType.Int,
                                ParameterName = "@UserCommentId",
                                Direction = ParameterDirection.Input
                            }
                        }));
                return data;
            }
        }


        public static int Delete(UserCommentParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.Write(CommandType.Text,
                    "DELETE FROM [dbo].[UserComment] WHERE [UserCommentId] = @UserCommentId;", new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.UserCommentId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@UserCommentId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        //Update
        public static int Update(UserCommentParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.Write(
                    CommandType.Text,
                    "UPDATE [dbo].[UserComment] SET [Name] = @Name,[Title] = @Title,[ImgPath] = @ImgPath,[Comment] = @Comment WHERE [UserCommentId] = @UserCommentId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.UserCommentId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@UserCommentId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.Name,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Name",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.Title,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Title",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.Comment,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Comment",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.ImgPath,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ImgPath",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static int Create(UserCommentParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.Write(
                    CommandType.Text,
                    "INSERT INTO [dbo].[UserComment]([Name],[Title],[Comment],[ImgPath],[CreateTime])VALUES(@Name,@Title,@Comment,@ImgPath,GETUTCDATE());",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.Name,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Name",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.Title,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Title",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.Comment,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Comment",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.ImgPath,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ImgPath",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

    }
}
