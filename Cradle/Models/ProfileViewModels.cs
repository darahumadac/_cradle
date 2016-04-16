using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Cradle.Models
{
    public abstract class BaseProfileViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }

    public class DesignerProfileViewModel : BaseProfileViewModel
    {
        //public string DesignerName { get; set; }
        public string Tagline { get; set; }
        //public string Email { get; set; }
        //public Address Address { get; set; }
        public List<string> MadeType { get; set; }
        public string Specialization { get; set; }
        public string StyleDescription { get; set; }
        public Dictionary<string, string> DeliveryTime { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<Collection> Collections { get; set; }
        public Statistics ProfStats { get; set; }
        public double ProfileCompletionPercentage
        {
            get
            {
                string[] _fields = new string[]{"Name", "Email", "Address",
                    "Tagline", "MadeType", "Specialization", "DeliveryTime", "MinPrice", "StyleDescription"};

                double completed = 0;
                PropertyInfo[] properties = typeof(DesignerProfileViewModel).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    if (_fields.FirstOrDefault(f => f == properties[i].Name) != null && 
                        (properties[i].GetValue(this, null) != null && !properties[i].GetValue(this, null).Equals(0)))
                    {
                        completed += 1;
                    }
                }
                   
                return (completed / _fields.Length) * 100.0;
            }
            
        }


        
    }
}