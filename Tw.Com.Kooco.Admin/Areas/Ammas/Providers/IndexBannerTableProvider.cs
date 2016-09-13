using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using jIAnSoft.Framework.Database;
using Tw.Com.Kooco.Admin.Areas.Ammas.Entitys;
using Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters;
using Tw.Com.Kooco.Admin.Misc.Definition;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Providers {
    internal static class IndexBannerTableProvider {
        public static DataSet List(IndexBannerParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.DataSet(
                    CommandType.StoredProcedure,
                    "[dbo].[sp_FetchIndexBannerList_Sel]",
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

        public static IndexBanner Detail(IndexBannerParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                var data = new IndexBanner();
                data.Fill(
                    db.First(
                        CommandType.Text,
                        "SELECT [IndexBannerId],[ImgPath],[FirstString],[FirstStringColor],[SecondString],[SecondStringColor],[ThreeString],[ThreeStringColor],[Align],[Link],[CreateTime] FROM [dbo].[IndexBanner] WITH(READUNCOMMITTED) WHERE [IndexBannerId] = @IndexBannerId;",
                        new DbParameter[] {
                            new SqlParameter {
                                Value = param.Entity.IndexBannerId,
                                SqlDbType = SqlDbType.Int,
                                ParameterName = "@IndexBannerId",
                                Direction = ParameterDirection.Input
                            }
                        }));
                return data;
            }
        }

        public static int Delete(IndexBannerParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.Write(
                    CommandType.Text,
                    " DELETE FROM [dbo].[IndexBanner] WHERE [IndexBannerId] = @IndexBannerId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.IndexBannerId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@IndexBannerId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static int Update(IndexBannerParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.Write(
                    CommandType.Text,
                    "UPDATE [dbo].[IndexBanner] SET [ImgPath] = @ImgPath,[Link] = @Link, [Align]=@Align,[FirstString] = @FirstString,[FirstStringColor] = @FirstStringColor,[SecondString] = @SecondString,[SecondStringColor] = @SecondStringColor,[ThreeString] = @ThreeString,[ThreeStringColor] = @ThreeStringColor WHERE [IndexBannerId] = @IndexBannerId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.IndexBannerId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@IndexBannerId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.FirstString,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@FirstString",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.FirstStringColor,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@FirstStringColor",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.SecondString,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@SecondString",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.SecondStringColor,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@SecondStringColor",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.ThreeString,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ThreeString",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.ThreeStringColor,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ThreeStringColor",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.Align,
                            SqlDbType = SqlDbType.VarChar,
                            ParameterName = "@Align",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.Link,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Link",
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

        public static int Create(IndexBannerParameter param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.Write(
                    CommandType.Text,
                    "INSERT INTO [dbo].[IndexBanner]([ImgPath],[FirstString],[FirstStringColor],[SecondString],[SecondStringColor],[ThreeString],[ThreeStringColor],[Align],[Link],[CreateTime])VALUES(@ImgPath,@FirstString,@FirstStringColor,@SecondString,@SecondStringColor,@ThreeString,@ThreeStringColor,@Align,@Link,GETUTCDATE());",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.FirstString,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@FirstString",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.FirstStringColor,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@FirstStringColor",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.SecondString,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@SecondString",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.SecondStringColor,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@SecondStringColor",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.ThreeString,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ThreeString",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.ThreeStringColor,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ThreeStringColor",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.Align,
                            SqlDbType = SqlDbType.VarChar,
                            ParameterName = "@Align",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.Link,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Link",
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
