using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using System.Net;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Tw.Com.Kooco.Admin.Models.Msg
{
    public class MsgFunction
    {
        public string fromnumber = ConfigurationManager.AppSettings["TwNumber"].ToString();
        public string twsid = ConfigurationManager.AppSettings["TwAccountSid"].ToString();
        public string twtoken = ConfigurationManager.AppSettings["TwAccountToken"].ToString();
        public string cs = ConfigurationManager.ConnectionStrings["Remotesql"].ConnectionString;
        public string status_callback = ConfigurationManager.AppSettings["Domain"].ToString() + "";

        public static string pat = @"[0-9]+";
        
        public bool updateSmsInfo(string ssid, string sstate)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("update SmsReports set STATE=@state where SMSID=@sid", con))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        SqlParameter sid = new SqlParameter("@sid", ssid);
                        SqlParameter state = new SqlParameter("@state", sstate);

                        command.Parameters.Add(sid);
                        command.Parameters.Add(state);

                        command.ExecuteNonQuery();
                    }
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                string msgs = ex.Message;

                return false;
            }
            return true;
        }


        public List<Results> sendSms(Msg msg)
        {
            //For the result
            List<Results> getres = new List<Results>();

            TwilioRestClient twilio = new TwilioRestClient(twsid, twtoken);

            string phonenumber = msg.phonenumber;
            phonenumber.Replace("_", "");
            phonenumber.Replace("(", "");
            phonenumber.Replace(")", "");
            phonenumber.Replace("+", "");
            phonenumber.Replace(" ", "");

            msg.phonenumber = phonenumber;


            msg.content = WebUtility.HtmlEncode(msg.content);

           char[] splits = new char[]{' ',',',':'};
            string[] phonenumbers = msg.phonenumber.Split(splits);

            Regex reg_ex = new Regex(pat, RegexOptions.IgnoreCase);


            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    foreach (string phone in phonenumbers)
                    {
                        Results res = new Results();
                        res.phonenumber = phone;

                        Match match = reg_ex.Match(phone);
                        if (match.Success && phone.Length > 10 && phone.Length < 16)
                        {
                            try
                            {
                                Message twil_msg = twilio.SendMessage(fromnumber, "+" + phone, msg.content);
                                if (twil_msg.Sid != null) res.sid = twil_msg.Sid;
                                if (twil_msg.Status != null) res.state = twil_msg.Status;
                            }
                            catch (Exception ex)
                            {
                                res.state = "Failed to send " + ex.Message;
                                res.sid = "";
                            }

                        }
                        else
                        {
                            res.state = "format error";
                            res.sid = "";
                        }


                        getres.Add(res);


                        using (SqlCommand command = new SqlCommand("insert into SmsReports (PHONENUMBER, DATESENT , SMSID , STATE,BODY) values (@number, @datesent, @sid, @state, @body)", con))
                        {
                            command.CommandType = System.Data.CommandType.Text;
                            SqlParameter name = new SqlParameter("@number", phone);
                            SqlParameter date = new SqlParameter("@datesent", DateTime.Now);
                            SqlParameter sid = new SqlParameter("@sid", res.sid);
                            SqlParameter state = new SqlParameter("@state", res.state);
                            SqlParameter body = new SqlParameter("@body", msg.content);

                            command.Parameters.Add(name);
                            command.Parameters.Add(date);
                            command.Parameters.Add(sid);
                            command.Parameters.Add(state);
                            command.Parameters.Add(body);

                            command.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                }

            }catch(Exception ex)
            {
                string msgs = ex.Message;

                throw ex;
            }
            return getres;
        }
    }
}