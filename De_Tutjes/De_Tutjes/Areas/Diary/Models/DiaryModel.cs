using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace De_Tutjes.Areas.Diary.Models
{
    public class DiaryModel
    {
        public SelectListItem Item { get; set; }
        public List<SelectListItem> Childcards
        {
            get;
            set;
        }
    }
}