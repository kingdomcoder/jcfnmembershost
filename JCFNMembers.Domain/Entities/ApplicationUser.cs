using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JCFNMembers.Domain.Entities {
    public class ApplicationUser : IdentityUser {
        public ApplicationUser() {
        }
        //[Key]
        //[Required]
        //public string Id { get; set; } ....... MicrosoftAspNetCore.Identity.IdentityUser

        //[Required]
        //[MaxLength(128)]
        //public string UserName { get; set; }....... MicrosoftAspNetCore.Identity.IdentityUser

        //[Required]
        //public string Email { get; set; }....... MicrosoftAspNetCore.Identity.IdentityUser

        public string DisplayName { get; set; }

        public string Notes { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Flags { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        #region Lazy-Load Properties
        /// <summary>
        /// A list of all the quiz created by this users.
        /// </summary>
 //       public virtual List<Quiz> Quizzes { get; set; }

        /// <summary>
        /// A list of all the refresh tokens issued for this users.
        /// </summary>
        //public virtual List<Token> Tokens { get; set; }
        #endregion

        /******************************************************
         * Added by OZ
         * ***************************************************/
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Prefix { get; set; }
        public string Shimei { get; set; }
        public string Myoji { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        //Japan Address
        public string Yubin_Bango { get; set; }
        public string To_Do_Fu_Ken { get; set; }
        public string Shi_Gun_Ku { get; set; }
        public string Cho_Son { get; set; }
        public string Apartment_Etc { get; set; }
        //Japan Kanji Address
        public string To_Do_Fu_Ken_JP { get; set; }
        public string Shi_Gun_Ku_JP { get; set; }
        public string Cho_Son_JP { get; set; }
        public string Apartment_Etc_JP { get; set; }
        //Other personal info
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string TelNo { get; set; }
        public string TimezoneName { get; set; }
        public int? UTCdiff { get; set; }
        public string Token { get; set; }
        public string Occupation { get; set; }
        public string MemberStatus { get; set; }
        public bool? Married { get; set; }
        public string ZoomAccount { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public DateTime? LastAccessedDate { get; set; }
        public bool? Christian { get; set; }
    }
}
