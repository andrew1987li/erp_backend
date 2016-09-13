using Tw.Com.Kooco.Admin.Areas.Ammas.Entitys;
using Tw.Com.Kooco.Admin.Models.Parameters;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters
{
    public class AnnouncementParameter : ListParameter
    {
        public Announcement Announcement { get; set; }

        public bool Forever { get; set; }

        public int AnnouncementType { get; set; }
    }
}
