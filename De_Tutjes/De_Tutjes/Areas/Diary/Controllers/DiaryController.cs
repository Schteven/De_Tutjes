using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using De_Tutjes.Areas.Diary.Models;
using De_Tutjes.Models;
using De_Tutjes.Services;

namespace De_Tutjes.Areas.Diary.Controllers
{
    public class DiaryController : Controller
    {
        // GET: Diary/Diary

        private ChildManager cm = new ChildManager();

        public ActionResult Index()
        {
            return View(cm.GetChildCardsForToday());
        }


        //public ActionResult Index(Location location)
        //{
        //    cm.SetLocation(location);
        //    childcards = cm.GetAllChilds();
        //    return View(childcards);
        //}
        public ActionResult ChildStatus(string submitButton, FormCollection col, string commentinput)//DiaryModel diarymodel
        {
            try
            {
                string comment = null;
                foreach (var c in col)
                {
                    if(commentinput != null)
                    {
                        comment = commentinput;
                    }
                }
                switch (submitButton)
                {
                    case "ChildCheckIn":
                        {
                            
                            foreach (var c in col)
                            {
                                cm.SetChildUpdate((string)c, ChildUpdate.CheckIn, comment);
                                cm.SetChildStatus((string)c, Models.ChildStatus.Normal);
                            }
                            break;
                        }

                    case "ChildSleeping":
                        {
                            foreach (var c in col)
                            {
                                cm.SetChildUpdate((string)c, ChildUpdate.Sleeping, comment);
                                cm.SetChildStatus((string)c, Models.ChildStatus.Sleeping);
                            }
                            break;
                        }
                    case "ChildEating":
                        {
                            foreach (var c in col)
                            {
                                cm.SetChildUpdate((string)c, ChildUpdate.Eating, comment);
                            }
                            break;
                        }
                        
                    case "ChildDiaper":
                        {
                            foreach (var c in col)
                            {
                                cm.SetChildUpdate((string)c, ChildUpdate.Diaper, comment);
                            }
                            break;
                        }
                        
                    case "ChildComment":
                        foreach (var c in col)
                        {
                            
                                cm.SetChildUpdate((string)c, ChildUpdate.Comment, comment);
                            
                        }
                        break;
                    case "ChildOverview":
                        foreach (var c in col)
                        {
                            //TODO: IF count >2, niets doen

                            //Give window with overview of this child
                            return RedirectToAction("ShowUpdates");
                        }
                        break;
                    case "ChildCheckOut":
                        {
                            foreach (var c in col)
                            {
                                cm.SetChildUpdate((string)c, ChildUpdate.CheckOut, comment);
                                cm.SetChildStatus((string)c, Models.ChildStatus.Home);
                            }
                            break;
                        }
                        
                    default:
                        break;

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return Index();
            }
            
        }
        
        public ActionResult ShortInfo(string toddlerid)
        {
            Child child = new Child();
            if(toddlerid != null)
            {
                child = cm.GetChildById(toddlerid);               
            }

            return PartialView("ShortToddlerInfo", child);
        }

        public ActionResult ShowUpdates()
        {
            return View(cm.GetChildrenWithUpdates());
        }

        public void SendUpdateMail(string toddlerId)
        {
            MailService ms = new MailService();

           



        }


    }
}