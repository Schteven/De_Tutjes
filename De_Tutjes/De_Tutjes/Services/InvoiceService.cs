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
        /// This function will build the invoice for a specific toddler. It will only save if there is a need for an Invoice
        /// </summary>
        /// <param name="toddler"></param>
        private void BuildInvoice(Toddler toddler)
        {
            Invoice lastInvoice = getLastInvoice(toddler);
            DateTime startdate = lastInvoice.CreationDate; // Startdate to calculate the extra days and closing days
            DateTime enddate = DateTime.Now; // This is the new CreationDate

            DateTime lastInvoicePeriod = new DateTime(lastInvoice.Year, lastInvoice.Month, 1);
            DateTime newInvoicePeriod = lastInvoicePeriod.AddMonths(1); // This is done to handle december of a year and jump to the new year.

            int ndnm = getAllAgreedDaysInMonth(toddler, newInvoicePeriod.Month, newInvoicePeriod.Year).Count;
            int edtm = getAllExtraDaysinPeriod(toddler, startdate, enddate).Count;
            int dcctm = getAllClosingDaysInPeriod(toddler, startdate, enddate).Count;
            
            if(ndnm != 0 || edtm != 0 || dcctm != 0)
            {
                Invoice newInvoice = new Invoice();
                newInvoice.CreationDate = enddate; // setting the new CreationDate
                newInvoice.Month = newInvoicePeriod.Month;
                newInvoice.Year = newInvoicePeriod.Year;
                newInvoice.Toddler = toddler;
                newInvoice.NormalDaysNextMonth = ndnm;
                newInvoice.ExtraDaysThisMonth = edtm;
                newInvoice.DayCareClosedThisMonth = dcctm;
                newInvoice.Payed = false;
                newInvoice.HasSibling = false;
                newInvoice.TotalAmount = 0;
                //load invoice to db
            }

        }

        /// <summary>
        /// Create invoices for active child which come next month and/or have not payed extra days since last Invoice
        /// </summary>
        /// <param name="toddler"></param>
        public void CreateInvoices()
        {
            List<Toddler> allActiveToddlers = db.Toddlers.Where(at => at.Person.Active == true).ToList();

            foreach (Toddler t in allActiveToddlers)
            {
                BuildInvoice(t);
            }
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
            //TODO : Get last Invoice from DB

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
        private List<DateTime> getAllAgreedDaysInMonth(Toddler t, int year, int month)
        {
            List<DateTime> ADays = new List<DateTime>();
            AgreedDays ADnextMonth = agreedDaysThatApply(t, year, month);
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
        
        /// <summary>
        /// Returns all the extra days the child has come, not according to the AgreedDays.
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        private List<DateTime> getAllExtraDaysinPeriod(Toddler t, DateTime startdate, DateTime enddate)
        {
            List<DateTime> extraDays = new List<DateTime>();
            // TODO: DB get the days from RegisteredDays where CheckedIN == true and ExtraDay == true.
            
            return extraDays;
        }

        /// <summary>
        /// Returns all the Closing Days that the child couldn't come, but the day was already payed.
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        private List<DateTime> getAllClosingDaysInPeriod(Toddler t, DateTime startdate, DateTime enddate)
        {
            List<RegisteredDay> registeredDaysChildHasntCome = new List<RegisteredDay>();
            // TODO: DB get the days where rb.CheckedIn == false.
            List<VacationDay> vacationDays = db.VacationDays.Where(vd => (vd.Day >= startdate)&&(vd.Day <= enddate)).ToList();
            List<DateTime> allClosingDays = new List<DateTime>();
            if(registeredDaysChildHasntCome != null && vacationDays != null)
            {
                foreach (RegisteredDay rb in registeredDaysChildHasntCome)
                {
                    foreach (VacationDay vd in vacationDays)
                    {
                        if(rb.DayInDaycare == vd.Day)
                        {
                            allClosingDays.Add(rb.DayInDaycare);
                        }
                    }
                }
            }
            
            

            return allClosingDays;
        }

        /// <summary>
        /// Return the ONLY AgreedDays where the NextMonth falls in
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private AgreedDays agreedDaysThatApply(Toddler t, int year, int month)
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

        private string buildOGM(int childid, int month, int year)
        {
            string challenge = "";
            if(childid < 100)
            {
                challenge += "0";
            }
            challenge += childid.ToString();
            challenge += month.ToString();
            

            return null;
        }
    }
}