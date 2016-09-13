using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Tw.Com.Kooco.Admin.Models.Msg
{
    public class GetReport
    {
        public string cs = ConfigurationManager.ConnectionStrings["Remotesql"].ConnectionString;

        public List<Reports> getWeekInfo(string year, string month)
        {
            List<Reports> report_list = new List<Reports>();

            DateTime firstday = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);
            while (firstday.DayOfWeek != DayOfWeek.Monday) firstday.AddDays(1);

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                   Reports report = new Reports();

                    int index = 1;
                    report.key = index + "周";
                    try
                    {
                        string sql =String.Format( "select ID, DATEDIFF(week,\'{0}\',DATESENT) as WeekNumber from SmsReports where MONTH(DATESENT) ={1} AND YEAR(DATESENT)={2}", firstday.ToString("yyyy/MM/dd"), month, year);
                        string csql =String.Format( "select count(*), WeekNumber from ({0})a group by WeekNumber",sql);
                        using (SqlCommand command = new SqlCommand(csql, con))
                        {
                            SqlDataReader sqlreader = command.ExecuteReader();
                            while (sqlreader.Read())
                            {

                                report.value = Convert.ToInt32(sqlreader[0]);
                                report.key = sqlreader[1]+ "周";
                                report_list.Add(report);
                                report = new Reports();
                               
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        report.value = 0;


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

        public List<Reports> getDayInfo(string year, string month)
        {
            List<Reports> report_list = new List<Reports>();

            DateTime firstday = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    Reports report = new Reports();

                    int index = 1;
                    report.key = index + "日";
                    try
                    {
                        string sql = String.Format("select ID, DATEDIFF(day,\'{0}\',DATESENT) as WeekNumber from SmsReports where MONTH(DATESENT) ={1} AND YEAR(DATESENT)={2}", firstday.ToString("yyyy/MM/dd"), month, year);
                        string csql = String.Format("select count(ID), WeekNumber from ({0})a group by WeekNumber", sql);
                        using (SqlCommand command = new SqlCommand(csql, con))
                        {
                            SqlDataReader sqlreader = command.ExecuteReader();
                            while (sqlreader.Read())
                            {

                                report.value = Convert.ToInt32(sqlreader[0]);
                                report.days = Convert.ToInt32( sqlreader[1]);
                                report.key = report.days + "日";
                                report_list.Add(report);
                                report = new Reports();
                                
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        report.value = 0;


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


        public List<DetailReport> getDayDetailInfo(string year, string month, string day)
        {
            List<DetailReport> report_list = new List<DetailReport>();

            DateTime firstday = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    DetailReport report = new DetailReport();

                    int index = 1;
                    try
                    {
                        string sql = String.Format("select ID,PHONENUMBER,DATESENT, SMSID,STATE,BODY  from SmsReports where MONTH(DATESENT) ={0} AND YEAR(DATESENT)={1} AND DAY(DATESENT)={2}",  month, year, day);
                       
                        using (SqlCommand command = new SqlCommand(sql, con))
                        {
                            SqlDataReader sqlreader = command.ExecuteReader();
                            while (sqlreader.Read())
                            {
                                report.id = index++;
                                report.phonenumber = sqlreader[1].ToString();
                                report.date = sqlreader[2].ToString();
                                report.sid = sqlreader[3].ToString();
                                report.state = sqlreader[4].ToString();
                                report.body = sqlreader[5].ToString();
                                report_list.Add(report);
                                report = new DetailReport();
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


        public List<Reports> getMonthInfo(string year)
        {
            List<Reports> report_list = new List<Reports>();


            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    for (int i = 1; i < 13; i++)
                    {
                        Reports report = new Reports();

                        report.key = i + "月";
                        try
                        {
                            using (SqlCommand command = new SqlCommand("select count(*) from SmsReports where MONTH(DATESENT) =" + i + " AND YEAR(DATESENT)=" + year, con))
                            {
                                SqlDataReader sqlreader = command.ExecuteReader();
                                while (sqlreader.Read())
                                {

                                    report.value = Convert.ToInt32(sqlreader[0]);
                                }


                            }
                        }
                        catch (Exception ex)
                        {
                            report.value = 0;


                        }

                        report_list.Add(report);

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

        public RangeDate getRange()
        {
            RangeDate range = new RangeDate();
            range.startdate = "";
            range.enddate = "";

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    try
                    {
                        using (SqlCommand command = new SqlCommand("select min(DATESENT) from SmsReports", con))
                        {
                            SqlDataReader sqlreader = command.ExecuteReader();
                            while (sqlreader.Read())
                            {
                                DateTime tmp = Convert.ToDateTime(sqlreader[0]);
                                range.startdate = tmp.ToString("yyyy/MM/dd");
                            }

                            command.CommandText = "select max(DATESENT) from SmsReports";
                            sqlreader.Close();
                            sqlreader = command.ExecuteReader();
                            while (sqlreader.Read())
                            {
                                range.enddate = Convert.ToDateTime(sqlreader[0]).ToString("yyyy/MM/dd");
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        
                        throw ex;
                    }
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }



            return range;
        }
    }
}