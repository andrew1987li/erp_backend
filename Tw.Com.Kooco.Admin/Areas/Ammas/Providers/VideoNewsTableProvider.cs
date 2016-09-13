using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using jIAnSoft.Framework.Database;
using Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters;
using Tw.Com.Kooco.Admin.Misc.Definition;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Providers
{
    internal static class VideoNewsTableProvider
    {

        public static int Create(VideoNewsParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "INSERT INTO [dbo].[VideoNews]([Sort],[Type],[Link],[ImgPath],[IsTop],[Title],[TextBody],[StartDate],[EndDate],[Status],[CreateTime],[UpdateTime])VALUES(@Sort,@Type,@Link,@ImgPath,@IsTop,@Title,@TextBody,@StartDate,@EndDate,@Status,GETUTCDATE(),GETUTCDATE());",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.VideoNews.Sort,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@Sort",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.StartDate,
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@StartDate",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.EndDate,
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@EndDate",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.Status,
                            SqlDbType = SqlDbType.TinyInt,
                            ParameterName = "@Status",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.Type,
                            SqlDbType = SqlDbType.TinyInt,
                            ParameterName = "@Type",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.Link,
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 256,
                            ParameterName = "@Link",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.ImgPath,
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 512,
                            ParameterName = "@ImgPath",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.IsTop ? 1 : 0,
                            SqlDbType = SqlDbType.Bit,
                            ParameterName = "@IsTop",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.Title,
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 128,
                            ParameterName = "@Title",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.TextBody,
                            SqlDbType = SqlDbType.NVarChar,
                            Size = -1,
                            ParameterName = "@TextBody",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static int Delete(VideoNewsParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(CommandType.Text, " DELETE FROM [dbo].[VideoNews] WHERE VideoNewsId = @VideoNewsId;", new DbParameter[] {
                        new SqlParameter {
                            Value = param.VideoNews.VideoNewsId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@VideoNewsId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static StringDictionary Detail(VideoNewsParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.First(
                    CommandType.Text,
                    "SELECT [VideoNewsId],[Sort], [Type],[ImgPath], [Link],[IsTop],[Title],  [TextBody], [StartDate],[EndDate],[CreateTime],[UpdateTime],[Status]FROM [dbo].[VideoNews] WITH(READUNCOMMITTED)WHERE [VideoNewsId] = @VideoNewsId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.VideoNews.VideoNewsId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@VideoNewsId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static DataSet List(VideoNewsParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(
                    CommandType.StoredProcedure,
                    "[dbo].[sp_VideoNewsList_Sel]",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.KeyWord,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@argStrKeyWord",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.StartUtcDateTime,
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@argDteStart",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.EndUtcDateTime,
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
                        },
                        new SqlParameter {
                            Value = param.VideoNewsType,
                            SqlDbType = SqlDbType.TinyInt,
                            ParameterName = "@argIntType",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        //Update
        public static int Update(VideoNewsParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "  UPDATE [dbo].[VideoNews]SET [Sort] = @Sort,[Type] = @Type,[ImgPath] = @ImgPath,[Link] = @Link,[IsTop] = @IsTop,[Title] = @Title,[TextBody] = @TextBody,[StartDate] = @StartDate,[EndDate] = @EndDate,[UpdateTime] = GETUTCDATE(),[Status] = @Status  WHERE [VideoNewsId] = @VideoNewsId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.VideoNews.VideoNewsId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@VideoNewsId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.Sort,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@Sort",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.StartDate,
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@StartDate",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.EndDate,
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@EndDate",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.Status,
                            SqlDbType = SqlDbType.TinyInt,
                            ParameterName = "@Status",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.Type,
                            SqlDbType = SqlDbType.TinyInt,
                            ParameterName = "@Type",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.Link,
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 256,
                            ParameterName = "@Link",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.ImgPath,
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 512,
                            ParameterName = "@ImgPath",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.IsTop ? 1 : 0,
                            SqlDbType = SqlDbType.Bit,
                            ParameterName = "@IsTop",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.Title,
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 128,
                            ParameterName = "@Title",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.VideoNews.TextBody,
                            SqlDbType = SqlDbType.NVarChar,
                            Size = -1,
                            ParameterName = "@TextBody",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }
    }
}
