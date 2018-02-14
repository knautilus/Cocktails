using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktails.Mailing.Models
{
    public sealed class MailAddressCollection : List<MailAddress>
    {
        public void Add(string address, string displayName)
        {
            Add(new MailAddress(address, displayName));
        }

        public void Add(string address)
        {
            Add(new MailAddress(address));
        }

        public override string ToString()
        {
            return string.Join(";", this);
        }
    }
}
