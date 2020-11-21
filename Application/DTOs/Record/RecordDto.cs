using System;

namespace Application.DTOs.Record
{
    public class RecordDto
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal DoseIn { get; set; }
        public decimal DoseOut { get; set; }
        public decimal Time { get; set; }
        public decimal Rating { get; set; }
    }
}
