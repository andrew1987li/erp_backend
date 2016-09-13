using System;
using System.Collections.Specialized;
using System.Data;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Entitys {
    public class OurGoal {
        public int OurGoalId { get; set; }
        public string LeftImg { get; set; }
        public string RightImg { get; set; }


        public void Fill(StringDictionary row) {
            if (row.Count<=0) {
                OurGoalId = 0;
                RightImg = LeftImg = string.Empty;
            }
            else {
                OurGoalId = Convert.ToInt32(row["OurGoalId"]);
                LeftImg = row["LeftImg"];
                RightImg = row["RightImg"];
            }
        }

        public void Fill(DataRow row) {
            OurGoalId = Convert.ToInt32(row["OurGoalId"]);
            LeftImg = row["LeftImg"].ToString();
            RightImg = row["RightImg"].ToString();
        }
    }
}
