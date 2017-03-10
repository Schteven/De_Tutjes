using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using De_Tutjes.Models;
using System.ComponentModel;

namespace De_Tutjes.Areas.Administration.Models
{
    public class CreateNewChildModel
    {
        //public Toddler child { get; set; }
        //public Parent firstParent { get; set; }
        //public Parent secondParent { get; set; }
        
        // Child
        [Required]
        public string childFirstname { get; set; }
        [Required]
        public string childLastname { get; set; }
        public string childGender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime childBirthdate { get; set; }

        // Parents
        public bool hasGotSibling { get; set; }
        public bool isLivingTogether { get; set; }

        // First Parent
        [Required]
        public string firstParentFirstname { get; set; }
        [Required]
        public string firstParentLastname { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime firstParentBirthdate { get; set; }
        public string firstParentGender { get; set; }

        [Required]
        public string firstParentStreet { get; set; }
        [Required]
        public int firstParentHouseNumber { get; set; }
        public string firstParentHouseBus { get; set; }
        [Required]
        public int firstParentPostalCode { get; set; }
        [Required]
        public string firstParentCity { get; set; }

        public int firstParentPhoneHome { get; set; }
        [Required]
        public int firstParentPhoneMobile { get; set; }
        public int firstParentPhoneWork { get; set; }

        [EmailAddress]
        public string firstParentEmailAddress { get; set; }

        // Second Parent
        public string secondParentFirstname { get; set; }
        public string secondParentLastname { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime secondParentBirthdate { get; set; }
        public string secondParentGender { get; set; }

        public string secondParentStreet { get; set; }
        public int secondParentHouseNumber { get; set; }
        public string secondParentHouseBus { get; set; }
        public int secondParentPostalCode { get; set; }
        public string secondParentCity { get; set; }

        public int secondParentPhoneHome { get; set; }
        public int secondParentPhoneMobile { get; set; }
        public int secondParentPhoneWork { get; set; }

        [EmailAddress]
        public string secondParentEmailAddress { get; set; }
    
    }

}