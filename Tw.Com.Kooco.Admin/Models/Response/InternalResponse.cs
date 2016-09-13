namespace Tw.Com.Kooco.Admin.Models.Response
{
    public class InternalResponse : DetailResponse
    {
        private string _msg;

        public string Message
        {
            get { return string.IsNullOrEmpty(_msg) ? "" : _msg; }
            set { _msg = string.IsNullOrEmpty(value) ? "" : value; }
        }
    }
}