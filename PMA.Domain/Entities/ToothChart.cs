using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class ToothChart : BaseEntity
    {
        public int ToothNumber { get; set; }
        public string Condition { get; set; }
        public string Notes { get; set; }
        public Guid DentistryConsultationId { get; set; }
        public DentistryConsultation DentistryConsultation { get; set; }
    }
}
