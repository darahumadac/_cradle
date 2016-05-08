using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cradle.Models.Repository
{
    public class CradleHomeManager : IHomeManager
    {
        private CradleDbContext _context;

        public CradleHomeManager()
        {
            _context = new CradleDbContext();
        }

        public List<DesignerResultsViewModel> GetDesignerSearchResults(SearchCriteria searchCriteria)
        {
            List<DesignerResultsViewModel> designerList = new List<DesignerResultsViewModel>();

            if (string.IsNullOrWhiteSpace(searchCriteria.DesignerType) || 
                !searchCriteria.DesignerType.Equals("custom-made"))
            {
                //All designers
                _context.DesignerProfiles.ToList()
                .ForEach(dp => designerList.Add(new DesignerResultsViewModel(dp)));
            }
            else if (searchCriteria.DesignerType.Equals("custom-made"))
            {
                _context.DesignerProfiles.Where(dp => dp.IsCustomMade == true)
                    .ToList().ForEach(dp => designerList.Add(new DesignerResultsViewModel(dp)));
            }

            return designerList;
        }


    }
}