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
            new DateTimeOffset(2018, 7, 7, 10, 0, 00, new TimeSpan(8, 0, 0)),
            new DateTimeOffset(2018, 7, 7, 13, 0, 00, new TimeSpan(8, 0, 0)),
            new DateTimeOffset(2018, 7, 7, 16, 0, 00, new TimeSpan(8, 0, 0))
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
                    return Json(new { info = false, numlist = new { } });
            }
            else
                return Json(new { info = false, numlist = new { } });
        }

        [HttpPost]
        public JsonResult ChooseNumber(string teamId, int number)
        {
            if (number != 0)
            {
                var response = _client.Post<ReturnResult<bool>, ChooseNumberApiModel>("Action/Choose", new ChooseNumberApiModel() { TeamId = teamId, Number = number });
                if (response.HttpStatusCode == HttpStatusCode.OK)
                    return Json(new { info = true });
                else
                    return Json(new { info = false });
            }
            else
                return Json(new { info = false });
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
            return Json(new { info = false , now = now});
        }

        private bool checkTime(DateTimeOffset now, DateTimeOffset check)
        {
            if (DateTimeOffset.Compare(now, check) < 0)
                return false;
            check.AddHours(1);
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