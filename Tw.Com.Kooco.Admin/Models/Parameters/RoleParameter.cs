using System.Collections.Generic;

namespace Tw.Com.Kooco.Admin.Models.Parameters
{
    public class RoleParameter
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Disable { get; set; }

        public List<string> ActionIDs { get; set; }
    }
}