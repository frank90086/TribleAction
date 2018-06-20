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
            ViewData["Message"] = "請輸入你的小隊代碼：";
            return View();
        }

        [HttpPost]
        public JsonResult LotteryNumberList(string teamId)
        {
            if (!String.IsNullOrEmpty(teamId))
            {
                var response = _client.Get<ReturnResult<ICollection<int>>>("Action/" + teamId);
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
                var response = _client.Post<ReturnResult<bool>, ChooseNumberApiModel>("Action/Choose", new ChooseNumberApiModel(){TeamId = teamId, Number = number});
                if (response.HttpStatusCode == HttpStatusCode.OK)
                return Json(new { info = true});
                else
                    return Json(new { info = false});
            }
            else
                return Json(new { info = false});
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