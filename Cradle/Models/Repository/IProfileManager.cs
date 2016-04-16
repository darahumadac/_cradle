using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cradle.Models.Repository
{
    public interface IProfileManager
    {
        DesignerProfileViewModel GetDesignerProfile(string userId);
        ProfileResult UpdateDesignerProfile(DesignerProfileViewModel profile);

    }
}
