using jIAnSoft.Framework.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using jIAnSoft.Framework.Database;
using Tw.Com.Kooco.Admin.Misc.Definition;
using Tw.Com.Kooco.Admin.Models.Parameters;
using static System.String;

namespace Tw.Com.Kooco.Admin.Providers {
    public static class DataAccessProvider {
        #region -- Function --

        public static class Function {
            public static bool Check(FunctionParameter param) {
                if (IsNullOrEmpty(param.Function.Target)) {
                    return true;
                }
                string sql =
                    $@"
                        SELECT COUNT(*) AS Count
                        FROM [dbo].[Function]
                        WHERE [FunctionId]!={param
                        .Function.FunctionId} AND [Target]='{param.Function.Target}'
                    ";

                using (var db = new MsSql(DbName.Official)) {
                    var sd = db.First(CommandType.Text, sql);
                    return (Convert.ToInt32(sd["Count"]) == 0);
                }
            }

            public static int Delete(FunctionParameter param) {
                using (var db = new MsSql(DbName.Official)) {
                    return Convert.ToInt32(
                        db.Value(
                            CommandType.StoredProcedure,
                            "[dbo].[sp_Function_Del]",
                            new DbParameter[] {
                                new SqlParameter {
                                    Value = param.Function.FunctionId,
                                    SqlDbType = SqlDbType.Int,
                                    ParameterName = "@argIntFunctionId",
                                    Direction = ParameterDirection.Input
                                }
                            }));
                }
            }

            public static StringDictionary Detail(FunctionParameter param) {
                using (var db = new MsSql(DbName.Official)) {
                    return db.First(
                        CommandType.StoredProcedure,
                        "[dbo].[sp_FunctionDetail_Sel]",
                        new DbParameter[] {
                            new SqlParameter {
                                Value = param.Function.FunctionId,
                                SqlDbType = SqlDbType.Int,
                                ParameterName = "@argIntFunctionId",
                                Direction = ParameterDirection.Input
                            }
                        });
                }
            }

            public static int Insert(FunctionParameter param) {
                using (var db = new MsSql(DbName.Official)) {
                    return
                        Convert.ToInt32(
                            db.Value(
                                CommandType.StoredProcedure,
                                "[dbo].[sp_Function_Ins]",
                                new DbParameter[] {
                                    new SqlParameter {
                                        Value = param.Function.ParentFunctionId,
                                        SqlDbType = SqlDbType.Int,
                                        ParameterName = "@argIntParentFunctionId",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Name,
                                        SqlDbType = SqlDbType.NVarChar,
                                        Size = 128,
                                        ParameterName = "@argStrName",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Icon,
                                        SqlDbType = SqlDbType.NVarChar,
                                        Size = 128,
                                        ParameterName = "@argStrIcon",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Target,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 1024,
                                        ParameterName = "@argStrTarget",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Area,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 1024,
                                        ParameterName = "@argStrArea",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Controller,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 1024,
                                        ParameterName = "@argStrController",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Action,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 1024,
                                        ParameterName = "@argStrAction",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Parameters,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 128,
                                        ParameterName = "@argStrParameters",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = 0,
                                        SqlDbType = SqlDbType.Int,
                                        ParameterName = "@argIntOwner",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Priority,
                                        SqlDbType = SqlDbType.Int,
                                        ParameterName = "@argIntPriority",
                                        Direction = ParameterDirection.Input
                                    }
                                }));
                }
            }

            public static DataSet List() {
                return List(new FunctionParameter());
            }

            public static DataSet List(FunctionParameter param) {
                using (var db = new MsSql(DbName.Official)) {
                    return db.DataSet(
                        CommandType.StoredProcedure,
                        "[dbo].[sp_Function_Sel]",
                        new DbParameter[] {
                            new SqlParameter {
                                Value = param.Function.Owner,
                                SqlDbType = SqlDbType.Int,
                                ParameterName = "@argIntOwner",
                                Direction = ParameterDirection.Input
                            }
                        });
                }
            }

