namespace Tw.Com.Kooco.Admin.Models
{
    public class MiscModel
    {
        public class MessageParameter
        {
            private int _stayTime;

            private string _method;

            private string _parameter;

            public bool IsTransfer { get; set; }

            public string Target { get; set; }

            public int StayTime
            {
                get { return (_stayTime <= 0) ? 1 : _stayTime; }
                set { _stayTime = value; }
            }

            public string Message { get; set; }

            public string Method
            {
                get { return string.IsNullOrEmpty(_method) ? "GET" : _method; }
                set { _method = value; }
            }

            public string Parameter
            {
                get { return string.IsNullOrEmpty(_parameter) ? "" : _parameter; }
                set { _parameter = value; }
            }
        }
    }
}