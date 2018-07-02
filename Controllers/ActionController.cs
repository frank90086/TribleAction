using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TimeZoneConverter;
using TribleAction.Models;
using TribleAction.Models.ApiModels;

namespace TribleAction.Controllers
{
    public class ActionController : Controller
    {
        private TimeZoneInfo _customerTimezone;
        private TimeZoneInfo _systemTimezone;
        HttpClientHelper _client;
        private List<DateTimeOffset> timeTable = new List<DateTimeOffset>()
        {
            new DateTimeOffset(2018, 7, 7, 9, 0, 00, new TimeSpan(8, 0, 0)),
            new DateTimeOffset(2018, 7, 7, 14, 0, 00, new TimeSpan(8, 0, 0)),
            new DateTimeOffset(2018, 7, 7, 17, 0, 00, new TimeSpan(8, 0, 0))
        };
        public ActionController(IOptions<ApiBaseUrl> setting)
        {
            _client = new HttpClientHelper(setting);
            _customerTimezone = TimeZoneInfo.FindSystemTimeZoneById(TZConvert.IanaToWindows("Asia/Taipei"));
            _systemTimezone = TimeZoneInfo.Local;
        }

        [HttpGet]
        public IActionResult Lottery()
        {
            ViewData["Message"] = "請輸入你的小隊代碼：";
            return View();
        }

        [HttpPost]
        public JsonResult LotteryNumberList(string teamId)
        {
            if (!String.IsNullOrEmpty(teamId))
            {
                var response = _client.Get<ReturnResult<ICollection<int>> > ("Action/" + teamId);
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    var numlist = response.Result;
                    return Json(new { info = true, numlist = numlist });
                }
                else
                    return Json(new { info = false, numlist = new { }, message = "代碼錯誤，請重新查詢" });
            }
            else
                return Json(new { info = false, numlist = new { }, message = "代碼錯誤，請重新查詢" });
        }

        [HttpPost]
        public JsonResult ChooseNumber(string teamId, int number)
        {
            if (number != 0)
            {
                var response = _client.Post<ReturnResult<bool>, ChooseNumberApiModel>("Action/Choose", new ChooseNumberApiModel() { TeamId = teamId, Number = number });
                if (response.HttpStatusCode == HttpStatusCode.OK)
                    return Json(new { info = true, message = "選取號碼 - " + number + "，操作成功！" });
                else if (response.HttpStatusCode == HttpStatusCode.Continue)
                    return Json(new { info = false, message = "此時段已選擇，敬請期待下次開放" });
                else
                    return Json(new { info = false, message = "發生錯誤，請重新操作" });
            }
            else
                return Json(new { info = false, message = "發生錯誤，請重新操作" });
        }

        [HttpPost]
        public IActionResult Lottery(string teamId, int num)
        {
            return View();
        }

        [HttpGet]
        public JsonResult CheckTime()
        {
            DateTimeOffset now = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, _customerTimezone);
            foreach (DateTimeOffset check in timeTable)
            {
                if (checkTime(now, check))
                    return Json(new { info = true });
            }
            return Json(new { info = false, message = "尚未開放大樂透，請關注Line@相關資訊" });
        }

        private bool checkTime(DateTimeOffset now, DateTimeOffset check)
        {
            if (DateTimeOffset.Compare(now, check) < 0)
                return false;
            check = check.AddHours(2);
            if (DateTimeOffset.Compare(now, check) <= 0)
                return true;
            else
                return false;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}