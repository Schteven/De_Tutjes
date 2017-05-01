using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using De_Tutjes.Models;

namespace De_Tutjes.Areas.Administration.Controllers
{
    public class CalendarController : Controller
    {
        private DeTutjesContext db = new DeTutjesContext();

        public class calDay
        {
            public string id { get; set; }
            public string title { get; set; }
            public string date { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string url { get; set; }

            public bool allDay { get; set; }
        }

        // GET: Administration/Calendar
        public ActionResult Overview()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetToddlersOfPeriod(string start, string end)
        {
            DateTime calStartDate = Convert.ToDateTime(start);
            DateTime calEndDate = Convert.ToDateTime(end);
            int days = Convert.ToInt32((calEndDate - calStartDate).TotalDays);

            List<calDay> calDayList = new List<calDay>();
            List<AgreedDays> agreedDaysOfToddlers = db.AgreedDays.ToList();

            List<DateTime> calPeriod = new List<DateTime>();
            List<DateTime> agreedDaysPeriod = new List<DateTime>();

            for (int i = 0; i <= days; i++ )
            {
                calPeriod.Add(calStartDate.AddDays(i));
            }
            
            foreach(DateTime date in calPeriod)
            {
                int toddlers = 0;
                switch (date.DayOfWeek.ToString())
                {
                    case "Monday":
                        toddlers = db.AgreedDays.Where(d => (d.StartDate <= date) && (d.EndDate >= date)).Where(m => m.Monday == true).Count();
                        break;
                    case "Tuesday":
                        toddlers = db.AgreedDays.Where(d => (d.StartDate <= date) && (d.EndDate >= date)).Where(m => m.Tuesday == true).Count();
                        break;
                    case "Wednesday":
                        toddlers = db.AgreedDays.Where(d => (d.StartDate <= date) && (d.EndDate >= date)).Where(m => m.Wednesday == true).Count();
                        break;
                    case "Thursday":
                        toddlers = db.AgreedDays.Where(d => (d.StartDate <= date) && (d.EndDate >= date)).Where(m => m.Thursday == true).Count();
                        break;
                    case "Friday":
                        toddlers = db.AgreedDays.Where(d => (d.StartDate <= date) && (d.EndDate >= date)).Where(m => m.Friday == true).Count();
                        break;
                }
                if (toddlers != 0)
                {
                    calDay newCalDay = new calDay
                    {
                        id = "",
                        title = "Aantal: " + toddlers,
                        start = date.ToString("s"),
                        allDay = false
                    };

                    calDayList.Add(newCalDay);
                }
            }

            var rows = calDayList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
    }
}