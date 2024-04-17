using Attendance.ApplicationCore.Interfaces.Infra;
using Attendance.Domain.Models.Infra;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ApplicationCore.Services.Infra
{
    public class InfraService:BaseService,IInfraService
    {
        public InfraService(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        //public List<InfraType> GetInfraTypeList(int id)
        //{
        //    List<InfraType> lstInfra = null;

        //    try
        //    {
        //        lstInfra = new List<InfraType>();
        //        lstInfra = _infra.GetInfraTypeList(id);

        //    }
        //    catch
        //    {

        //    }
        //    return lstInfra;
        //}
        public List<InfraType> GetInfraTypeList(string SearchValue)
        {
            List<InfraType> lstInfra = null;

            try
            {
                lstInfra = new List<InfraType>();
                lstInfra = _infraRepository.GetInfraTypeList(SearchValue);

            }
            catch
            {

            }
            return lstInfra;
        }

        public int SaveInfraType(InfraTypeSave modal, string image)
        {
            return _infraRepository.SaveInfraType(modal, image);

        }

        public List<InfraTypeSave> GetEmpAttdenceInfraDetails(InfraType model)
        {
            List<InfraTypeSave> lstInfradtl = null;

            try
            {
                lstInfradtl = new List<InfraTypeSave>();
                lstInfradtl = _infraRepository.GetEmpAttdenceInfraDetails(model);

            }
            catch
            {

            }
            return lstInfradtl;
        }

        public List<SerialNumber> GetInfraStockSerialNumber(string SearchValue, int infraGrpid, int type)
        {
            List<SerialNumber> lstSerialNumber = null;

            try
            {
                lstSerialNumber = new List<SerialNumber>();
                lstSerialNumber = _infraRepository.GetInfraStockSerialNumber(SearchValue, infraGrpid, type);

            }
            catch
            {

            }
            return lstSerialNumber;
        }

        public int DeleteInfraRecordDetails(string EmpID, int RecordId)
        {
            int num = 0;
            try
            {
                num = this._infraRepository.DeleteInfraRecordDetails(EmpID, RecordId);
            }
            catch
            {
            }
            return num;
        }

        public List<Getinfravalues> Getinfrastatus()
        {
            List<Getinfravalues> value = null;

            try
            {
                value = new List<Getinfravalues>();
                value = _infraRepository.Getinfrastatus();

            }
            catch
            {

            }
            return value;
        }

        public List<GetInfraEditdetails> EditBindInfraTable(int recordId)
        {
            List<GetInfraEditdetails> value = null;
            try
            {
                value = new List<GetInfraEditdetails>();
                value = _infraRepository.EditBindInfraTable(recordId);

            }
            catch
            {
            }
            return value;
        }

        public List<GetImageInfra> GetInfraImage(int recordId)
        {
            List<GetImageInfra> value = null;
            try
            {
                value = new List<GetImageInfra>();
                value = _infraRepository.GetInfraImage(recordId);

            }
            catch
            {
            }
            return value;

        }

        public List<GetInfraIssue> GetInfraIssue(string empID)
        {
            List<GetInfraIssue> value = null;
            try
            {
                value = new List<GetInfraIssue>();
                value = _infraRepository.GetInfraIssue(empID);

            }
            catch
            {
            }
            return value;
        }

        public int SaveUploadExcelItems(DataTable excelData)
        {
            return _infraRepository.SaveUploadExcelItems(excelData);
        }

        public List<GetPreviousDate> GetInfraPreviousDate(string empID)
        {
            List<GetPreviousDate> value = new List<GetPreviousDate>();
            try
            {
                value = new List<GetPreviousDate>();
                value = _infraRepository.GetInfraPreviousDate(empID);

            }
            catch
            {
            }
            return value;

        }
    }
}
