namespace Tw.Com.Kooco.Admin.Models.Parameters
{
    public class ActionParameter
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Default { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public int Type { get; set; }

        public int Disable { get; set; }
    }
}