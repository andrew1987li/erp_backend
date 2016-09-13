using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Tw.Com.Kooco.Admin.Models.Msg
{
    public class SmsUserContext
    {
        public string cs = ConfigurationManager.ConnectionStrings["Remotesql"].ConnectionString;

        public bool createUser(SmsUser user)
        {
            int rows = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    try
                    {
                        string sql = "insert into SmsUsers (NAME,EMAIL, AUTH, AUTHKEY, DATE, STATE)  values (@name, @email, @auth, @authkey, @date, @state)";
                        using (SqlCommand command = new SqlCommand(sql, con))
                        {

                            command.CommandType = System.Data.CommandType.Text;
                            SqlParameter auth = new SqlParameter("@auth", user.AUTH);
                            SqlParameter authkey = new SqlParameter("@authkey", user.AUTHKEY);
                            SqlParameter state = new SqlParameter("@state", user.STATE);
                            SqlParameter name = new SqlParameter("@name", user.NAME);
                            SqlParameter email = new SqlParameter("@email", user.EMAIL);
                            SqlParameter date = new SqlParameter("@date", user.DATE);

                            command.Parameters.Add(auth);
                            command.Parameters.Add(authkey);
                            command.Parameters.Add(state);
                            command.Parameters.Add(name);
                            command.Parameters.Add(email);
                            command.Parameters.Add(date);

                            rows = command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {


                    }


                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (rows > 0) return true; else return false;

        }

        public bool updateUser(SmsUser user)
        {
            int rows = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    try
                    {
                        string sql = "update SmsUsers set AUTH=@auth, AUTHKEY=@authkey, STATE=@state where ID=@id";
                        using (SqlCommand command = new SqlCommand(sql, con))
                        {

                            command.CommandType = System.Data.CommandType.Text;
                            SqlParameter auth = new SqlParameter("@auth", user.AUTH);
                            SqlParameter authkey = new SqlParameter("@authkey", user.AUTHKEY);
                            SqlParameter state = new SqlParameter("@state", user.STATE);
                            SqlParameter id = new SqlParameter("@id", user.ID);

                            command.Parameters.Add(auth);
                            command.Parameters.Add(authkey);
                            command.Parameters.Add(state);
                            command.Parameters.Add(id);

                            rows = command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {


                    }


                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (rows > 0) return true; else return false;

        }


        public bool deleteUser(string ID)
        {
            int rows=0;
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    try
                    {
                        using (SqlCommand command = new SqlCommand("delete from SmsUsers where ID="+ID, con))
                        {
                            rows = command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {


                    }


                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (rows > 0) return true; else return false;
        }
        public List<SmsUser> getUsers()
        {
            List<SmsUser> report_list = new List<SmsUser>();


            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    try
                    {
                        using (SqlCommand command = new SqlCommand("select * from SmsUsers", con))
                        {
                            SqlDataReader sqlreader = command.ExecuteReader();
                            while (sqlreader.Read())
                            {
                                SmsUser user = new SmsUser();
                                user.ID = sqlreader[0].ToString();
                                user.NAME = sqlreader[1].ToString();
                                user.EMAIL = sqlreader[2].ToString();
                                user.AUTH = sqlreader[3].ToString();
                                user.AUTHKEY = sqlreader[4].ToString();
                                user.STATE = sqlreader[5].ToString();
                                user.DATE = Convert.ToDateTime(sqlreader[6].ToString());
                                report_list.Add(user);
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                           

                    }


                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return report_list;
        }
        public SmsUser getUser(string id)
        {
            SmsUser user = new SmsUser();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    try
                    {
                        using (SqlCommand command = new SqlCommand("select * from SmsUsers where ID=" + id, con))
                        {
                            SqlDataReader sqlreader = command.ExecuteReader();
                            while (sqlreader.Read())
                            {
                                
                                user.ID = sqlreader[0].ToString();
                                user.NAME = sqlreader[1].ToString();
                                user.EMAIL = sqlreader[2].ToString();
                                user.AUTH = sqlreader[3].ToString();
                                user.AUTHKEY = sqlreader[4].ToString();
                                user.STATE = sqlreader[5].ToString();
                                user.DATE = Convert.ToDateTime(sqlreader[6].ToString());
                            }


                        }
                    }
                    catch (Exception ex)
                    {


                    }


                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return user;
        }
    }
}