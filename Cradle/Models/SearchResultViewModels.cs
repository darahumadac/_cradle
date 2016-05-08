using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cradle.Models
{
    public class DesignerResultsViewModel
    {
        public DesignerResultsViewModel(DesignerProfile designer)
        {
            Username = designer.Account.UserName;
            DesignerName = designer.BusinessName;
            City = designer.Address.City;
            Country = designer.Address.Country;
            Tagline = designer.Tagline;
            StyleDescription = designer.StyleDescription;
            CollectionCount = designer.Collection.Count;
            IsCustomMade = designer.IsCustomMade;
        }

        public string Username { get; set; }
        public string DesignerName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsCustomMade { get; set; }
        public string Tagline { get; set; }
        public string StyleDescription { get; set; }
        public int CollectionCount { get; set; }
    }
}