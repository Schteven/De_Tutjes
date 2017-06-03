using De_Tutjes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace De_Tutjes.Services
{
    /*
        Calculating an invoice
        Amount of Days the child has to come next month
        +
        Amount of Extra Days the child was registered since last Invoice
        -
        Amount of Closed Days since last Invoice
        =
        Total amount of Days that are going to be payed
    */


    public class InvoiceService
    {
        private DeTutjesContext db = new DeTutjesContext();
        public Invoice Invoice { get; set; }
        public Toddler Toddler { get; set; }
        public AgreedDays AgreedDaysPeriodThatApplies { get; set; }

        public InvoiceService()
        {

        }
        /// <summary>
        /// This function will build the invoice for a specific toddler. It will call private functions to get the days.
        /// </summary>
        /// <param name="toddler"></param>
        public void BuildInvoice(Toddler toddler)
        {
            Invoice lastInvoice = getLastInvoice(toddler);
            DateTime startdate = lastInvoice.CreationDate; // Startdate to calculate the extra days and closing days
            DateTime enddate = DateTime.Now; // This is the new CreationDate

            DateTime lastInvoicePeriod = new DateTime(lastInvoice.Year, lastInvoice.Month, 1);
            DateTime newInvoicePeriod = lastInvoicePeriod.AddMonths(1); // This is done to handle december of a year and jump to the new year.

            Invoice newInvoice = new Invoice();
            newInvoice.CreationDate = enddate; // setting the new CreationDate
            newInvoice.Month = newInvoicePeriod.Month;
            newInvoice.Year = newInvoicePeriod.Year;
            newInvoice.Toddler = toddler;
            newInvoice.NormalDaysNextMonth = getAllAgreedDaysInMonth(lastInvoice.Year, ++lastInvoice.Month).Count;
            newInvoice.ExtraDaysThisMonth = 0;
            newInvoice.DayCareClosedThisMonth = 0;
            
            
        }
        /// <summary>
        /// This function will calculate the total amount of days the toddler has to pay.
        /// </summary>
        // How many days will have to be payed 
        public int CalculateAmountOfPayedDays()
        {
            int totalDays = 0;
            // add the days for next month
            // add the extra days for last month

            // subtract the closed days for last month
            return totalDays;
        }

        // Return the last invoice that was made for a specific toddler
        private Invoice getLastInvoice(Toddler t)
        {
            Invoice lastInvoice = new Invoice();
            lastInvoice.CreationDate = new DateTime(2017, 5, 30);
            lastInvoice.Month = 6;
            lastInvoice.Year = 2017;
            //Get last Invoice from DB

            return lastInvoice;
        }

        // Generating all DateTimes that are in the next month
        private IEnumerable<DateTime> getAllDaysInMonth(int year, int month)
        {
            int days = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
            {
                yield return new DateTime(year, month, day);
            }
        }
        /// <summary>
        /// This function will generate a list of all the days the toddler will come next month.
        /// Based on the year and the month integers that are provided in the parameters.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>List of all the days that the child has to come</returns>
        /*
            Get the Agreed days for nex month
            Generate every day in the next month
            Check if the toddler has to come that day 
            Add that day to the list
        */

        private List<DateTime> getAllAgreedDaysInMonth(int year, int month)
        {
            List<DateTime> ADays = new List<DateTime>();
            AgreedDays ADnextMonth = agreedDaysThatApply(year, month);
            int days = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
            {
                DateTime thisDay = new DateTime(year, month, day);
                if(thisDay >= ADnextMonth.StartDate && thisDay <= ADnextMonth.EndDate)
                {
                    switch (thisDay.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            if(ADnextMonth.Monday == true)
                            {
                                ADays.Add(thisDay);
                            }
                            break;
                        case DayOfWeek.Tuesday:
                            if (ADnextMonth.Tuesday == true)
                            {
                                ADays.Add(thisDay);
                            }
                            break;
                        case DayOfWeek.Wednesday:
                            if (ADnextMonth.Wednesday == true)
                            {
                                ADays.Add(thisDay);
                            }
                            break;
                        case DayOfWeek.Thursday:
                            if (ADnextMonth.Thursday == true)
                            {
                                ADays.Add(thisDay);
                            }
                            break;
                        case DayOfWeek.Friday:
                            if (ADnextMonth.Friday == true)
                            {
                                ADays.Add(thisDay);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return ADays;
        }

        // How many days will the toddler come next month
        // This is the next month after the last Invoice month
        // We will check for every day there is in the next month if the agreed days apply to them
        // get all AgreedDays for the toddler
        // check each day in the next month if its below End Date of AD
        // then check if its the day that the toddler is coming.

        private int amountOfDaysNextMonth(int year, int month)
        {
            AgreedDays ADnextMonth = agreedDaysThatApply(year, month);



            return 0;
        }

        // Return the ONLY AgreedDays where the NextMonth falls in
        private AgreedDays agreedDaysThatApply(int year, int month)
        {
            List<AgreedDays> allAgreedDaysForToddler = new List<AgreedDays>(); //TODO return from database
            
            foreach (AgreedDays AD in allAgreedDaysForToddler)
            {
                if (AD.StartDate <= new DateTime(year, month, 1) && AD.EndDate.Month >= new DateTime(year, month, 1).Month)
                {
                    return AD;
                }
            }
            return null;
        }

    }
}