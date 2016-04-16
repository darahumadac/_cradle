using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Cradle.Models.Repository
{
    public class CradleUserManager : UserManager<Account>
    {
        private CradleUserStore _userStore;

        public CradleUserStore UserStore
        {
            get { return _userStore; }
            set { _userStore = value; }
        }

        public CradleUserManager(CradleUserStore userStore) : base(userStore)
        {
            _userStore = userStore;
        }

  
    }
}