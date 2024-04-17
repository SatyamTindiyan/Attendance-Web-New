using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Models.Infra
{
    public class InfraTypeSave
    {
        public int Id { get; set; }
        public string EMP_ID { get; set; }
        public string INFRAGRP_ID { get; set; }

        public string INFRA_TYPE { get; set; }

        public string MODEL_NUMBER { get; set; }

        public string SERIAL_NUMBER { get; set; }

        public string SERVICE_TAG_NUMBER { get; set; }

        public string IMEI_NUMBER { get; set; }
        public string INFRA_STATUS { get; set; }

        public string RECIEVE_DATE { get; set; }

        public string RECIEVE_BY { get; set; }

        public string RECIEVE_FROM { get; set; }

        public string REMARKS { get; set; }

        public string INFRA_TYPE_ID { get; set; }

        public string UNQ_REC_ID { get; set; }

        public string CREATED_BY { get; set; }

        public string CREATION_DATE { get; set; }

        public string UPDATED_BY { get; set; }

        public string LAST_UPDATE_DATE { get; set; }

        public string IP_ADDRESS { get; set; }
        public string HOST_NAME { get; set; }

        public string AUDIT_DATE { get; set; }

        public string INFRA_IMAGE { get; set; }

        public string DEVICE { get; set; }

        public string INFRA_IMAGE_NEW { get; set; }
        public string RecordId { get; set; }
    }

    public class Getinfravalues
    {
        public int ID { get; set;}
        public string InfraStatus { get; set;}
    }

    public class Exceldata
    {
        public string ExceljsonData { get; set; }
    }
}
