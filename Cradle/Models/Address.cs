using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Cradle.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string StreetNo { get; set; }
        public string StreetName { get; set; }
        public string Municipality { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public override string ToString()
        {
            string fullAddress = string.Empty;

            if(StreetNo != null)
            {
                fullAddress += StreetNo;
            }
            if(!string.IsNullOrEmpty(StreetName))
            {
                fullAddress += StreetName + ", ";
            }
            if (!string.IsNullOrEmpty(Municipality))
            {
                fullAddress += Municipality + ", ";
            }
            if (!string.IsNullOrEmpty(City))
            {
                fullAddress += City + ", ";
            }
            if (!string.IsNullOrEmpty(Country))
            {
                fullAddress += Country + " ";
            }
            if (!string.IsNullOrEmpty(ZipCode))
            {
                fullAddress += ZipCode;
            }

            return fullAddress;
           
        }


    }
}
