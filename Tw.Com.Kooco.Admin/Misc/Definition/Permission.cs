using System.Collections.Specialized;

namespace Tw.Com.Kooco.Admin.Misc.Definition
{
    public class Permission
    {
        public static readonly StringDictionary Data = new StringDictionary();

        static Permission()
        {
            //list
            Data.Add("1", "List");
            //add
            Data.Add("2", "Add");
            //edit
            Data.Add("4", "Edit");
            //del or delete
            Data.Add("8", "Del");
            //view or detail
            Data.Add("16", "View");
            //export
            Data.Add("32", "Export");
            //exec
            Data.Add("64", "Exec");
            //search
            Data.Add("128", "Search");
        }
    }
}