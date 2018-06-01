using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TribleAction.Models;
using TribleAction.Models.ApiModels;

namespace TribleAction.Controllers
{
    public class ActionController : Controller
    {
        HttpClientHelper _client;
        public ActionController(IOptions<ApiBaseUrl> setting)
        {
            _client = new HttpClientHelper(setting);
        }

        [HttpGet]
        public IActionResult Lottery()
        {
            ViewData["Message"] = "請選擇一個樂透號碼：";
            return View();
        }

        [HttpPost]
        public JsonResult LotteryNumberList(string teamId)
        {
            if (!String.IsNullOrEmpty(teamId))
            {
                var response = _client.Get<ReturnResult<Team>>("Action/"+teamId);
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    var numlist = response.Result.TeamLottery.Select(x => new { num = x.Number}).ToList();
                    return Json(new { info = true, numlist = numlist });
                }
                else
                return Json(new { info = false, numlist = new { } });
            }
            else
                return Json(new { info = false, numlist = new { } });
        }

        [HttpPost]
        public IActionResult Lottery(string teamId, int num)
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}