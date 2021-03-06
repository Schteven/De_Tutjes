﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace De_Tutjes.Functions
{
    public class SchoolHolidays
    {

        private static DateTime now = DateTime.Now;
        private static DateTime zero = new DateTime(2000, 1, 1);
        //debug
        public static DateTime birthday { get; set; }
        //private static DateTime birthday = new DateTime(2015, 7, 1);
        //public static DateTime birthday { get; set; }

        private static DateTime readyForSchool;

        public SchoolHolidays(DateTime birthdate)
        {
            birthday = birthdate;
        }

        private static DateTime ReadyForSchool(DateTime birthday)
        {
            // SETTINGOPTION!!!
            int MonthsAfterBirth = 30;
            DateTime Birthday = birthday;
            DateTime ReadyForSchool = Birthday.AddMonths(MonthsAfterBirth);
            readyForSchool = ReadyForSchool;

            return ReadyForSchool;
        }

                /*
                string[] date = singleDate.Split('/');
                int day = Int32.Parse(date[0]);
                int month = Int32.Parse(date[1]);
                int year = Int32.Parse(date[2]);
                DateTime easter = new DateTime(year, month, day);
                */

        private static DateTime FirstEaster()
        {
            string[] dates = System.IO.File.ReadAllLines(HostingEnvironment.MapPath(@"~/App_Data/easter.txt"));
            foreach (string date in dates)
            {
                DateTime easter = Convert.ToDateTime(date);
                if (easter.Year == readyForSchool.Year)
                {
                    return easter;
                }
            }
            return zero;
        }

        public DateTime CanGoToSchoolFrom()
        {
            readyForSchool = ReadyForSchool(birthday);
            DateTime canGoOn = zero;
            switch (readyForSchool.Month)
            {
                case 1:
                    if (readyForSchool <= FirstDayInJanuary())
                    {
                        canGoOn = FirstDayInJanuary();
                    }
                    else
                    {
                        canGoOn = FirstDayInFebruary();
                    }
                    break;
                case 2:
                    if (readyForSchool <= FirstDayInFebruary())
                    {
                        canGoOn = FirstDayInFebruary();
                    }
                    else
                    {
                        canGoOn = FirstDayAfterCrocus();
                    }
                    break;
                case 3:
                    if (readyForSchool <= FirstDayAfterCrocus())
                    {
                        canGoOn = FirstDayAfterCrocus();
                    }
                    else
                    {
                        canGoOn = FirstDayAfterEaster();
                    }
                    break;
                case 4:
                    if (readyForSchool <= FirstDayAfterEaster())
                    {
                        canGoOn = FirstDayAfterEaster();
                    }
                    else
                    {
                        canGoOn = FirstDayAfterAscension();
                    }
                    break;
                case 5:
                    if (readyForSchool <= FirstDayAfterAscension())
                    {
                        canGoOn = FirstDayAfterAscension();
                    }
                    else
                    {
                        canGoOn = FirstDayInSeptember();
                    }
                    break;
                case 6:
                case 7:
                case 8:
                    canGoOn = FirstDayInSeptember();
                    break;
                case 9:
                    if (readyForSchool <= FirstDayInSeptember())
                    {
                        canGoOn = FirstDayInSeptember();
                    }
                    else
                    {
                        canGoOn = FirstDayAfterFall();
                    }
                    break;
                case 10:
                    canGoOn = FirstDayAfterFall();
                    break;
                case 11:
                    if (readyForSchool <= FirstDayAfterFall())
                    {
                        canGoOn = FirstDayAfterFall();
                    }
                    else
                    {
                        canGoOn = FirstDayAfterChristmas();
                    }
                    break;
                case 12:
                    canGoOn = FirstDayAfterChristmas();
                    break;
                default:
                    break;
            }

            return canGoOn;
        }
        // First schoolday of September
        public static DateTime FirstDayInSeptember()
        {
            DateTime septemberFirst = new DateTime(readyForSchool.Year, 9, 1);
            switch (septemberFirst.ToString("dddd"))
            {
                case "Saturday":
                    septemberFirst = new DateTime(readyForSchool.Year, 9, 3);
                    break;
                case "Sunday":
                    septemberFirst = new DateTime(readyForSchool.Year, 9, 2);
                    break;
                default:
                    septemberFirst = new DateTime(readyForSchool.Year, 9, 1);
                    break;
            }
            return septemberFirst;
        }
        // Holiday at Fall (1st of November) -- 1 week
        // Case Sunday is 1st: Holiday starts first Monday after.
        // Case MON/SAT has 1st: Holiday starts first Monday before. (1/11 is on Monday -> start holiday)
        public static DateTime FirstDayAfterFall()
        {
            DateTime novemberFirst = new DateTime(readyForSchool.Year, 11, 1);
            switch (novemberFirst.ToString("dddd"))
            {
                case "Monday":
                    novemberFirst = new DateTime(readyForSchool.Year, 11, 8);
                    break;
                case "Tuesday":
                    novemberFirst = new DateTime(readyForSchool.Year, 11, 7);
                    break;
                case "Wednesday":
                    novemberFirst = new DateTime(readyForSchool.Year, 11, 6);
                    break;
                case "Thursday":
                    novemberFirst = new DateTime(readyForSchool.Year, 11, 5);
                    break;
                case "Friday":
                    novemberFirst = new DateTime(readyForSchool.Year, 11, 4);
                    break;
                case "Saturday":
                    novemberFirst = new DateTime(readyForSchool.Year, 11, 3);
                    break;
                case "Sunday":
                    novemberFirst = new DateTime(readyForSchool.Year, 11, 2);
                    break;
                default:
                    novemberFirst = new DateTime(readyForSchool.Year, 11, 1);
                    break;
            }
            return novemberFirst;
        }

        // Holiday at Christmas -- 2 weeks
        // Case Christmas is on SAT/SUN: Holiday starts at first Monday after.
        // Case Christmas is on MON/FRI: Holiday starts first Monday before. (25/12 is on Monday -> start holiday)
        public static DateTime FirstDayAfterChristmas()
        {
            DateTime christmas = new DateTime(readyForSchool.Year, 12, 25);
            DateTime firstDayAfterHoliday;
            switch (christmas.ToString("dddd"))
            {
                case "Monday":
                    firstDayAfterHoliday = christmas.AddDays(14);
                    break;
                case "Tuesday":
                    firstDayAfterHoliday = christmas.AddDays(13);
                    break;
                case "Wednesday":
                    firstDayAfterHoliday = christmas.AddDays(12);
                    break;
                case "Thursday":
                    firstDayAfterHoliday = christmas.AddDays(11);
                    break;
                case "Friday":
                    firstDayAfterHoliday = christmas.AddDays(10);
                    break;
                case "Saturday":
                    firstDayAfterHoliday = christmas.AddDays(16);
                    break;
                case "Sunday":
                    firstDayAfterHoliday = christmas.AddDays(15);
                    break;
                default:
                    firstDayAfterHoliday = christmas.AddDays(14);
                    break;
            }
            return firstDayAfterHoliday;
        }
        public static DateTime FirstDayInJanuary()
        {
            DateTime januaryFirst = new DateTime(readyForSchool.Year, 1, 1);
            DateTime firstDayInJanuary;
            switch (januaryFirst.ToString("dddd"))
            {
                case "Monday":
                    firstDayInJanuary = januaryFirst.AddDays(7);
                    break;
                case "Tuesday":
                    firstDayInJanuary = januaryFirst.AddDays(6);
                    break;
                case "Wednesday":
                    firstDayInJanuary = januaryFirst.AddDays(5);
                    break;
                case "Thursday":
                    firstDayInJanuary = januaryFirst.AddDays(4);
                    break;
                case "Friday":
                    firstDayInJanuary = januaryFirst.AddDays(3);
                    break;
                case "Saturday":
                    firstDayInJanuary = januaryFirst.AddDays(9);
                    break;
                case "Sunday":
                    firstDayInJanuary = januaryFirst.AddDays(8);
                    break;
                default:
                    firstDayInJanuary = januaryFirst.AddDays(9);
                    break;
            }
            return firstDayInJanuary;
        }

        // No Holiday, just a first schoolday
        public static DateTime FirstDayInFebruary()
        {
            DateTime februaryFirst = new DateTime(readyForSchool.Year, 2, 1);
            switch (februaryFirst.ToString("dddd"))
            {
                case "Saturday":
                    februaryFirst = new DateTime(readyForSchool.Year, 2, 3);
                    break;
                case "Sunday":
                    februaryFirst = new DateTime(readyForSchool.Year, 2, 2);
                    break;
                default:
                    februaryFirst = new DateTime(readyForSchool.Year, 2, 1);
                    break;
            }
            return februaryFirst;
        }

        // Holiday at Crocus -- 1 week
        // 7th Monday before Easter(monday)
        public static DateTime FirstDayAfterCrocus()
        {
            DateTime EndOfYear = new DateTime(readyForSchool.Year, 12, 31);
            bool LeapYear;
            if (EndOfYear.DayOfYear == 366)
            {
                LeapYear = true;
            }
            else
            {
                LeapYear = false;
            }
            DateTime easterMonday;
            if (LeapYear)
            {
                easterMonday = FirstEaster().AddDays(-50);
            }
            else
            {
                easterMonday = FirstEaster().AddDays(-49);
            }
            DateTime firstDayAfterCrocus = easterMonday.AddDays(7);

            return firstDayAfterCrocus;
        }

        // Holiday at Easter (date from file) -- 2 weeks
        // Easter is in March: Holiday starts with Easter in first weekend.
        // Easter is after April 15th: Holiday end with Easter.
        // Easter is before April 15th: Holiday starts first Monday of April.
        public static DateTime FirstDayAfterEaster()
        {
            DateTime firstDayAfterEaster = FirstEaster().AddDays(14);
            switch (FirstEaster().Month)
            {
                case 3:
                    firstDayAfterEaster = FirstEaster().AddDays(15);
                    break;
                case 4:
                    if (FirstEaster().Day > 15)
                    {
                        firstDayAfterEaster = FirstEaster().AddDays(2);
                    }
                    else
                    {
                        switch (FirstEaster().ToString("dddd"))
                        {
                            case "Monday":
                                firstDayAfterEaster = FirstEaster().AddDays(14);
                                break;
                            case "Tuesday":
                                firstDayAfterEaster = FirstEaster().AddDays(13);
                                break;
                            case "Wednesday":
                                firstDayAfterEaster = FirstEaster().AddDays(12);
                                break;
                            case "Thursday":
                                firstDayAfterEaster = FirstEaster().AddDays(11);
                                break;
                            case "Friday":
                                firstDayAfterEaster = FirstEaster().AddDays(10);
                                break;
                            case "Saturday":
                                firstDayAfterEaster = FirstEaster().AddDays(16);
                                break;
                            case "Sunday":
                                firstDayAfterEaster = FirstEaster().AddDays(15);
                                break;
                            default:
                                firstDayAfterEaster = FirstEaster().AddDays(14);
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
            return firstDayAfterEaster;

        }

        // Holiday at Ascension -- always Thursday (friday included)
        // 10 days before Pentecost(7th Sunday after Easter) 39 days after easter
        public static DateTime FirstDayAfterAscension()
        {
            DateTime AscensionDay = FirstEaster().AddDays(39);
            DateTime firstDayAfterAscension = AscensionDay.AddDays(4);

            return firstDayAfterAscension;
        }
    }
}