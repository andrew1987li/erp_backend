namespace Tw.Com.Kooco.Admin.Models.Response
{
    public class ListResponse : GeneralResponse
    {
        public long Records { get; set; }

        public object Collection { get; set; }
    }
}