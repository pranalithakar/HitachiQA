using System;
using System.Collections.Generic;
using System.Text;

namespace HitachiQA.Data
{
    public class Vendor
    {
        public String VendorAccount;
        public String Type;
        public String Name;
        public String Group;
        public String Address;
        public String ContactInfo;
        public String Currency;
        public String TermsOfPayment;
        public String MethodOfPayment;
        public String BankAccountInfo;
        public String InvoiceAccount;

        public Vendor(String VendorAccount, String Type, String Name, String Group, String Address)
        {
            this.VendorAccount = VendorAccount;
            this.Type = Type;
        }
    }
}
