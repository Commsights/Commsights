using Commsights.Data.Helpers;
using System;
using System.Collections.Generic;

namespace Commsights.Data.Models
{
    public partial class Membership : BaseModel
    {
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? CategoryID { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string CitizenIdentification { get; set; }
        public string Passport { get; set; }
        public decimal? Points { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string GUICode { get; set; }
        public string TaxCode { get; set; }
        public string ShortName { get; set; }
        public string EnglishName { get; set; }
        public string Avatar { get; set; }
        public string Website { get; set; }
        public void InitDefaultValue()
        {
            if (string.IsNullOrEmpty(this.GUICode))
            {
                this.GUICode = AppGlobal.InitGuiCode;
            }
        }
        public void EncryptPassword()
        {
            this.Password = SecurityHelper.Encrypt(this.GUICode, this.Password);
        }
        public void DecryptPassword()
        {
            this.Password = SecurityHelper.Decrypt(this.GUICode, this.Password);
        }
    }
}
