using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyLogic.Database
{
    class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid OrderId { get; set; }

        [MinLength(16), MaxLength(16)]
        public virtual string PatientFiscalCode { get; set; }

        public virtual DateTime ExpireDate { get; set; }

        public virtual IDictionary<string, int> Medicines { get; set; }

    }
}
