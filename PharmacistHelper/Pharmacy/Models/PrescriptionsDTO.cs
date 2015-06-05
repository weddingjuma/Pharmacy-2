using System.Collections.Generic;
using PrescriptionResourceInterface;

namespace Pharmacy.Models
{
    public class PrescriptionsDTO
    {
        public virtual IEnumerable<PrescriptionDTO> Prescriptions { get; set; }
    }
}