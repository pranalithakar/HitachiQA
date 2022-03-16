using System;
using System.Collections.Generic;
using System.Text;

namespace HitachiQA.Data.Entity
{
    public class Address
    {
        public String NameOrDescription;
        public String Purpose;
        public String CountryRegion;
        public String PostalCode;
        public String Street;
        public String StreetNumber;
        public String BuildingComplement;
        public String PostBox;
        public String City;
        public String District;
        public String State;
        public String Country;

        public Address(String NameOrDescription, String Purpose, String CountryRegion, String PostalCode, String Street, String StreetNumber, String BuildingComplement, String PostBox, String City, String State, String District, String Country)
        {
            this.NameOrDescription = NameOrDescription;
            this.Purpose = Purpose;
            this.CountryRegion = CountryRegion;
            this.PostalCode = PostalCode;
            this.Street = Street;
            this.StreetNumber = StreetNumber;
            this.BuildingComplement = BuildingComplement;
            this.PostBox = PostBox;
            this.City = City;
            this.District = District;
            this.State = State;
            this.Country = Country;
        }

        public Dictionary<string, string> AddressInputs = new Dictionary<string, string>
        {
            { "Details_Description", "automationAddress" },
            { "Roles", "Other" },
            { "LogisticsPostalAddress_CountryRegionId", "United States" },
            { "LogisticsPostalAddress_ZipCode", "37323" },
            { "LogisticsPostalAddress_Street", "55876 Waterlevel Hwy" },
            { "LogisticsPostalAddress_City", "Cleveland" },
            { "LogisticsPostalAddress_State", "TN" },
            { "LogisticsPostalAddress_County", "BRADLEY" },
        };
    }
}
