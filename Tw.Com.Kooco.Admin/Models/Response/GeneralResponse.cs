namespace Tw.Com.Kooco.Admin.Models.Response
{
    public class GeneralResponse : IResponse
    {
        private string _code;

        public string Code
        {
            get { return string.IsNullOrEmpty(_code) ? "" : _code; }
            set { _code = value; }
        }

        public bool Ok { get; set; }

        public int dbResult { get; set; }
    }
}