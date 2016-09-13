using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using jIAnSoft.Framework.Database;
using Tw.Com.Kooco.Admin.Areas.Ammas.Entitys;
using Tw.Com.Kooco.Admin.Misc.Definition;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Providers {
    public static class OurGoalTableProvider {
        public static OurGoal Detail() {
            using (var db = new MsSql(DbName.Official)) {
                var data = new OurGoal();
                data.Fill(
                    db.First(
                        CommandType.Text,
                        "SELECT Top 1 [OurGoalId],[LeftImg],[RightImg] FROM [dbo].[OurGoal] WITH(READUNCOMMITTED);",
                        new DbParameter[] { }));
                return data;
            }
        }

        public static int Update(OurGoal param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.Write(
                    CommandType.Text,
                    "UPDATE [dbo].[OurGoal] SET [LeftImg] = @LeftImg,[RightImg] = @RightImg  WHERE [OurGoalId] = @OurGoalId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.LeftImg,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@LeftImg",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.RightImg,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@RightImg",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.OurGoalId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@OurGoalId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static int Create(OurGoal param) {
            using (var db = new MsSql(DbName.Official)) {
                return db.One<int>(
                    CommandType.Text,
                    "INSERT INTO [dbo].[OurGoal]([LeftImg],[RightImg])VALUES(@LeftImg,@RightImg);SELECT @@IDENTITY;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.LeftImg ?? "",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@LeftImg",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.RightImg ?? "",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@RightImg",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.OurGoalId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@OurGoalId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }
    }
}