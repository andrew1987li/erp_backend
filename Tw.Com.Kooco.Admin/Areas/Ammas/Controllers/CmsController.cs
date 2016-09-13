using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using jIAnSoft.Framework.Configuration;
using jIAnSoft.Framework.Security.Cryptography;
using jIAnSoft.Framework.Utility;
using log4net;
using Tw.Com.Kooco.Admin.Areas.Ammas.Entitys;
using Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters;
using Tw.Com.Kooco.Admin.Areas.Ammas.Providers;
using Tw.Com.Kooco.Admin.Misc;
using Tw.Com.Kooco.Admin.Models;
using Tw.Com.Kooco.Admin.Models.Response;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Controllers {

    public class CmsController : Controller {
        private static readonly ILog Log = LogManager.GetLogger(typeof (CmsController));

        // GET: Ammas/Cms
        public ActionResult Index() {
            return View();
        }


        #region -- 新聞 --

        [Auth(Name = "編輯新聞", Description = "刪除")]
        [HttpPost]
        public ActionResult AjaxAnnouncementDelete(AnnouncementParameter param) {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try {
                var detail = AnnouncementTableProvider.Detail(param);
                r.Code = AnnouncementTableProvider.Delete(param).ToString(Section.Get.Common.Culture);

                if (!string.IsNullOrEmpty(detail["ImgPath"])) {
                    Io.DeleteFile(System.Web.HttpContext.Current.Server.MapPath($"~{detail["ImgPath"]}"));
                }
            }
            catch (Exception ex) {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }

        [Auth(Name = "編輯新聞", Description = "刪除圖檔")]
        public ActionResult AjaxAnnouncementDeleteFile(AnnouncementParameter param) {
            var result = new JsonNetResult();
            Io.DeleteFile(HttpContext.Server.MapPath($"~{param.Announcement.ImgPath}"));
            result.Data = new { Ststus = "OK" };
            return result;
        }

        [Auth(Name = "編輯新聞", Description = "編輯公告")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxAnnouncementEdit(AnnouncementParameter param) {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try {
                if (param.Forever) {
                    param.Announcement.EndDate = new System.DateTime(9999, 12, 31, 23, 59, 59);
                }
                param.Announcement.StartDate = TimeZoneInfo.ConvertTimeToUtc(param.Announcement.StartDate,
                    Section.Get.Common.TimeZone);
                param.Announcement.EndDate = TimeZoneInfo.ConvertTimeToUtc(param.Announcement.EndDate,
                    Section.Get.Common.TimeZone);

                r.Code = (0L < param.Announcement.AnnouncementId
                    ? AnnouncementTableProvider.Update(param)
                    : AnnouncementTableProvider.Create(param)).ToString(Section.Get.Common.Culture);
                //通知Master更新快取
                //  MasterServer.GetInstance().RefreshGameAnnouncements();
            }
            catch (Exception ex) {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }

        [HttpPost]
        public ActionResult AjaxUploadFiles(string imgFolder = "Announcement") {
            var result = new JsonNetResult();

            //var r = new List<UploadFilesResult>();
            var httpRequest = HttpContext.Request;
            //foreach (string file in httpRequest.Files) {
            var postedFile = httpRequest.Files["file"];

            var remoteAttachDir = $"/Tmp/{imgFolder}/{System.DateTime.Now.ToString("yyyy/MM/dd")}";

            var savePath = HttpContext.Server.MapPath($"~{remoteAttachDir}");
            Io.CreateFolder(savePath);

            if (postedFile == null) {
                return result;
            }
            var extension = Io.GetFileExt(postedFile.FileName).ToLower();
            var filename = $"{Md5.Encrypt(System.DateTime.Now.Ticks.ToString(Section.Get.Common.Culture))}.{extension}";
            var filePath = Path.Combine(savePath, filename);

            if (extension.Equals("png") || extension.Equals("jpg") || extension.Equals("jpeg")) {
                TinyPNG.Instance.Upload(postedFile, filePath);
            }
            else {
                postedFile.SaveAs(filePath);
            }

            result.Data = new { path = remoteAttachDir, name = filename };
            return result;
        }

        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AnnouncementEdit(AnnouncementParameter param) {
            if (0L < param.Announcement.AnnouncementId) {
                param.Announcement.Fill(AnnouncementTableProvider.Detail(param));
            }
            return View(new InternalDataTransferToView { Data = param });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult Announcements(AnnouncementParameter param) {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView {
                    List = AnnouncementTableProvider.List(param),
                    Data = param
                });
        }

        #endregion -- 新聞 --

        #region -- 影音新聞 --

        [Auth(Name = "編輯影音新聞", Description = "刪除")]
        [HttpPost]
        public ActionResult AjaxVideoNewsDelete(VideoNewsParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                var detail = VideoNewsTableProvider.Detail(param);
                r.Code = VideoNewsTableProvider.Delete(param).ToString(Section.Get.Common.Culture);

                if (!string.IsNullOrEmpty(detail["ImgPath"]))
                {
                    Io.DeleteFile(System.Web.HttpContext.Current.Server.MapPath($"~{detail["ImgPath"]}"));
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }

        [Auth(Name = "編輯影音新聞", Description = "刪除圖檔")]
        public ActionResult AjaxVideoNewsDeleteFile(VideoNewsParameter param)
        {
            var result = new JsonNetResult();
            Io.DeleteFile(HttpContext.Server.MapPath($"~{param.VideoNews.ImgPath}"));
            result.Data = new { Ststus = "OK" };
            return result;
        }

        [Auth(Name = "編輯影音新聞", Description = "編輯公告")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxVideoNewsEdit(VideoNewsParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                if (param.Forever)
                {
                    param.VideoNews.EndDate = new System.DateTime(9999, 12, 31, 23, 59, 59);
                }
                param.VideoNews.StartDate = TimeZoneInfo.ConvertTimeToUtc(param.VideoNews.StartDate,
                    Section.Get.Common.TimeZone);
                param.VideoNews.EndDate = TimeZoneInfo.ConvertTimeToUtc(param.VideoNews.EndDate,
                    Section.Get.Common.TimeZone);

                r.Code = (0L < param.VideoNews.VideoNewsId
                    ? VideoNewsTableProvider.Update(param)
                    : VideoNewsTableProvider.Create(param)).ToString(Section.Get.Common.Culture);
                //通知Master更新快取
                //  MasterServer.GetInstance().RefreshGameAnnouncements();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }



        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VideoNewsEdit(VideoNewsParameter param)
        {
            if (0L < param.VideoNews.VideoNewsId)
            {
                param.VideoNews.Fill(VideoNewsTableProvider.Detail(param));
            }
            return View(new InternalDataTransferToView { Data = param });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult VideoNews(VideoNewsParameter param)
        {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView
                {
                    List = VideoNewsTableProvider.List(param),
                    Data = param
                });
        }

        #endregion -- 影音新聞 --

        #region -- 用戶感言 --

        /// <summary>
        /// 用戶感言 清單頁 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult UserComments(UserCommentParameter param) {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView {
                    List = UserCommentTableProvider.List(param),
                    Data = param
                });
        }

        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserCommentEdit(UserCommentParameter param) {
            if (0L < param.Entity.UserCommentId) {
                param.Entity = UserCommentTableProvider.Detail(param);
            }
            return View(new InternalDataTransferToView { Data = param });
        }

        [ValidateInput(false)]
        public ActionResult AjaxUserCommentEdit(UserCommentParameter param) {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try {
                r.Code = (0L < param.Entity.UserCommentId
                    ? UserCommentTableProvider.Update(param)
                    : UserCommentTableProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex) {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }

        [HttpPost]
        public ActionResult AjaxUserCommentDelete(UserCommentParameter param) {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try {
                var detail = UserCommentTableProvider.Detail(param);
                r.Code = UserCommentTableProvider.Delete(param).ToString(Section.Get.Common.Culture);

                if (!string.IsNullOrEmpty(detail.ImgPath)) {
                    Io.DeleteFile(System.Web.HttpContext.Current.Server.MapPath($"~{detail.ImgPath}"));
                }
            }
            catch (Exception ex) {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }


        public ActionResult AjaxUserCommentDeleteFile(UserCommentParameter param) {
            var result = new JsonNetResult();
            Io.DeleteFile(HttpContext.Server.MapPath($"~{param.Entity.ImgPath}"));
            result.Data = new { Ststus = "OK" };
            return result;
        }

        #endregion -- 用戶感言 --

        #region -- 首頁 Banner --

        /// <summary>
        /// 首頁 Banner 清單頁
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult IndexBanners(IndexBannerParameter param) {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView {
                    List = IndexBannerTableProvider.List(param),
                    Data = param
                });
        }

        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IndexBannerEdit(IndexBannerParameter param) {
            if (0L < param.Entity.IndexBannerId) {
                param.Entity = IndexBannerTableProvider.Detail(param);
            }
            return View(new InternalDataTransferToView { Data = param });
        }

        [ValidateInput(false)]
        public ActionResult AjaxIndexBannerEdit(IndexBannerParameter param) {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try {
                r.Code = (0L < param.Entity.IndexBannerId
                    ? IndexBannerTableProvider.Update(param)
                    : IndexBannerTableProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex) {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }

        [HttpPost]
        public ActionResult AjaxIndexBannerDelete(IndexBannerParameter param) {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try {
                var detail = IndexBannerTableProvider.Detail(param);
                r.Code = IndexBannerTableProvider.Delete(param).ToString(Section.Get.Common.Culture);

                if (!string.IsNullOrEmpty(detail.ImgPath)) {
                    Io.DeleteFile(System.Web.HttpContext.Current.Server.MapPath($"~{detail.ImgPath}"));
                }
            }
            catch (Exception ex) {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }


        public ActionResult AjaxIndexBannerDeleteFile(IndexBannerParameter param) {
            var result = new JsonNetResult();
            Io.DeleteFile(HttpContext.Server.MapPath($"~{param.Entity.ImgPath}"));
            result.Data = new { Ststus = "OK" };
            return result;
        }

        #endregion -- 首頁 Banner --

        #region -- 我們的目標圖片置換 --

        /// <summary>
        /// 編輯
        /// </summary>
        /// <returns></returns>

        public ActionResult OurGoalEdit() {
            var obj = OurGoalTableProvider.Detail();
            return View(new InternalDataTransferToView { Data = obj });
        }

        [ValidateInput(false)]
        public ActionResult AjaxOurGoalEdit(OurGoal param) {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try {
                r.Code = (0L < param.OurGoalId
                    ? OurGoalTableProvider.Update(param)
                    : OurGoalTableProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex) {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }

        #endregion -- 我們的目標圖片置換  --

        #region -- 預約訪談 --

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult InterviewAppointments(InterviewAppointmentParameter param) {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView {
                    List = InterviewAppointmentTableProvider.List(param),
                    Data = param
                });
        }

        [HttpPost]
        public ActionResult AjaxInterviewAppointmentDelete(InterviewAppointmentParameter param) {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try {

                r.Code = InterviewAppointmentTableProvider.Delete(param).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex) {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }

        #endregion -- 預約訪談  --

        #region -- 醫學文獻 --

        public ActionResult Literatures(LiteratureParameter param) {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView {
                    List = LiteratureTableProvider.List(param),
                    Data = param
                });
        }

        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LiteratureEdit(LiteratureParameter param) {
            if (0L < param.Entity.LiteratureId) {
                param.Entity.Fill(LiteratureTableProvider.Detail(param));
            }
            return View(new InternalDataTransferToView { Data = param });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxLiteratureEdit(LiteratureParameter param) {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try {

                r.Code = (0L < param.Entity.LiteratureId
                    ? LiteratureTableProvider.Update(param)
                    : LiteratureTableProvider.Create(param)).ToString(Section.Get.Common.Culture);
            }
            catch (Exception ex) {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }


        [HttpPost]
        public ActionResult AjaxLiteratureDelete(LiteratureParameter param) {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try {
                r.Code = LiteratureTableProvider.Delete(param).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex) {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }

        #endregion -- 醫學文獻  --

        #region -- 投資者專區 --

        public ActionResult Investor(InvestorParameter param)
        {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView
                {
                    List = InvestorTableProvider.List(param),
                    Data = param
                });
        }

        /// <summary>
        /// 編輯
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InvestorEdit(InvestorParameter param)
        {
            if (0L < param.Entity.InvestorId)
            {
                param.Entity.Fill(InvestorTableProvider.Detail(param));
            }
            return View(new InternalDataTransferToView { Data = param });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxInvestorEdit(InvestorParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {

                r.Code = (0L < param.Entity.InvestorId
                    ? InvestorTableProvider.Update(param)
                    : InvestorTableProvider.Create(param)).ToString(Section.Get.Common.Culture);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }


        [HttpPost]
        public ActionResult AjaxInvestorDelete(InvestorParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = InvestorTableProvider.Delete(param).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }

        #endregion -- 投資者專區  --

        #region -- 我們的夥伴圖片置換 --

        /// <summary>
        /// 編輯
        /// </summary>
        /// <returns></returns>

        public ActionResult PartnerEdit()
        {
            var obj = PartnerTableProvider.Detail();
            return View(new InternalDataTransferToView { Data = obj });
        }

        [ValidateInput(false)]
        public ActionResult AjaxPartnerEdit(Partner param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = (0L < param.PartnerId
                    ? PartnerTableProvider.Update(param)
                    : PartnerTableProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }

        #endregion -- 我們的目標圖片置換  --

        #region -- 建案清單 --

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public ActionResult buildingList(BuildingCaseParameter param)
        {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView
                {
                    List = BuildingCaseProvider.List(param),
                    Data = param
                });
        }

        /*[ValidateInput(false)]
        public ActionResult AjaxbuildingList(BuildingCaseParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = (0L < param.Entity.Id
                    ? BuildingCaseProvider.Update(param)
                    : BuildingCaseProvider.Create(param)).ToString(Section.Get.Common.Culture);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }*/

        #endregion -- 建案清單  --

        #region -- 建案編輯 --

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult buildingEdit(BuildingCaseParameter param)

        {
            if ((0L < param.Entity.Id))
            {
                param.Entity = BuildingCaseProvider.GetRecordById(param);
            }
            return View(new InternalDataTransferToView { Data = param });
            
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxbuildingEdit(BuildingCaseParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try {
                r.Code = (0L < param.Entity.Id
                    ? BuildingCaseProvider.Update(param)
                    : BuildingCaseProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex) {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }

        [HttpPost]
        public ActionResult AjaxbuildingDelete(BuildingCaseParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = BuildingCaseProvider.Delete(param).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }

        #endregion -- 建案新增  --

        #region -- 合約清單 --
        /// <summary>
        /// 編輯
        /// </summary>
        /// <returns></returns>

        public ActionResult ContractList(ContractParameter param)
        {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView
                {
                    List = ContractProvider.List(param),
                    Data = param
                });
        }
        /*[ValidateInput(false)]
        public ActionResult AjaxItemList(Partner param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = (0L < param.PartnerId
                    ? PartnerTableProvider.Update(param)
                    : PartnerTableProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }*/
        #endregion -- 合約清單  --

        #region -- 合約編輯 --
        [HttpPost]
        public ActionResult ContractEdit(ContractParameter param)
        {
            if ((0L < param.Entity.Id))
            {
                param.Entity = ContractProvider.GetRecordById(param);
                param.Entity.ConsDetailType =  ConstructorProvider.GetConstructorConType(param.Entity.ConstructorId);
            }
            else
            {
                param.Entity.ConsDetailType = ConstructorProvider.GetAllConType();
            }
            param.Entity.ContractDetail = ContractProvider.GetContractDetail(param.Entity.Id); //取明細表

            return View(new InternalDataTransferToView
            {
                //List = ConstructorProvider.GetConstructionTypeList(), //取工程類別總清單
                //ContractProvider.GetDetail(param),
                Data = param
            });
        }

        public ActionResult ContractetDetailList(ContractParameter param)
        {
            param.Entity.ContractDetail = ContractProvider.GetContractDetail(param.Entity.Id);

            return View(new InternalDataTransferToView
            {
                //List = ConstructorProvider.GetConstructionTypeList(), //取工程類別總清單
                //ContractProvider.GetDetail(param),
                Data = param
            });
        }

        //取建案的工程項目
        /*[HttpPost]
        public ActionResult GetConstructionTypeById(ContractParameter param)
        {
            if ((0L < param.Entity.Id))
            {
                param.Entity = ContractProvider.GetRecordById(param);
            }
            return View(new InternalDataTransferToView
            {
                Data = param.Entity.
            });
        }*/

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxContractEdit(ContractParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = (0L < param.Entity.Id
                    ? ContractProvider.Update(param)
                    : ContractProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }

        [HttpPost]
        public ActionResult AjaxContractDelete(ContractParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = ContractProvider.Delete(param).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }
        #endregion -- 合約編輯  --


        #region -- 供應商清單 --
        /// <summary>
        /// 編輯
        /// </summary>
        /// <returns></returns>
        public ActionResult ConstructorList(ConstructorParameter param)
        {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView
                {
                    List = ConstructorProvider.List(param),
                    Data = param
                });
        }
        /*[ValidateInput(false)]
        public ActionResult AjaxItemList(Partner param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = (0L < param.PartnerId
                    ? PartnerTableProvider.Update(param)
                    : PartnerTableProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }*/
        #endregion -- 供應商清單  --

        #region -- 供應商編輯 --
        [HttpPost]
        public ActionResult ConstructorEdit(ConstructorParameter param)

        {
            if ((0L < param.Entity.Id))
            {
                param.Entity = ConstructorProvider.GetRecordById(param);
            }
            //取承包商的承包工程類別清單
            param.Entity.ConsDetailType = ConstructorProvider.GetConstructorConType(param.Entity.Id);

            return View(new InternalDataTransferToView
            {
                List = ConstructorProvider.GetAllConType(), //取工程類別總清單
                //ConstructorProvider.GetDetail(param),
                Data = param
            });

        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxConstructorEdit(ConstructorParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = (0L < param.Entity.Id
                    ? ConstructorProvider.Update(param)
                    : ConstructorProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }

        [HttpPost]
        public ActionResult AjaxConstructorDelete(ConstructorParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = ConstructorProvider.Delete(param).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }
        #endregion -- 供應商編輯  --

        #region -- 項目清單 --
        /// <summary>
        /// 編輯
        /// </summary>
        /// <returns></returns>

        public ActionResult ItemList(ConstructionItemParameter param)
        {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView
                {
                    List = ConstructionItemProvider.List(param),
                    Data = param
                });
        }

        /*[ValidateInput(false)]
        public ActionResult AjaxItemList(Partner param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = (0L < param.PartnerId
                    ? PartnerTableProvider.Update(param)
                    : PartnerTableProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }*/
        #endregion -- 項目清單  --

        #region -- 項目編輯 --
        [HttpPost]
        public ActionResult ItemEdit(ConstructionItemParameter param)

        {
            if ((0L < param.Entity.Id))
            {
                param.Entity = ConstructionItemProvider.GetRecordById(param);
            }
            return View(new InternalDataTransferToView { Data = param });

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxItemEdit(ConstructionItemParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = (0L < param.Entity.Id
                    ? ConstructionItemProvider.Update(param)
                    : ConstructionItemProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }

        [HttpPost]
        public ActionResult AjaxItemDelete(ConstructionItemParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = ConstructionItemProvider.Delete(param).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }
        #endregion -- 項目編輯  --

        #region -- 請款單清單 --

        public ActionResult InvoiceList(InvoiceParameter param)
        {
            param.KeyWord = HttpUtility.UrlDecode(param.KeyWord);
            return View(
                new InternalDataTransferToView
                {
                    List = InvoiceProvider.List(param),
                    Data = param
                });
        }
        /*[ValidateInput(false)]
        public ActionResult AjaxItemList(Partner param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = (0L < param.PartnerId
                    ? PartnerTableProvider.Update(param)
                    : PartnerTableProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }*/
        #endregion -- 請款單清單  --

        #region -- 請款單編輯 --
        [HttpPost]
        public ActionResult InvoiceEdit(InvoiceParameter param)
        {
            if ((0L < param.Entity.Id))
            {
                param.Entity = InvoiceProvider.GetRecordById(param);
            }

            return View(new InternalDataTransferToView
            {
                List = InvoiceProvider.GetDetail(param),
                //InvoiceProvider.GetDetail(param),
                Data = param
            });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxInvoiceEdit(InvoiceParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = (0L < param.Entity.Id
                    ? InvoiceProvider.Update(param)
                    : InvoiceProvider.Create(param)).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11" + ex.Message;
            }
            result.Data = r;
            return result;
        }

        [HttpPost]
        public ActionResult AjaxInvoiceDelete(InvoiceParameter param)
        {
            var result = new JsonNetResult();
            var r = new GeneralResponse();
            try
            {
                r.Code = InvoiceProvider.Delete(param).ToString(Section.Get.Common.Culture);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }
        #endregion -- 請款單編輯  --

    }
}