using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using jIAnSoft.Framework.Database;
using Tw.Com.Kooco.Admin.Areas.Ammas.Entitys;
using Tw.Com.Kooco.Admin.Misc.Definition;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Providers
{
    public static class PartnerTableProvider
    {
        public static Partner Detail()
        {
            using (var db = new MsSql(DbName.Official))
            {
                var data = new Partner();
                data.Fill(
                    db.First(
                        CommandType.Text,
                        "SELECT Top 1 [PartnerId],[ImgPath] FROM [dbo].[Partner] WITH(READUNCOMMITTED);",
                        new DbParameter[] { }));
                return data;
            }
        }

        public static int Update(Partner param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "UPDATE [dbo].[Partner] SET [ImgPath] = @ImgPath WHERE [PartnerId] = @PartnerId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.ImgPath,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ImgPath",
                            Direction = ParameterDirection.Input
                        },                       
                        new SqlParameter {
                            Value = param.PartnerId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@PartnerId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static int Create(Partner param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.One<int>(
                    CommandType.Text,
                    "INSERT INTO [dbo].[Partner]([ImgPath])VALUES(@ImgPath);SELECT @@IDENTITY;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.ImgPath ?? "",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ImgPath",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.PartnerId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@PartnerId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }
    }
}