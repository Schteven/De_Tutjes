using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using De_Tutjes.Models;
using System.Data.Entity;

namespace De_Tutjes.Areas.Administration.Controllers
{
    public class CalendarController : Controller
    {
        private DeTutjesContext db = new DeTutjesContext();

        public class calDay
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
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
            ICollection<AgreedDays> agreedDaysOnThisDay = new List<AgreedDays>();

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
                        agreedDaysOnThisDay = db.AgreedDays.Where(d => (d.StartDate <= date) && (d.EndDate >= date)).Where(m => m.Monday == true).Include(t => t.Toddler.Person).ToList();
                        break;
                    case "Tuesday":
                        agreedDaysOnThisDay = db.AgreedDays.Where(d => (d.StartDate <= date) && (d.EndDate >= date)).Where(m => m.Tuesday == true).Include(t => t.Toddler.Person).ToList();
                        break;
                    case "Wednesday":
                        agreedDaysOnThisDay = db.AgreedDays.Where(d => (d.StartDate <= date) && (d.EndDate >= date)).Where(m => m.Wednesday == true).Include(t => t.Toddler.Person).ToList();
                        break;
                    case "Thursday":
                        agreedDaysOnThisDay = db.AgreedDays.Where(d => (d.StartDate <= date) && (d.EndDate >= date)).Where(m => m.Thursday == true).Include(t => t.Toddler.Person).ToList();
                        break;
                    case "Friday":
                        agreedDaysOnThisDay = db.AgreedDays.Where(d => (d.StartDate <= date) && (d.EndDate >= date)).Where(m => m.Friday == true).Include(t => t.Toddler.Person).ToList();
                        break;
                }
                toddlers = agreedDaysOnThisDay.Count();
                string toddlersOnThisDay = "";
                foreach (AgreedDays agreedDay in agreedDaysOnThisDay)
                {
                    int i = 1;
                    int hr = 0;
                    string h = "00";
                    string sm = "00";
                    string em = "30";

                    toddlersOnThisDay += agreedDay.Toddler.Person.FirstName;
                    toddlersOnThisDay += "<br>";

                    string startTime = h + ":" + sm + ":00";
                    string endTime = h + ":" + em + ":00";

                    calDay newCalDay = new calDay
                    {
                        id = ""+i,
                        title = agreedDay.Toddler.Person.FirstName,
                        //description = toddlersOnThisDay,
                        start = date.ToString("s").Replace("00:00:00", startTime),
                        end = date.ToString("s").Replace("00:00:00", endTime),
                        allDay = false
                    };

                    calDayList.Add(newCalDay);

                    if (i % 2 != 0)
                    {
                        hr++;
                        h = (hr < 10) ? "0" + hr : "" + hr ;
                        sm = "00";
                        em = "30";
                    }
                    else
                    {
                        sm = "30";
                        em = "00";
                    }
                    i++;

                } 
                if (toddlers != 0)
                {
                    calDay newCalDay = new calDay
                    {
                        id = "0",
                        title = "Aantal: " + toddlers,
                        //description = toddlersOnThisDay,
                        start = date.ToString("s"),
                        allDay = true
                    };

                    calDayList.Add(newCalDay);
                }
            }

            var rows = calDayList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
    }
}