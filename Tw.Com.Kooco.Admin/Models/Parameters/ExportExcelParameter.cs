using System.Collections.Generic;

namespace Tw.Com.Kooco.Admin.Models.Parameters
{
    public class ExportExcelParameter
    {
        public string FileName { get; set; }

        public List<string> columesName { get; set; }

        public List<string> columesType { get; set; }

        public List<List<string>> rows { get; set; }
    }
}