            public static int NextPriority(FunctionParameter param) {
                using (var db = new MsSql(DbName.Official)) {
                    return
                        Convert.ToInt32(
                            db.One(
                                CommandType.StoredProcedure,
                                "[dbo].[sp_FunctionNextPriority_Sel]",
                                new DbParameter[] {
                                    new SqlParameter {
                                        Value = param.Function.FunctionId,
                                        SqlDbType = SqlDbType.Int,
                                        ParameterName = "@argIntFunctionId",
                                        Direction = ParameterDirection.Input
                                    }
                                }));
                }
            }

            public static int Update(FunctionParameter param) {
                using (var db = new MsSql(DbName.Official)) {
                    return
                        Convert.ToInt32(
                            db.Value(
                                CommandType.StoredProcedure,
                                "[dbo].[sp_Function_Upd]",
                                new DbParameter[] {
                                    new SqlParameter {
                                        Value = param.Function.FunctionId,
                                        SqlDbType = SqlDbType.Int,
                                        ParameterName = "@argIntFunctionId",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Name,
                                        SqlDbType = SqlDbType.NVarChar,
                                        Size = 128,
                                        ParameterName = "@argStrName",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Icon,
                                        SqlDbType = SqlDbType.NVarChar,
                                        Size = 128,
                                        ParameterName = "@argStrIcon",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Target,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 1024,
                                        ParameterName = "@argStrTarget",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Area,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 1024,
                                        ParameterName = "@argStrArea",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Controller,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 1024,
                                        ParameterName = "@argStrController",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Action,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 1024,
                                        ParameterName = "@argStrAction",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Parameters,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 128,
                                        ParameterName = "@argStrParameters",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.Function.Priority,
                                        SqlDbType = SqlDbType.Int,
                                        ParameterName = "@argIntPriority",
                                        Direction = ParameterDirection.Input
                                    }
                                }));
                }
            }

            public static int UpdatePriority(long functionId, int priority, string code, string parent, int depth) {
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(
                        CommandType.Text,
                        "UPDATE [dbo].[Function] SET [Priority] = @argIntPriority, [Code] = @argStrCode, [Parent] = @argStrParent, [Depth] = @argIntDepth WHERE [FunctionId] = @argIntFunctionId;",
                        new DbParameter[] {
                            new SqlParameter {
                                Value = functionId,
                                SqlDbType = SqlDbType.BigInt,
                                ParameterName = "@argIntFunctionId",
                                Direction = ParameterDirection.Input
                            },
                            new SqlParameter {
                                Value = priority,
                                SqlDbType = SqlDbType.Int,
                                ParameterName = "@argIntPriority",
                                Direction = ParameterDirection.Input
                            },
                            new SqlParameter {
                                Value = code,
                                SqlDbType = SqlDbType.VarChar,
                                ParameterName = "@argStrCode",
                                Direction = ParameterDirection.Input
                            },
                            new SqlParameter {
                                Value = parent,
                                SqlDbType = SqlDbType.VarChar,
                                ParameterName = "@argStrParent",
                                Direction = ParameterDirection.Input
                            },
                            new SqlParameter {
                                Value = depth,
                                SqlDbType = SqlDbType.Int,
                                ParameterName = "@argIntDepth",
                                Direction = ParameterDirection.Input
                            }
                        });
                }
            }

