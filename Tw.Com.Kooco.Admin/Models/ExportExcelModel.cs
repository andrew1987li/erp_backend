using jIAnSoft.Framework.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Style.Dxf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Tw.Com.Kooco.Admin.Models
{
    public class ExcelColume
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
    }

    public class Format
    {
        public const string General = "General";
        public const string NODecimals = "0_ ";
        public const string Decimals = "0.00_ ";
        public const string Text = "@";
        public const string FullDate = "yyyy-mm-dd h:mm:ss";
    }

    public class ExportExcelModel
    {
        public ExcelPackage GetExcelPackage()
        {
            ExcelPackage ep = new ExcelPackage();

            ExcelWorkbook ewb = ep.Workbook;
            ewb.Properties.Company = "KOOCO Co., Ltd.";
            ewb.Properties.LastModifiedBy = Section.Get.Common.Name;
            ewb.Properties.Category = "";
            ewb.Properties.Author = "KOOCO Co., Ltd.";

            return ep;
        }

        public void SetWorksheets(ExcelPackage ep, string SheetName, IEnumerable table, List<ExcelColume> columes)
        {
            var ws = ep.Workbook.Worksheets.Add(SheetName);

            int c = 1;
            foreach (ExcelColume col in columes)
            {
                var cell = ws.Cells[1, c];
                cell.Value = col.Name;
                cell.Style.Font.Bold = true;
                c++;
            }

            int r = 1;
            foreach (Dictionary<string, string> row in table)
            {
                c = 1;
                foreach (ExcelColume col in columes)
                {
                    var cell = ws.Cells[r + 1, c];
                    if (row.ContainsKey(col.Id))
                    {
                        cell.Style.Numberformat.Format = col.Format;

                        switch (col.Format)
                        {
                            case Format.NODecimals:
                                cell.Value = Convert.ToInt64(row[col.Id].ToString());
                                break;

                            case Format.Decimals:
                                cell.Value = Convert.ToDecimal(row[col.Id].ToString());
                                break;

                            case Format.Text:
                                cell.Value = row[col.Id].ToString();
                                break;

                            default:
                                cell.Value = row[col.Id].ToString();
                                break;
                        }

                        c++;
                    }
                    else
                    {
                        Debug.WriteLine(string.Format("Key NotFount : {0}", col.Id));
                    }
                }
                r++;
            }

            int startColumn = ws.Dimension.Start.Column;
            int endColumn = ws.Dimension.End.Column;
            for (int i = startColumn; i <= endColumn; i++)
            {
                var cell = ws.Column(i);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;//靠左對齊
                cell.Style.Font.Name = "Cambria";
                cell.Style.Font.Size = 12;
                cell.AutoFit();//依內容fit寬度
            }
        }

        public void SetWorksheets(ExcelPackage ep, string SheetName, IEnumerable table, Dictionary<string, string> columes)
        {
            var ws = ep.Workbook.Worksheets.Add(SheetName);

            int c = 1;
            foreach (string key in columes.Keys)
            {
                string ColumnName = columes[key];

                var cell = ws.Cells[1, c];
                cell.Value = ColumnName;
                cell.Style.Font.Bold = true;
                c++;
            }

            int r = 1;
            foreach (Dictionary<string, string> row in table)
            {
                c = 1;
                foreach (string key in columes.Keys)
                {
                    var cell = ws.Cells[r + 1, c];
                    if (row.ContainsKey(key))
                    {
                        cell.Value = row[key].ToString();
                        c++;
                    }
                    else
                    {
                        Debug.WriteLine(string.Format("Key NotFount : {0}", key));
                    }
                }
                r++;
            }

            int startColumn = ws.Dimension.Start.Column;
            int endColumn = ws.Dimension.End.Column;
            for (int i = startColumn; i <= endColumn; i++)
            {
                var cell = ws.Column(i);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;//靠左對齊
                cell.Style.Font.Name = "Cambria";
                cell.Style.Font.Size = 12;
                cell.AutoFit();//依內容fit寬度
            }
        }
    }
}