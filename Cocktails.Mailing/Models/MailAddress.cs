using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktails.Mailing.Models
{
    public sealed class MailAddress
    {
        public MailAddress(string address, string displayName)
        {
            Address = address;
            DisplayName = displayName;
        }

        public MailAddress(string address)
        {
            Address = address;
        }

        public MailAddress()
        {
        }

        public string Address { get; set; }

        public string DisplayName { get; set; }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(this.DisplayName) ? string.Format("{0} <{1}>", this.DisplayName, this.Address) : this.Address;
        }
    }
}