            public static int UpdateSort(long functionId) {
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(
                        CommandType.Text,
                        "UPDATE [Function] SET [Sort] = [dbo].[fn_GetSortCode]([FunctionId]);",
                        new DbParameter[] {
                            new SqlParameter {
                                Value = functionId,
                                SqlDbType = SqlDbType.BigInt,
                                ParameterName = "@argIntFunctionId",
                                Direction = ParameterDirection.Input
                            }
                        });
                }
            }
        }

        #endregion -- Function --

        #region -- User --

        public static class User {
            public static int ChangePassword(string account, string password, string encryptPassword) {
                var sql =
                    Format(
                        "UPDATE [dbo].[User]SET [Password]='{1}',[EncryptPassword]='{2}',UpdateTime=GETUTCDATE()WHERE [Account]='{0}'",
                        account, password, encryptPassword);
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }

            public static DataSet Detail(string account) {
                using (var db = new MsSql(DbName.Official)) {
                    return db.DataSet(
                        CommandType.StoredProcedure,
                        "[dbo].[sp_UserDetailByAccount_Sel]",
                        new DbParameter[] {
                            new SqlParameter {
                                Value = account,
                                SqlDbType = SqlDbType.VarChar,
                                Size = 32,
                                ParameterName = "@argStrAccount",
                                Direction = ParameterDirection.Input
                            }
                        });
                }
            }

            public static DataSet Detail(long userId) {
                using (var db = new MsSql(DbName.Official)) {
                    return db.DataSet(
                        CommandType.StoredProcedure,
                        "[dbo].[sp_UserDetailById_Sel]",
                        new DbParameter[] {
                            new SqlParameter {
                                Value = userId,
                                SqlDbType = SqlDbType.BigInt,
                                ParameterName = "@argIntUserId",
                                Direction = ParameterDirection.Input
                            }
                        });
                }
            }

            public static DataSet GetUserAction(long userId) {
                string sql =
                    $@"
                        SELECT *
                        FROM [dbo].[Actions];

                        SELECT ActionID
                        FROM [dbo].[UserAction]
                        WHERE UserID={userId};
                    ";
                using (var db = new MsSql(DbName.Official)) {
                    return db.DataSet(CommandType.Text, sql);
                }
            }

            public static DataSet GetUserRole(long userId) {
                string sql = Format(@"
                        SELECT *
                        FROM [dbo].[Role];

                        SELECT RoleID
                        FROM [dbo].[UserRole]
                        WHERE UserID={0};
                    ", userId);
                using (var db = new MsSql(DbName.Official)) {
                    return db.DataSet(CommandType.Text, sql);
                }
            }

            public static int Insert(UserParameter param) {
                using (var db = new MsSql(DbName.Official)) {
                    return
                        Convert.ToInt32(
                            db.Value(
                                CommandType.StoredProcedure,
                                "[dbo].[sp_User_Ins]",
                                new DbParameter[] {
                                    new SqlParameter {
                                        Value = param.User.Account,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 32,
                                        ParameterName = "@argStrAccount",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = AzDG.Encrypt(param.User.Password),
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 128,
                                        ParameterName = "@argStrPassword",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = Md5.Encrypt(param.User.Password),
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 64,
                                        ParameterName = "@argStrEncryptPassword",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.User.FirstName,
                                        SqlDbType = SqlDbType.NVarChar,
                                        Size = 64,
                                        ParameterName = "@argStrFirstName",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.User.LastName,
                                        SqlDbType = SqlDbType.NVarChar,
                                        Size = 64,
                                        ParameterName = "@argStrLastName",
                                        Direction = ParameterDirection.Input
                                    }
                                }));
                }
            }

            public static DataSet List(UserParameter param) {
                using (var db = new MsSql(DbName.Official)) {
                    return db.DataSet(
                        CommandType.StoredProcedure,
                        "[dbo].[sp_User_Sel]",
                        new DbParameter[] {
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
                                Value = param.KeyWord,
                                SqlDbType = SqlDbType.NVarChar,
                                Size = 128,
                                ParameterName = "@argStrKeyWord",
                                Direction = ParameterDirection.Input
                            },
                            new SqlParameter {
                                Value = param.User.Status,
                                SqlDbType = SqlDbType.Int,

                                ParameterName = "@argIntStatus",
                                Direction = ParameterDirection.Input
                            }
                        });
                }
            }

            public static int PermsUpdate(UserParameter param) {
                using (var db = new MsSql(DbName.Official)) {
                    return
                        Convert.ToInt32(
                            db.Value(
                                CommandType.StoredProcedure,
                                "[dbo].[sp_UserFunctionsAndOperations_Upd]",
                                new DbParameter[] {
                                    new SqlParameter {
                                        Value = param.User.IdentityKey,
                                        SqlDbType = SqlDbType.BigInt,
                                        ParameterName = "@argIntUserId",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.User.Functions,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = -1,
                                        ParameterName = "@argStrFunctions",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.User.Operations,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = -1,
                                        ParameterName = "@argStrOperations",
                                        Direction = ParameterDirection.Input
                                    }
                                }));
                }
            }

            public static int Update(UserParameter param) {
                using (var db = new MsSql(DbName.Official)) {
                    return
                        Convert.ToInt32(
                            db.Value(
                                CommandType.StoredProcedure,
                                "[dbo].[sp_User_Upd]",
                                new DbParameter[] {
                                    new SqlParameter {
                                        Value = param.User.IdentityKey,
                                        SqlDbType = SqlDbType.BigInt,
                                        ParameterName = "@argIntUserId",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value =
                                            (IsNullOrEmpty(param.User.Password))
                                                ? Empty
                                                : AzDG.Encrypt(param.User.Password),
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 128,
                                        ParameterName = "@argStrPassword",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value =
                                            (IsNullOrEmpty(param.User.Password))
                                                ? Empty
                                                : Md5.Encrypt(param.User.Password),
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 64,
                                        ParameterName = "@argStrEncryptPassword",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = IsNullOrEmpty(param.User.FirstName) ? "" : param.User.FirstName,
                                        SqlDbType = SqlDbType.NVarChar,
                                        Size = 64,
                                        ParameterName = "@argStrFirstName",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = IsNullOrEmpty(param.User.LastName) ? "" : param.User.LastName,
                                        SqlDbType = SqlDbType.NVarChar,
                                        Size = 64,
                                        ParameterName = "@argStrLastName",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.User.Status,
                                        SqlDbType = SqlDbType.TinyInt,
                                        ParameterName = "@argIntStatus",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = param.User.PrimaryRule,
                                        SqlDbType = SqlDbType.BigInt,
                                        ParameterName = "@argIntPrimaryRuleId",
                                        Direction = ParameterDirection.Input
                                    },
                                    new SqlParameter {
                                        Value = IsNullOrEmpty(param.User.RuleGroups) ? "" : param.User.RuleGroups,
                                        SqlDbType = SqlDbType.VarChar,
                                        Size = 8000,
                                        ParameterName = "@argStrRuleGroups",
                                        Direction = ParameterDirection.Input
                                    }
                                }));
                }
            }
        }

        #endregion -- User --

        #region -- Actions --

        public static class Actions {
            public static int DeleteActions(long id) {
                string sql = $"DELETE FROM [dbo].[Actions] WHERE ID={id}";

                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }

            public static DataTable GetActions() {
                const string sql = "SELECT * FROM [dbo].[Actions] ORDER BY [Area], [Controller], [Action]";
                using (var db = new MsSql(DbName.Official)) {
                    return db.Table(CommandType.Text, sql);
                }
            }

            public static int InsertActions(List<ActionParameter> list) {
                var values = Join(",",
                    list.Select(
                        action =>
                            $"('{action.Name}', '{action.Description}', {action.Default}, '{action.Area}', '{action.Controller}', '{action.Action}', {action.Type}, {0}, {"GETUTCDATE()"}, {"GETUTCDATE()"})")
                        .ToArray());

                string sql =
                    $"INSERT INTO [dbo].[Actions] ([Name],[Description],[Default],[Area],[Controller],[Action],[Type],[Disable],[CreateTime],[UpdateTime])VALUES {values};";

                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }

            public static int UpdateActions(ActionParameter param) {
                var sql =
                    Format(
                        "UPDATE [dbo].[Actions]SET [Name]='{1}',[Description]='{2}',[Default]={3},[Area]='{4}',[Controller]='{5}',[Action]='{6}',[Type]={7},[Disable]={8},[UpdateTime]=GETUTCDATE() WHERE ID={0}",
                        param.ID, param.Name, param.Description, param.Default, param.Area, param.Controller,
                        param.Action,
                        param.Type,
                        param.Disable);
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }
        }

        #endregion -- Actions --

        #region -- Role --

        public class Role {
            public static int Delete(RoleParameter param) {
                string sql =
                    $@"
                        DELETE FROM [dbo].[Role]
                        WHERE ID={param.ID}
                    ";
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }

            public static DataSet GetActions() {
                const string sql = @"
                        SELECT *
                        FROM [dbo].[Actions];
                    ";
                using (var db = new MsSql(DbName.Official)) {
                    return db.DataSet(CommandType.Text, sql);
                }
            }

            public static DataSet GetRoleDetail(RoleParameter param) {
                var sql = Format(@"
                        SELECT *
                        FROM [dbo].[Actions];

                        SELECT *
                        FROM [dbo].[Role]
                        WHERE ID={0};

                        SELECT ActionID
                        FROM [dbo].[RoleAction]
                        WHERE RoleID={0};
                    ", param.ID);
                using (var db = new MsSql(DbName.Official)) {
                    return db.DataSet(CommandType.Text, sql);
                }
            }

            public static DataSet GetRoleList() {
                const string sql = @"
                        SELECT *
                        FROM [dbo].[Role]
                        ORDER BY [Name]
                    ";
                using (var db = new MsSql(DbName.Official)) {
                    return db.DataSet(CommandType.Text, sql);
                }
            }

            public static int Insert(RoleParameter param) {
                string sql =
                    $"INSERT INTO [dbo].[Role] ([Name], [Description], [Disable], [CreateTime], [UpdateTime])VALUES ('{param.Name}','{param.Description}', {param.Disable}, GETUTCDATE(), GETUTCDATE());";
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }

            public static int Update(RoleParameter param) {
                var sql =
                    Format(
                        "UPDATE [dbo].[Role]SET [Name]='{1}',[Description]='{2}',[Disable]={3},[UpdateTime]=GETUTCDATE()WHERE ID={0}",
                        param.ID, param.Name, param.Description, param.Disable);
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }
        }

        #endregion -- Role --

        #region -- RoleAction --

        public static class RoleAction {
            public static int Clear(long roleId) {
                string sql =
                    $"DELETE FROM [dbo].[RoleAction]WHERE RoleID={roleId};";
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }

            public static int Update(long roleId, string values) {
                string sql =
                    $"DELETE FROM [dbo].[RoleAction]  WHERE RoleID={roleId}; INSERT INTO [dbo].[RoleAction] ([RoleID], [ActionID]) VALUES {values};";
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }
        }

        #endregion -- RoleAction --

        #region -- UserRole --

        public static class UserRole {
            public static int Clear(long userId) {
                string sql = $"DELETE FROM [dbo].[UserRole]WHERE UserID={userId}; ";
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }

            public static int Update(long userId, string values) {
                string sql =
                    $"DELETE FROM [dbo].[UserRole] WHERE UserID={userId};INSERT INTO [dbo].[UserRole] ([UserID], [RoleID]) VALUES {values};";
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }
        }

        #endregion -- UserRole --

        #region -- UserAction --

        public static class UserAction {
            public static int Clear(long userId) {
                string sql = $"DELETE FROM [dbo].[UserAction] WHERE UserID={userId};";
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }

            public static int Update(long UserID, string Values) {
                string sql =
                    $"DELETE FROM [dbo].[UserAction]WHERE UserID={UserID};INSERT INTO [dbo].[UserAction] ([UserID], [ActionID])VALUES {Values};";
                using (var db = new MsSql(DbName.Official)) {
                    return db.Write(CommandType.Text, sql);
                }
            }
        }

        #endregion -- UserAction --
    }
}