using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters;
using Tw.Com.Kooco.Admin.Areas.Ammas.Entitys;
using Tw.Com.Kooco.Admin.Misc.Definition;
using System.Data.SqlClient;
using jIAnSoft.Framework.Database;
using System.Data;
using System.Data.Common;
using static System.String;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Providers
{
    //建案管理
    internal static class BuildingCaseProvider
    {
        public static DataSet List(BuildingCaseParameter param)
        { 
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(
                    CommandType.StoredProcedure,
                    "[dbo].[sp_FetchConCompanyList_Sel]",
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

        #region 取全部資料項
        /*
        public static DataSet GetBuildingCaseList()
        {
            const string sql = @"
                        SELECT *
                        FROM [dbo].[ConstructionItem]
                        ORDER BY [Id];
                    ";
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(CommandType.Text, sql);
            }
        }*/
        #endregion

        #region 取指定的Id資料項
        public static BuildingCaseEntity GetRecordById(BuildingCaseParameter param)
        {
            string sql = @"SELECT TOP 1 * FROM [dbo].[conCompany] WHERE Id=@Id;";

            using (var db = new MsSql(DbName.Official))
            {
                var data = new BuildingCaseEntity();
                data.Fill(
                    db.First(
                        CommandType.Text,
                        sql,
                        new DbParameter[] {
                            new SqlParameter {
                                Value = param.Entity.Id,
                                SqlDbType = SqlDbType.BigInt,
                                ParameterName = "@Id",
                                Direction = ParameterDirection.Input
                            }
                        }));
                return data;
            }
        }
        #endregion
        
        #region 新增
        public static int Create(BuildingCaseParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "INSERT INTO [dbo].[conCompany] ([ProjectName],[StartDate],[FinishDate],[Status],[Address]) VALUES(@ProjectName,@StartDate,@FinishDate,@Status,@Address);",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = (object)param.Entity.ProjectName ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ProjectName",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            //Value = string.IsNullOrEmpty(param.Entity.StartDate) ? null : param.Entity.StartDate,
                            Value = (object)param.Entity.StartDate ?? DBNull.Value,
                            SqlDbType = SqlDbType.Date,
                            ParameterName = "@StartDate",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.FinishDate ?? DBNull.Value,
                            SqlDbType = SqlDbType.Date,
                            ParameterName = "@FinishDate",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Status ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Status",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Address ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Address",
                            Direction = ParameterDirection.Input
                        }
                    });
            }

        }
        #endregion

        #region 刪除 指定的Id資料項
        public static int Delete(BuildingCaseParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "DELETE FROM [dbo].[conCompany] WHERE [Id] = @Id;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.Id,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@Id",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }
        #endregion

        #region 修改 指定的Id資料項
        public static int Update(BuildingCaseParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "UPDATE [dbo].[conCompany] SET [ProjectName] = @ProjectName, [StartDate] = @StartDate, [FinishDate] = @FinishDate, [Status] = @Status, [Address] = @Address WHERE [Id] = @Id;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = (object)param.Entity.Id ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@Id",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.ProjectName ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ProjectName",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.StartDate ?? DBNull.Value,
                            SqlDbType = SqlDbType.Date,
                            ParameterName = "@StartDate",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.FinishDate ?? DBNull.Value,
                            SqlDbType = SqlDbType.Date,
                            ParameterName = "@FinishDate",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Status ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Status",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Address ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Address",
                            Direction = ParameterDirection.Input
                        }
                    });
            }

        }
        #endregion
    }

    //工程項目單元
    internal static class ConstructionItemProvider
    {
        public static DataSet List(ConstructionItemParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(
                    CommandType.StoredProcedure,
                    "[dbo].[sp_FetchConstructionItemList_Sel]",
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

        #region 取全部資料項
        /*public static DataSet GetList()
        {
            const string sql = @"
                        SELECT *
                        FROM [dbo].[ConstructionItem]
                        ORDER BY [Id];
                    ";
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(CommandType.Text, sql);
            }
        }*/
        #endregion

        #region 取指定的Id資料項
        public static ConstructionItemEntity GetRecordById(ConstructionItemParameter param)
        {
            string sql = @"SELECT TOP 1 * FROM [dbo].[ConstructionItem] WHERE Id=@Id;";

            using (var db = new MsSql(DbName.Official))
            {
                var data = new ConstructionItemEntity();
                data.Fill(
                    db.First(
                        CommandType.Text,
                        sql,
                        new DbParameter[] {
                            new SqlParameter {
                                Value = param.Entity.Id,
                                SqlDbType = SqlDbType.BigInt,
                                ParameterName = "@Id",
                                Direction = ParameterDirection.Input
                            }
                        }));
                return data;
            }
        }
        #endregion

        #region 新增
        public static int Create(ConstructionItemParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "INSERT INTO [dbo].[ConstructionItem] ([Name],[Unit],[Price],[DateChange]) VALUES(@Name,@Unit,@Price,GETUTCDATE());",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = (object)param.Entity.Name ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Name",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            //Value = string.IsNullOrEmpty(param.Entity.StartDate) ? null : param.Entity.StartDate,
                            Value = (object)param.Entity.Unit ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Unit",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Price ?? DBNull.Value,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@Price",
                            Direction = ParameterDirection.Input
                        }
                    });
            }

        }
        #endregion

        #region 刪除 指定的Id資料項
        public static int Delete(ConstructionItemParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "DELETE FROM [dbo].[ConstructionItem] WHERE [Id] = @Id;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.Id,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@Id",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }
        #endregion

        #region 修改 指定的Id資料項
        public static int Update(ConstructionItemParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "UPDATE [dbo].[ConstructionItem] SET [Name]=@Name, [Unit]=@Unit, [Price]=@Price, [DateChange]=GETUTCDATE() WHERE [Id] = @Id;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = (object)param.Entity.Id ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@Id",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Name ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Name",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            //Value = string.IsNullOrEmpty(param.Entity.StartDate) ? null : param.Entity.StartDate,
                            Value = (object)param.Entity.Unit ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Unit",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Price ?? DBNull.Value,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@Price",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.DateChange ?? DBNull.Value,
                            SqlDbType = SqlDbType.DateTime,
                            ParameterName = "@DateChange",
                            Direction = ParameterDirection.Input
                        }
                    });
            }

        }
        #endregion
    }

    //承包商主表
    internal static class ConstructorProvider
    {
        public static DataSet List(ConstructorParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(
                    CommandType.StoredProcedure,
                    "[dbo].[sp_FetchConstructorList_Sel]",
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
        //Get ConstrucionType 工程項目類別清單
        public static DataSet GetConstructionTypeList(ConstructorParameter param)
        {
            /*const string sql = @"SELECT Id, Name FROM [dbo].[ConstructionType]";
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(CommandType.Text, sql);
            }*/

            var db = new MsSql(DbName.Official);
            {
                const string sql = @"SELECT Id, Name FROM [dbo].[ConstructionType] WHERE Id=@Id";
                return db.DataSet(
                    CommandType.Text,
                    sql,
                    new DbParameter[] {
                    new SqlParameter {
                        Value = param.Entity.Id,
                        SqlDbType = SqlDbType.BigInt,
                        ParameterName = "@Id",
                        Direction = ParameterDirection.Input
                    } }
                );
            }
        }

        //取 全部的 工程類別項目
        public static DataSet GetAllConType()
        {
            const string sql = @"SELECT Id, Name FROM [dbo].[ConstructionType] ;";
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(CommandType.Text, sql);
            }
        }

        //取 承包商的 工程類別項目
        public static DataSet GetConstructorConType(long ConstructorId)
        {
            var db = new MsSql(DbName.Official);
            {
                const string sql = @"SELECT rest.Id, rest.Name FROM (SELECT ctype.Id, ctype.Name, detail.ConstructorId FROM [dbo].[ConstructionType] ctype " +
                    "JOIN [dbo].[ConstructorDetail] detail ON detail.TypeId = ctype.Id) rest WHERE rest.ConstructorId = @Id";
                return db.DataSet(
                    CommandType.Text,
                    sql,
                    new DbParameter[] {
                    new SqlParameter {
                        Value = ConstructorId.ToString(),
                        SqlDbType = SqlDbType.BigInt,
                        ParameterName = "@Id",
                        Direction = ParameterDirection.Input
                    }
                });
            }
        }

        #region 取全部資料項
        /*public static DataSet GetList()
        {
            const string sql = @"
                        SELECT *
                        FROM [dbo].[ConstructorHead]
                        ORDER BY [Id];
                    ";
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(CommandType.Text, sql);
            }
        }*/
        #endregion

        #region 取指定的Id資料項
        public static ConstructorEntity GetRecordById(ConstructorParameter param)
        {
            string sql = @"SELECT TOP 1 * FROM [dbo].[ConstructorHead] WHERE Id=@Id;";

            using (var db = new MsSql(DbName.Official))
            {
                var data = new ConstructorEntity();
                data.Fill(
                    db.First(
                        CommandType.Text,
                        sql,
                        new DbParameter[] {
                            new SqlParameter {
                                Value = param.Entity.Id,
                                SqlDbType = SqlDbType.BigInt,
                                ParameterName = "@Id",
                                Direction = ParameterDirection.Input
                            }
                        }));
                return data;
            }
        }
        #endregion

        #region 新增
        public static int Create(ConstructorParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "INSERT INTO [dbo].[ConstructorHead] ([Name],[Tel],[Fax],[TaxId],[Address],[MEMO]) VALUES(@Name,@Tel,@Fax,@TaxId,@Address,@MEMO);",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = (object)param.Entity.Name ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Name",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            //Value = string.IsNullOrEmpty(param.Entity.StartDate) ? null : param.Entity.StartDate,
                            Value = (object)param.Entity.Tel ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Tel",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Fax ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Fax",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.TaxId ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@TaxId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Address ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Address",
                            Direction = ParameterDirection.Input
                        },
                        /*new SqlParameter {
                            Value = (object)param.Entity.Duty ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Duty",
                            Direction = ParameterDirection.Input
                        },*/
                        new SqlParameter {
                            Value = (object)param.Entity.MEMO ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@MEMO",
                            Direction = ParameterDirection.Input
                        }
                    });
            }

        }
        #endregion

        #region 刪除 指定的Id資料項
        public static int Delete(ConstructorParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "DELETE FROM [dbo].[ConstructorHead] WHERE [Id] = @Id;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.Id,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@Id",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }
        #endregion

        #region 修改 指定的Id資料項
        public static int Update(ConstructorParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "UPDATE [dbo].[ConstructorHead] SET [Name]=@Name, [Tel]=@Tel, [Fax]=@Fax, [TaxId]=@TaxId, [Address]=@Address, [MEMO]=@MEMO WHERE [Id] = @Id;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = (object)param.Entity.Id ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@Id",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Name ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Name",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Tel ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Tel",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Fax ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Fax",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.TaxId ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@TaxId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.Address ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Address",
                            Direction = ParameterDirection.Input
                        },
                        /*new SqlParameter {
                            Value = (object)param.Entity.Duty ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@Duty",
                            Direction = ParameterDirection.Input
                        },*/
                        new SqlParameter {
                            Value = (object)param.Entity.MEMO ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@MEMO",
                            Direction = ParameterDirection.Input
                        }
                    });
            }

        }
        #endregion
    }

    //承包商明細
    internal static class ConstructorDetailProvider
    {

    }

    //合約主表
    internal static class ContractProvider
    {
        public static DataSet List(ContractParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(
                    CommandType.StoredProcedure,
                    "[dbo].[sp_FetchContractList_Sel]",
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
        
        //Get Detail
        public static DataSet GetDetail(ContractParameter param)
        {
            var db = new MsSql(DbName.Official);
            {
                /*const string sql = @"SELECT rest.Id, rest.Name FROM (SELECT ctype.Id, ctype.Name, detail.ConstructorId FROM [dbo].[ConstructionType] ctype " +
                    "JOIN [dbo].[ConstructorDetail] detail ON detail.TypeId = ctype.Id) rest WHERE rest.ConstructorId = @Id";*/
                const string sql= "SELECT r3.* FROM (SELECT r2.*, ctype.Name AS ConstructionName FROM"+
                    " (SELECT r.*, c.ProjectName as CaseName FROM"+
                    " (SELECT rmaster.*, con.Name AS ConstructorName  FROM"+
                    " [dbo].[ContractHead] rmaster LEFT JOIN[dbo].[ConstructorHead] con ON rmaster.ConstructorId = con.Id) r" +
                    " LEFT JOIN[dbo].[conCompany] c ON r.CaseId = c.Id) r2"+
                    " LEFT JOIN[dbo].[ConstructionType] ctype ON r2.ConstructionTypeId = ctype.Id) r3;";
                return db.DataSet(
                    CommandType.Text,
                    sql,
                    new DbParameter[] {
                    new SqlParameter {
                        Value = param.Entity.Id,
                        SqlDbType = SqlDbType.BigInt,
                        ParameterName = "@Id",
                        Direction = ParameterDirection.Input
                    }
                });
            }
        } 

        //取子表資料
        public static DataSet GetContractDetail(long Id)
        {
            string sql = @"SELECT * FROM [dbo].[ContractDetail] AS Detail LEFT JOIN [dbo].[ContractHead] AS Head ON Detail.ContractId = Head.Id WHERE Head.Id=@Id ;";

            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(
                    CommandType.Text,
                    sql,
                    new DbParameter[] {
                        new SqlParameter {
                            Value = (object)Id ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@Id",
                            Direction = ParameterDirection.Input
                        }
                });
            }
        }

        #region 取指定的Id資料項
        public static ContractEntity GetRecordById(ContractParameter param)
        {
            string sql = @"SELECT TOP 1 * FROM [dbo].[ContractHead] WHERE Id=@Id;";

            using (var db = new MsSql(DbName.Official))
            {
                var data = new ContractEntity();
                data.Fill(
                    db.First(
                        CommandType.Text,
                        sql,
                        new DbParameter[] {
                            new SqlParameter {
                                Value = param.Entity.Id,
                                SqlDbType = SqlDbType.BigInt,
                                ParameterName = "@Id",
                                Direction = ParameterDirection.Input
                            }
                        }));
                return data;
            }
        }
        #endregion

        #region 新增
        public static int Create(ContractParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "INSERT INTO [dbo].[ContractHead] ([ContractName],[CaseId],[ConstructorId],[ConstructionTypeId],[RetentionMoney],[SpecialRetention],[MEMO],[ContactPerson],[ContractDate],[YearCount]) " +
                    " VALUES(@ContractName,@CaseId,@ConstructorId,@ConstructionTypeId,@RetentionMoney,@SpecialRetention,@MEMO,@ContactPerson,@ContractDate,@YearCount);",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = (object)param.Entity.ContractName ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ContractName",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.CaseId ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@CaseId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.ConstructorId ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@ConstructorId",
                            Direction = ParameterDirection.Input
                        },                        
                        new SqlParameter {
                            Value = (object)param.Entity.ConstructionTypeId ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@ConstructionTypeId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.RetentionMoney ?? DBNull.Value,
                            SqlDbType = SqlDbType.SmallInt,
                            ParameterName = "@RetentionMoney",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.SpecialRetention ?? DBNull.Value,
                            SqlDbType = SqlDbType.SmallInt,
                            ParameterName = "@SpecialRetention",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.MEMO ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@MEMO",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.ContactPerson ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ContactPerson",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.ContractDate ?? DBNull.Value,
                            SqlDbType = SqlDbType.Date,
                            ParameterName = "@ContractDate",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.YearCount ?? DBNull.Value,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@YearCount",
                            Direction = ParameterDirection.Input
                        }
                    });
            }

        }
        #endregion

        #region 刪除 指定的Id資料項
        public static int Delete(ContractParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "DELETE FROM [dbo].[ContractHead] WHERE [Id] = @Id;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.Id,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@Id",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }
        #endregion

        #region 修改 指定的Id資料項
        public static int Update(ContractParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "UPDATE [dbo].[ContractHead] SET [ContractName]=@ContractName, [CaseId]=@CaseId, [ConstructorId]=@ConstructorId,"+
                    " [ConstructionTypeId]=@ConstructionTypeId, [RetentionMoney]=@RetentionMoney, [SpecialRetention]=@SpecialRetention, [MEMO]=@MEMO,[ContactPerson]=@ContactPerson,[ContractDate]=@ContractDate,[YearCount]=@YearCount WHERE [Id] = @Id;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = (object)param.Entity.ContractName ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ContractName",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.CaseId ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@CaseId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.ConstructorId ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@ConstructorId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.ConstructionTypeId ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@ConstructionTypeId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.RetentionMoney ?? DBNull.Value,
                            SqlDbType = SqlDbType.SmallInt,
                            ParameterName = "@RetentionMoney",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.SpecialRetention ?? DBNull.Value,
                            SqlDbType = SqlDbType.SmallInt,
                            ParameterName = "@SpecialRetention",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.MEMO ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@MEMO",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.ContactPerson ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@ContactPerson",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.ContractDate ?? DBNull.Value,
                            SqlDbType = SqlDbType.Date,
                            ParameterName = "@ContractDate",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.YearCount ?? DBNull.Value,
                            SqlDbType = SqlDbType.Int,
                            ParameterName = "@YearCount",
                            Direction = ParameterDirection.Input
                        }
                    });
            }

        }
        #endregion
    }



    //請款單主表
    internal static class InvoiceProvider
    {
        public static DataSet List(InvoiceParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.DataSet(
                    CommandType.StoredProcedure,
                    "[dbo].[sp_FetchInvoiceList_Sel]",
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
        
        //Get Detail
        public static DataSet GetDetail(InvoiceParameter param)
        {
            var db = new MsSql(DbName.Official);
            {
                /*const string sql = @"SELECT rest.Id, rest.Name FROM (SELECT ctype.Id, ctype.Name, detail.ConstructorId FROM [dbo].[ConstructionType] ctype " +
                    "JOIN [dbo].[ConstructorDetail] detail ON detail.TypeId = ctype.Id) rest WHERE rest.ConstructorId = @Id";*/
                const string sql = "SELECT r3.* FROM"+
                    " (SELECT r2.*, ctype.Name AS ConstructionName FROM"+
                    " (SELECT r.*, c.ProjectName as CaseName FROM"+
                    " (SELECT rmaster.*, con.CaseId, con.ConstructorId, con.ContractName AS ContractName, con.ConstructionTypeId AS ConstructionTypeId FROM"+
                    " [dbo].[InvoiceHead] rmaster LEFT JOIN[dbo].[ContractHead] con ON rmaster.ContractId = con.Id)" +
                    " r LEFT JOIN[dbo].[conCompany] c ON r.CaseId = c.Id"+
                    " ) r2"+
                    " LEFT JOIN[dbo].[ConstructionType] ctype ON r2.ConstructionTypeId = ctype.Id) r3";
                return db.DataSet(
                    CommandType.Text,
                    sql,
                    new DbParameter[] {
                    new SqlParameter {
                        Value = param.Entity.Id,
                        SqlDbType = SqlDbType.BigInt,
                        ParameterName = "@Id",
                        Direction = ParameterDirection.Input
                    }
                });
            }
        }

        #region 取指定的Id資料項
        public static InvoiceEntity GetRecordById(InvoiceParameter param)
        {
            string sql = @"SELECT TOP 1 * FROM [dbo].[InvoiceHead] WHERE Id=@Id;";

            using (var db = new MsSql(DbName.Official))
            {
                var data = new InvoiceEntity();
                data.Fill(
                    db.First(
                        CommandType.Text,
                        sql,
                        new DbParameter[] {
                            new SqlParameter {
                                Value = param.Entity.Id,
                                SqlDbType = SqlDbType.BigInt,
                                ParameterName = "@Id",
                                Direction = ParameterDirection.Input
                            }
                        }));
                return data;
            }
        }
        #endregion

        #region 新增
        public static int Create(InvoiceParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "INSERT INTO [dbo].[InvoiceHead] ([ContractId],[MEMO]) " +
                    " VALUES(@ContractId,@MEMO);",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = (object)param.Entity.ContractId ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@ContractId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.MEMO ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@MEMO",
                            Direction = ParameterDirection.Input
                        },
                    });
            }

        }
        #endregion

        #region 刪除 指定的Id資料項
        public static int Delete(InvoiceParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "DELETE FROM [dbo].[InvoiceHead] WHERE [Id] = @Id;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = param.Entity.Id,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@Id",
                            Direction = ParameterDirection.Input
                        }
                    });
            }
        }
        #endregion

        #region 修改 指定的Id資料項
        public static int Update(InvoiceParameter param)
        {
            using (var db = new MsSql(DbName.Official))
            {
                return db.Write(
                    CommandType.Text,
                    "UPDATE [dbo].[InvoiceHead] SET [ContractId]=@ContractId, [MEMO]=@MEMO WHERE [Id] = @Id;",
                    new DbParameter[] {
                        new SqlParameter {
                            Value = (object)param.Entity.ContractId ?? DBNull.Value,
                            SqlDbType = SqlDbType.BigInt,
                            ParameterName = "@ContractId",
                            Direction = ParameterDirection.Input
                        },
                        new SqlParameter {
                            Value = (object)param.Entity.MEMO ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar,
                            ParameterName = "@MEMO",
                            Direction = ParameterDirection.Input
                        },
                    });
            }

        }
        #endregion
    }
}
