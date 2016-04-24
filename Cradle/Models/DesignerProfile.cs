using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Cradle.Models.Enums;

namespace Cradle.Models
{
    public class DesignerProfile : Profile
    {
        public string DesignerProfileID { get; set; }
        public virtual Image ProfilePicture { get; set; }
        public string BusinessName { get; set; }
        public string BusinessEmailAddress { get; set; }
        public bool IsProfileComplete { get; set; }
        public string Tagline { get; set; }
        public string StyleDescription { get; set; }
        public bool IsRTW { get; set; }
        public bool IsCustomMade { get; set; }
        public int RTWMinDeliveryDays { get; set; }
        public int RTWMaxDeliveryDays { get; set; }
        public int CustomMadeMinDeliveryDays { get; set; }
        public int CustomMadeMaxDeliveryDays { get; set; }
        public virtual List<DesignerAttire> AttireSpecialization { get; set; }
        public virtual Statistics ProfileStats { get; set; }
        public virtual List<Collection> Collection { get; set; }

        public virtual Account Account { get; set; }

    }
}