using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace HitachiQA.Helpers
{
    public class SpecflowTables
    {
       public class OrganizationTable
        {
            public string BusinessName { get; set; }
            public string DBA { get; set; }
            public string OrganizationType { get; set; }
            public string TaxIdType { get; set; }
            public string TaxIdNumber { get; set; }
            public string DescriptionOfOperations { get; set; }
            public string BusinessPhoneNumber { get; set; }
            public string BusinessEamil { get; set; }
            public string BusinessWebsite { get; set; }
            public string Keyword { get; set; }
            public string ClassTaxonomy { get; set; }
            public string YearStarted { get; set; }
            public string YearOwned { get; set; }
        }    
    }
}
