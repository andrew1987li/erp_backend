using jIAnSoft.Framework.Configuration;
using System;
using Tw.Com.Kooco.Admin.Misc;

namespace Tw.Com.Kooco.Admin.Models.Parameters
{
    public class ListParameter
    {
        /*  private string _page;

        public string Page {
            get { return string.IsNullOrEmpty(_page) ? "0" : _page; }
            set { _page = string.IsNullOrEmpty(value) ? "0" : value; }
        }

        private string _pageSize;

         public string PageSize {
             get {
                 return string.IsNullOrEmpty(_pageSize) ? int.MaxValue.ToString(Section.Get.Common.Culture) : _pageSize;
             }
             set {
                 _pageSize = string.IsNullOrEmpty(value) ? int.MaxValue.ToString(Section.Get.Common.Culture) : value;
             }
         }*/
        private int _p;

        private long _ps;

        private string _kw;

        private string _startDate;

        private string _endDate;

        public int Page
        {
            get
            {
                if (_p < 0)
                {
                    _p = 0;
                }
                if (_p >= int.MaxValue)
                {
                    _p = int.MaxValue;
                }
                return _p;
            }
            set { _p = value; }
        }

        public long PageSize
        {
            get
            {
                if (_ps <= 0)
                {
                    _ps = Convert.ToInt32(Section.Get.Web.PageSize);
                }
                if (_ps > int.MaxValue)
                {
                    _ps = int.MaxValue;
                }
                return _ps;
            }
            set { _ps = value; }
        }

        public string KeyWord
        {
            get { return string.IsNullOrEmpty(_kw) ? "" : _kw; }
            set { _kw = value; }
        }

        public string StartDate
        {
            get { return string.IsNullOrEmpty(_startDate) ? "" : _startDate; }
            set { _startDate = value; }
        }

        public string EndDate
        {
            get { return string.IsNullOrEmpty(_endDate) ? "" : _endDate; }
            set { _endDate = value; }
        }

        /// <summary>
        /// 依 StartDate 的日期轉成設定檔內指定時區對應的UTC日期時間 格式︰yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string StartUtcDateTime => string.IsNullOrEmpty(_startDate)
            ? _startDate
            : UtilityHelper.TimeZoneToUtc($"{_startDate} 00:00:00").ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 依 StartDate 的日期轉成設定檔內指定時區對應的UTC日期時間 格式︰yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string EndUtcDateTime => (string.IsNullOrEmpty(_endDate))
            ? _endDate
            : UtilityHelper.TimeZoneToUtc($"{_endDate} 23:59:59").ToString("yyyy-MM-dd HH:mm:ss");

        public bool isAjax { get; set; }

        public string json { get; set; }

        public bool Export
        {
            get;
            set;
        }
    }
}