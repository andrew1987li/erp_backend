using jIAnSoft.Framework.Configuration;
using System;
using System.Text.RegularExpressions;

namespace Tw.Com.Kooco.Admin.Misc
{
    public class Pagination : jIAnSoft.Framework.Web.Paging
    {
        public Pagination(string p)
        {
            PageVariableName = p;
        }

        public override string Render()
        {
            if (TotalPage <= 1)
                return string.Empty;

            //<div class='row'>
            //    <div class='col-md-5 col-sm-12'>
            //        <div class='dataTables_info' id='sample_1_info' role='status' aria-live='polite'>Showing 21 to 25 of 25 entries</div>
            //    </div>
            //    <div class='col-md-7 col-sm-12'>
            //        <div class='dataTables_paginate paging_bootstrap_full_number' id='sample_1_paginate'>
            //            <ul class='pagination' style='visibility: visible;'>
            //                <li class='prev'><a href='#' title='First'><i class='fa fa-angle-double-left'></i></a></li>
            //                <li class='prev'><a href='#' title='Prev'><i class='fa fa-angle-left'></i></a></li>
            //                <li><a href='#'>1</a></li>
            //                <li><a href='#'>2</a></li>
            //                <li><a href='#'>3</a></li>
            //                <li><a href='#'>4</a></li>
            //                <li class='active'><a href='#'>5</a></li>
            //                <li class='next disabled'><a href='#' title='Next'><i class='fa fa-angle-right'></i></a></li>
            //                <li class='next disabled'><a href='#' title='Last'><i class='fa fa-angle-double-right'></i></a></li>
            //            </ul>
            //        </div>
            //    </div>
            //</div>

            Sb.Clear();

            Sb.Append(@"
<div class='row'>
    <div class='col-md-7 col-sm-12'>
        <div class='paging_bootstrap_full_number'>
            <ul class='pagination' style='visibility: visible;'>
");

            if (TotalPage <= SelectablePages)
            {
                // iterate through the available pages
                for (var i = 1; i <= TotalPage; i++)
                {
                    Sb.AppendFormat(
                        "<li {1}><a href='{0}' title='前往第 {2} 頁'>{3}</a></li>",
                        BuildUri(i - 1),
                        (CurrentPage.Equals(i - 1) ? " class='active'" : ""),
                        i,
                        i.ToString(Section.Get.Common.Culture).PadLeft(
                            TotalPage.ToString(Section.Get.Common.Culture).Length, '0'));
                }
            }
            else
            {
                // compute the number of pages to display to the left of the currently selected page
                // so that the current page is always centered
                // (when at the first and the last pages this will not be possible and we'll make some adjustments on the fly)
                var adjacent = Convert.ToInt32(Math.Floor((Convert.ToDouble(SelectablePages) - 3) / 2));

                // this number must be at least "1"
                adjacent = (adjacent < 0 ? 1 : adjacent);

                // compute the page after which to show "..." after the link to the first page
                var scrollFrom = SelectablePages - adjacent - 1;
                // writer.Write("scrollFrom=" + scrollFrom + "<br />");
                // this is the number from where we should start rendering selectable pages
                // it's "2" because we have already rendered the first page
                long startingPage = 1;
                // if we need to show "..." after the link to the first page
                if (CurrentPage >= scrollFrom)
                {
                    // by default, the starting_page should be whatever the current page minus $adjacent
                    startingPage = CurrentPage - adjacent;

                    // but if that would cause us to display less navigation links than specified in $this->selectable_pages
                    if (TotalPage - startingPage < (SelectablePages - 2))
                    {
                        // adjust it
                        startingPage -= (SelectablePages - 2) - (TotalPage - startingPage);
                    }
                }
                // this is the number where we should stop rendering selectable pages
                // by default, this value is the sum of the starting page plus whatever the number of $this->selectable_pages
                // minus 3 (first page, last page and current page)
                var endingPage = startingPage + SelectablePages;

                // if ending page would be greater than the total number of pages minus 1
                // (minus one because we don't take into account the very last page which we output automatically)
                // adjust the ending page
                if (endingPage + 1 > TotalPage) endingPage = TotalPage;

                if (CurrentPage >= scrollFrom)
                {
                    Sb.AppendFormat(
                        "<li {1}><a href='{0}' title='前往第 1 頁'>{2}</a></li>",
                        BuildUri(0),
                        (CurrentPage.Equals(0) ? " class='disable'" : ""),
                        ("1".PadLeft(endingPage.ToString(Section.Get.Common.Culture).Length, '0'))
                        );
                    if (TotalPage > SelectablePages)
                    {
                        Sb.AppendFormat(
                            "<li class='prev'><a href='{1}' title='上十頁'><i class='fa fa-angle-double-left'></i></a></li>" +
                            "<li class='prev'><a href='{0}' title='上一頁'><i class='fa fa-angle-left'></i></a></li>",
                            (CurrentPage == 0 ? "javascript:void(0);" : BuildUri(CurrentPage - 1)),
                            (CurrentPage - 10 < 0 ? "javascript:void(0);" : BuildUri(CurrentPage - 10)));
                    }
                    // put the "..." after the link to the first page
                    //Sb.Append("  ....  ");
                    Sb.Append("<li><a>....</a></li>");
                }
                // place links for each page
                for (var i = startingPage; i < endingPage; i++)
                {
                    Sb.AppendFormat(
                        "<li {1}><a href='{0}' title='前往第 {2} 頁'>{3}</a></li>",
                        BuildUri(i - 1),
                        (CurrentPage.Equals(i - 1) ? " class='active'" : ""),
                        i,
                        (i.ToString(Section.Get.Common.Culture).PadLeft(
                            endingPage.ToString(Section.Get.Common.Culture).Length, '0')));
                }
                if (endingPage - (CurrentPage + 1) >= scrollFrom)
                {
                    Sb.Append("<li><a>....</a></li>");
                    Sb.AppendFormat(
                        "<li class='next'><a href='{0}' title='下一頁'><i class='fa fa-angle-right'></i></a></li>" +
                        "<li class='next'><a href='{1}' title='下十頁'><i class='fa fa-angle-double-right'></i></a></li>",
                        (CurrentPage.Equals(TotalPage - 1) ? "javascript:void(0)" : BuildUri(CurrentPage + 1)),
                        (TotalPage - (CurrentPage + 1) > 10) ? BuildUri(CurrentPage + 10) : "javascript:void(0)");
                }
                Sb.AppendFormat(
                    "<li {1}><a href='{0}' title='前往最後一頁'>{2}</a></li>",
                    BuildUri(TotalPage),
                    (CurrentPage.Equals(TotalPage - 1) ? " class='disable'" : ""),
                    TotalPage);
            }

            Sb.Append("</ul>");
            Sb.Append("</div>");
            Sb.Append("</div>");

            Sb.Append("<div class='col-md-5 col-sm-12'>");

            var endRecord = CurrentPage * RecordsPerPage + RecordsPerPage;
            if (endRecord > TotalRecord)
            {
                endRecord = TotalRecord;
            }

            Sb.AppendFormat(
                "<div class='dataTables_paginate dataTables_info' role='status' aria-live='polite'>Showing {0} to {1} of {2} entries ({3} Pages)</div>",
                (CurrentPage * RecordsPerPage + 1).ToString("#,##;(#,##)"),
                endRecord.ToString("#,##;(#,##)"),
                TotalRecord.ToString("#,##;(#,##)"),
                TotalPage.ToString("#,##;(#,##)"));

            Sb.Append("</div>");
            Sb.Append("</div>");

            return (ReplaceCrossSiteScript(Sb.ToString()));
        }

        public string RenderAjaxLink()
        {
            var result = Render();

            Regex.Replace(result, "a href", "a class='js_PageLink' href", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            return result;
        }
    }
}