using System;

namespace Tw.Com.Kooco.Admin.Models.Parameters
{
    public class CouponParameter : ListParameter
    {
        public int CouponId { get; set; }
        public int CouponDetailId { get; set; }
        public int SerialLength { get; set; }
        public string Title { get; set; }
        public string Items { get; set; }
        public int Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}