using Kay.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kay.Boilerplate.Domain.Entities
{
    [Table("TbUser1")]
    public class TbUserEntity: BizEntity
    {
        public string Name { get; set; }

        public string Password { get; set; }
    }
}
