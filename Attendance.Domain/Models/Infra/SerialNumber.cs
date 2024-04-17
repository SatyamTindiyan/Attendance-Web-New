using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Infra
{
    public class SerialNumber
    {
        public int ID { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string SERVICE_TAG_NUMBER { get; set; }
        public string MODEL_NUMBER { get; set; }

        public string IMEI_NUMBER { get; set; }

        public int InfraGrpId { get; set; }
        public int InfraID { get; set; }

        public string p { get; set; }
    }
}
