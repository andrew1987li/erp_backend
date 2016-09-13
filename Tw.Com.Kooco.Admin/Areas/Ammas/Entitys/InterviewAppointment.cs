using System;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Entitys {
    public class InterviewAppointment {
        public int InterviewAppointmentId { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public int InterviewType { get; set; }
    }
}
