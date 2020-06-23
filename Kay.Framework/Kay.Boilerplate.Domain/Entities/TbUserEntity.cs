using Kay.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kay.Boilerplate.Domain.Entities
{
    [Table("TbAccount")]
    public class TbUserEntity: BizEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public int UserType { get; set; }
    }
}
