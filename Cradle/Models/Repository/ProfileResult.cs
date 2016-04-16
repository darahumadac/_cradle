using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cradle.Models.Repository
{
    public class ProfileResult
    {
        private static readonly ProfileResult _success = new ProfileResult(true);

        public ProfileResult(IEnumerable<string> errors)
        {
            this.Succeeded = false;
            this.Errors = errors;
        }

        private ProfileResult(bool succeeded)
        {
            this.Succeeded = succeeded;
            this.Errors = new string[0];
        }

        public static ProfileResult Success
        {
            get
            {
                return ProfileResult._success;
            }
        }

        public static ProfileResult Failed(params string[] errors)
        {
            return new ProfileResult(errors);
        }

        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
