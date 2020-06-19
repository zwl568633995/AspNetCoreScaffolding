using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kay.Framework.Utility.Utilities.Snowflake;
using Kay.Framework.Domain.Data;

namespace Kay.Framework.Domain.Entities
{
    public class BizEntity: IKeyEntity<long>,IAddTimeEntity, IModTimeEntity, ISoftStatusEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? ModTime { get; set; }
        public int Status { get; set; }
    }
}
