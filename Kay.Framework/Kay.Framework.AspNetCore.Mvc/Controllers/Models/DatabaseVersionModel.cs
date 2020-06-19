using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.AspNetCore.Mvc.Controllers
{
    public class DatabaseVersionModel
    {
        public string VersionName { get; set; }
        public string VersionDesc { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
