using System;
using System.Collections.Generic;
using System.Text;
using ApolloQA.Source.Driver;
using ApolloQA.Source.Helpers;

using System.Linq;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace ApolloQA.Data.Entity
{
    public class Policy
    {
        public readonly int Id;

        public Policy(int Id)
        {
            this.Id = Id;

        }
        public dynamic this[String propertyName]
        {
            get {

                
                var method = this.GetType().GetProperty(propertyName);
                if (method != null)
                {
                    return method.GetGetMethod().Invoke(this,null);

                }
                else
                {
                    return GetProperty(propertyName);
                }
            }
        }

        public dynamic GetProperties()
        {
            return RestAPI.GET($"/policy/{Id}");
        }
        public dynamic GetProperty(String propertyName)
        {
            var property = this.GetProperties()[propertyName];
            return property == null ? "" : property;
        }

        public List<dynamic> GetCoverages()
        {
            return ((JArray)this.GetProperties().coverages).Select(coverage => coverage).ToList<dynamic>();
        }
        public List<String> GetCoverageCodes()
        {
            return GetCoverages().Select(coverage => (String)coverage["associatedCoverage"]["coverageCode"]).ToList<String>();


        }
        public Application GetApplication()
        {
            return new Application(GetProperties()["applicationId"].ToObject<int>());
        }
        public dynamic GetVehicleTypeRisk()
        {
            return this.GetApplication().GetVehicleTypeRisk();
        }
        public List<Vehicle> GetVehicles()
        {
            return this.GetApplication().GetVehicles();
        }
        public JArray getCoverages()
        {
            dynamic body = new JObject();
            body.filters = new JObject();
            body.filters["PolicyId"] = this.Id;
            body.filters["EntityStatusComplexFilter"] = "{\"StatusIds\":[0,2]}";
            body.loadChildren = true;

            return RestAPI.POST("/policy/coverageoutput/search", body)["results"];
        }
        public List<String> getCoverageCodes(Vehicle risk)
        {
            var result = new List<String>();
            foreach(var coverage in this.getCoverages())
            {
                dynamic body = new JObject();
                body.filters = new JObject();
                body.filters["CoverageOutputId"] = coverage["id"];
                var risksAssociated = ((JArray)RestAPI.POST("/policy/CoverageOutputRisk/search", body)["results"]).Select(riskOutput => (int)riskOutput["riskId"]).ToList();
                if(risksAssociated.Contains((int)risk.GetProperty("RiskId")))
                {
                    result.Add((String)coverage["associatedCoverage"]["coverageCode"]);
                }
            }
            return result;

        }
        public List<String> getCoverageCodes()
        {
            return this.getCoverages().Select(coverage => (String)coverage["associatedCoverage"]["coverageCode"]).ToList<String>();

        }


        public Organization Organization{ get
            {
                try
                {
                    return new Organization("PartyId", this.GetProperties().insuredPartyId.Value.ToString());
                }
                catch(Exception exception)
                {
                    throw new Exception($"error constructing Organization with the following params 1=PartyId 2={this.GetProperties()?.insuredPartyId?.Value?.ToString()}");
                }
            }
        }
        
        public Boolean accidentPreventionCredit
        {
            get
            {
                return (Boolean)this["ratingFactors"]["accidentPreventionCredit"];
            }

        }
        public String coveredAutos
        {
            get
            {
                return (String)this["ratingFactors"]["coveredAutos"];
            }
        }
        public String motorCarrierFiling
        {
            get
            {
                return (String)this["ratingFactors"]["motorCarrierFiling"];
            }
        }
        public String BillingType
        {
            get
            {
                return (String)this["ratingFactors"]["billingType"];
            }
        }
        public String PaymentPlan
        {
            get
            {
                return (String)this["ratingFactors"]["paymentPlan"];
            }
        }
        public String isEft
        {
            get
            {
                return (String)this["ratingFactors"]["isEft"];
            }

        }

    }
}
