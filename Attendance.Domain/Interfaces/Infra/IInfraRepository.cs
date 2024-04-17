using Attendance.Domain.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Interfaces.Infra
{
    public interface IInfraRepository
    {
        int DeleteInfraRecordDetails(string empID, int RecordId);
        List<GetInfraEditdetails> EditBindInfraTable(int recordId);
        List<InfraTypeSave> GetEmpAttdenceInfraDetails(InfraType model);
        List<GetImageInfra> GetInfraImage(int recordId);
        List<GetInfraIssue> GetInfraIssue(string empID);
        List<GetPreviousDate> GetInfraPreviousDate(string empID);
        List<Getinfravalues> Getinfrastatus();
        List<SerialNumber> GetInfraStockSerialNumber(string searchValue, int infraGrpid, int type);
        List<InfraType> GetInfraTypeList(string searchValue);
        int SaveInfraType(InfraTypeSave modal, string image);
        int SaveUploadExcelItems(DataTable excelData);
    }
}
