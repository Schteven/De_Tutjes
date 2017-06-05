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
        Amount of Extra Days the child was registered since last Invoice (!)
        -
        Amount of Closed Days since last Invoice (!)
        =
        Total amount of Days that are going to be payed
    */


    public class InvoiceService
    {
        private DeTutjesContext db = new DeTutjesContext();
        private double priceDay = 25;
        private double priceDaySibling = 24;
        
        public InvoiceService()
        {

        }

        // PUBLIC FUNCTIONS

        /// <summary>
        /// Create the first Invoice for the new Child.
        /// </summary>
        /// <param name="toddlerId"></param>
        public void CreateFirstInvoice(string toddlerId)
        {
            Toddler t = db.Toddlers.Find(toddlerId);
            AgreedDays ad = db.AgreedDays.Where(ads => ads.Toddler == t).OrderBy(add => add.StartDate).First();

            Invoice firstInvoice = new Invoice();
            firstInvoice.CreationDate = DateTime.Now;
            firstInvoice.Month = ad.StartDate.Month;
            firstInvoice.Year = ad.StartDate.Year;
            firstInvoice.Toddler = t;
            firstInvoice.NormalDaysNextMonth = getAllAgreedDaysInMonth(t, firstInvoice.Month, firstInvoice.Year).Count;
            firstInvoice.ExtraDaysThisMonth = 0;
            firstInvoice.DayCareClosedThisMonth = 0;
            firstInvoice.Payed = false;
            firstInvoice.HasSibling = false;
            firstInvoice.TotalAmount = 0;
            db.Invoices.Add(firstInvoice);
            db.SaveChanges();

        }
        
        /// <summary>
        /// Creates invoices for all active childs which come next month and/or have not payed extra days since last Invoice
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
        public double CalculateTotalPrice(int invoiceid, Boolean hasSibling)
        {
            Invoice invoice = db.Invoices.Find(invoiceid);
            int totalDays = 0;
            totalDays += invoice.NormalDaysNextMonth;
            totalDays += invoice.ExtraDaysThisMonth;
            totalDays -= invoice.DayCareClosedThisMonth;
            double totalPrice;
            if (hasSibling) totalPrice = totalDays * priceDaySibling;
            else totalPrice = totalDays * priceDay;

            return totalPrice;
        }

        /// <summary>
        /// Returns all the invoices for a given month and year
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<Invoice> GetAllInvoicesForMonth(int month, int year)
        {
            return db.Invoices.Where(i => (i.Month == month) && (i.Year == year)).ToList();;
        }

        /// <summary>
        /// Returns all the invoices for a given toddlerid
        /// </summary>
        /// <param name="toddlerid"></param>
        /// <returns></returns>
        public List<Invoice> GetAllInvoicesForToddler(int toddlerid)
        {
            return db.Invoices.Where(i => (i.Toddler.PersonId == toddlerid)).ToList();
        }

        /// <summary>
        /// Returns the invoice when you give an OGM ( string = "+++012/3456/78910+++" )
        /// </summary>
        /// <param name="ogm"></param>
        /// <returns></returns>
        public Invoice GetInvoiceWithOGM(string ogm)
        {
            Invoice invoice = db.Invoices.Where(i => i.OGM == ogm).First();
            return invoice;
        }

        /// <summary>
        /// Pays the invoice for the specific OGM ( string = "+++012/3456/78910+++" )
        /// </summary>
        /// <param name="ogm"></param>
        public void PayInvoiceWithOGM(string ogm)
        {
            Invoice invoice = db.Invoices.Where(i => i.OGM == ogm).First();
            invoice.Payed = true;
            db.SaveChanges();
        }

        /// <summary>
        /// Pays the invoice for the given invoice id
        /// </summary>
        /// <param name="invoiceId"></param>
        public void PayInvoice(int invoiceId)
        {
            Invoice invoice = db.Invoices.Find(invoiceId);
            invoice.Payed = true;
            db.SaveChanges();
        }

        // PRIVATE FUNCTIONS

        // Returns the last invoice that was made for a specific toddler
        private Invoice getLastInvoice(Toddler t)
        {
            return db.Invoices.Where(i => i.Toddler == t).Last();
        }

        // This function will build the invoice for a given toddler. It will only create an invoice if there is a need for one.
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
                newInvoice.OGM = buildOGM(toddler.ToddlerId, newInvoicePeriod.Month, newInvoicePeriod.Year);
                db.Invoices.Add(newInvoice);
                db.SaveChanges();
            }
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
        
        // This function will generate a list of all the days the toddler will come next month.
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
        
        // Returns all the extra days the child has come that are not in the AgreedDays. startdate = creationdate last created invoice, enddate = creationdate new invoice
        private List<DateTime> getAllExtraDaysinPeriod(Toddler t, DateTime startdate, DateTime enddate)
        {
            List<RegisteredDay> extraRegisteredDays = db.RegisteredDays.Where(rd => (rd.CheckedIn == true) && (rd.ExtraDay == true)).ToList();
            List<DateTime> extraDays = new List<DateTime>();
            foreach(RegisteredDay rd in extraRegisteredDays)
            {
                extraDays.Add(rd.DayInDaycare);
            }
            return extraDays;
        }

        // Returns all the Closing Days when the child couldn't come, but the day was already payed. startdate = creationdate last created invoice, enddate = creationdate new invoice
        private List<DateTime> getAllClosingDaysInPeriod(Toddler t, DateTime startdate, DateTime enddate)
        {
            List<RegisteredDay> registeredDaysChildHasntCome = db.RegisteredDays.Where(rd => (rd.Toddler == t)&&(rd.CheckedIn==false)).ToList();
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
        
        // Return the ONLY AgreedDays row where the invoice month falls in
        private AgreedDays agreedDaysThatApply(Toddler t, int year, int month)
        {
            List<AgreedDays> allAgreedDaysForToddler = db.AgreedDays.Where(ad => ad.Toddler == t).ToList();
            
            foreach (AgreedDays AD in allAgreedDaysForToddler)
            {
                if (AD.StartDate <= new DateTime(year, month, 1) && AD.EndDate.Month >= new DateTime(year, month, 1).Month)
                {
                    return AD;
                }
            }
            return null;
        }

        //Build Structured Comment for Invoice (year = 2017, it will be cut to just 17)
        private string buildOGM(int childid, int month, int year)
        {
            string yearstr = year.ToString();
            string shortyear = yearstr.Substring(yearstr.Length - 2);
            string challenge = "";
            challenge += childid.ToString();
            challenge += month.ToString();
            challenge += year.ToString();
            challenge += "001";
            int tocalculate = int.Parse(challenge);
            int mod = tocalculate % 97;

            string stringbuild = "+++";
            if(childid < 100)
            {
                stringbuild += "0";
            }
            stringbuild += childid.ToString();
            stringbuild += "/";
            stringbuild += month.ToString();
            stringbuild += year.ToString();
            stringbuild += "/";
            stringbuild += "001";
            stringbuild += mod;
            stringbuild += "+++";

            return stringbuild;
        }
    }
}