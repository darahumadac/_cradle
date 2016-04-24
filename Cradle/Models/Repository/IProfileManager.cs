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
        ManageDesignerProfileViewModel GetDesignerInformation(string userId);
        ProfileResult UpdateDesignerProfile(string userId, ManageDesignerProfileViewModel profile);
        DesignerProfile GetDesigner(string userId);
        PersonalProfile GetPerson(string userId);
        byte[] GetDesignerProfilePicture(string userId);

    }
}
