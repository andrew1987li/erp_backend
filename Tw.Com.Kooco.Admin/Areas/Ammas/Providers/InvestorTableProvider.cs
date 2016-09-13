using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using jIAnSoft.Framework.Database;
using Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters;
using Tw.Com.Kooco.Admin.Misc.Definition;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Providers
{
    internal static class InvestorTableProvider
    {
        public static DataSet List(InvestorParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(
                    CommandType.StoredProcedure,
                    "[dbo].[sp_FetchInvestorList_Sel]",
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

        public static int Delete(InvestorParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "DELETE FROM [dbo].[Investor] WHERE [InvestorId] = @InvestorId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.InvestorId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@InvestorId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static int Create(InvestorParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "INSERT INTO [dbo].[Investor]([DocPath],[TypesOfCatagory],[Titel],[HyperLink],[PublishDate],[CreateTime])VALUES(@DocPath,@TypesOfCatagory,@Titel,@HyperLink,@PublishDate,GETUTCDATE());",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.TypesOfCatagory??"",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@TypesOfCatagory",
                            Size = 256,
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.Titel??"",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Titel",
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

        public static int Update(InvestorParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "UPDATE [dbo].[Investor] SET [DocPath] = @DocPath, [TypesOfCatagory]=@TypesOfCatagory,[Titel]=@Titel,[HyperLink]=@HyperLink,[PublishDate]=@PublishDate,[UpdateTime]=GETUTCDATE() WHERE [InvestorId] = @InvestorId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.InvestorId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@InvestorId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.TypesOfCatagory ?? "",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@TypesOfCatagory",
                            Size = 256,
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = param.Entity.Titel ?? "",
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Titel",
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
                        },
                        new SqlParameter {
                            Value = param.Entity.DocPath,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@DocPath",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }

        public static StringDictionary Detail(InvestorParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.First(
                    CommandType.Text,
                    "SELECT [InvestorId],[TypesOfCatagory],[Titel],[publishDate],[HyperLink],[CreateTime], [DocPath] FROM [dbo].[Investor] WITH(READUNCOMMITTED) WHERE [InvestorId] = @InvestorId;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.InvestorId,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@InvestorId",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }
    }
}
