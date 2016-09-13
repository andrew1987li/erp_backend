using Tw.Com.Kooco.Admin.Areas.Ammas.Entitys;
using Tw.Com.Kooco.Admin.Models.Parameters;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters
{
    public class VideoNewsParameter : ListParameter
    {
        public VideoNews VideoNews { get; set; }

        public bool Forever { get; set; }

        public int VideoNewsType { get; set; }
    }
}
