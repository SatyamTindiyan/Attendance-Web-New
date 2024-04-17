using Attendance.Domain.Models.DropDownModel;
using Attendance.Domain.Models.Holiday;
using Attendance.Domain.Models.Infra;
using Attendance.Infrastructure.Data.Common;
using Attendance.Infrastructure.loc;
using ExcelDataReader;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static Attendance.Infrastructure.loc.StatusMessageCode;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace Attendance.Controllers.Infra
{

    public class InfraController : BaseController
    {
        public InfraController(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        public IActionResult Infra()
        {
            return View();
        }

        //public JsonResult GetInfraType()
        //{
        //    int id = 0;
        //    List<InfraType> lstInfra = new List<InfraType>();
        //    lstInfra = _service.GetInfraTypeList(id);
        //    if (lstInfra != null)
        //        return Json(lstInfra, JsonRequestBehavior.AllowGet);
        //    else
        //        return Json(string.Empty, JsonRequestBehavior.AllowGet);

        //}
        public JsonResult GetInfraType(string SearchValue)
        {
            //int id = 0;
            List<InfraType> lstInfra = new List<InfraType>();
            lstInfra = _infraservice.GetInfraTypeList(SearchValue);
            return Json(new { lstInfra = lstInfra });
            //if (lstInfra != null)
            //    return Json(lstInfra, JsonRequestBehavior.AllowGet);
            //else
            //    return Json(string.Empty, JsonRequestBehavior.AllowGet);

        }

        //public string SaveInfraType(InfraTypeSave modal)
        //{
        //    modal.EMP_ID = MySession.EmpId;
        //    string strReturn = "";
        //    var result = _service.SaveInfraType(modal);

        //    if (result == (int)StatusMessageCode.StatusCode.Success)
        //        strReturn = "success";
        //    else if (result == (int)StatusMessageCode.StatusCode.AlreadyExist)
        //        strReturn = " Record already exist.";
        //    else
        //        strReturn = "Some error occured.";
        //    return strReturn;

        //    // return Json(attendance, JsonRequestBehavior.AllowGet);

        //}
        [HttpPost]
        public JsonResult GetEmpAttdenceInfraDetails(InfraType model)
        {
            int id = 0;
            model.EmpID = MySession.EmpId;
            model.RecordId = id;
            List<InfraTypeSave> lstinfradtl = new List<InfraTypeSave>();

            lstinfradtl = _infraservice.GetEmpAttdenceInfraDetails(model);
            //return Json(new { lstinfradtl = lstinfradtl });
            if (lstinfradtl != null)
                return Json(new { lstinfradtl });
            else
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetInfraStockSerialNumber(string SearchValue, int infraGrpid, int type)
        {
            List<SerialNumber> lstSerialNumber = new List<SerialNumber>();

            lstSerialNumber = _infraservice.GetInfraStockSerialNumber(SearchValue, infraGrpid, type);
            return Json(new { lstSerialNumber = lstSerialNumber });
            //if (lstSerialNumber != null)
            //    return Json(lstSerialNumber= lstSerialNumber);
            //else
            //    return Json(string.Empty);
        }
        [HttpPost]
        public string SaveInfraType(InfraTypeSave modal)
        {
            modal.EMP_ID = MySession.EmpId;
            string strReturn = "";
            InfraTypeSave test = new InfraTypeSave();
            //if (modal.INFRA_IMAGE != null)
            //{
            //    modal.INFRA_IMAGE.Replace("data:application/jpeg;base64,", "");
            //}

            var objects = modal.INFRA_IMAGE?.Split(',')
                                 .Where(x => !string.IsNullOrEmpty(x))
                                 .ToList();

            string image = null;
            if (objects != null && objects.Count >= 2)
            {
                image = objects[1];
            }


            var result = _infraservice.SaveInfraType(modal, image);


            if (result == (int)StatusCode1.Success)
                strReturn = "success";
            else if (result == (int)StatusCode1.AlreadyExist)
                strReturn = "Record already exist.";
            else if (result == (int)StatusCode1.DataUpdated)
                strReturn = "Updated";
            else
                strReturn = "Some error occured.";
            return strReturn;
            // return Json(attendance, JsonRequestBehavior.AllowGet);

        }
        public string DeleteInfraRecordDetails(int RecordId)
        {
            var EmpId = MySession.EmpId;
            string strReturn = "Some error occured.";

            if (RecordId > 0)
            {
                int result = _infraservice.DeleteInfraRecordDetails(EmpId, RecordId);

                if (result == (int)StatusCode1.Success)
                    strReturn = "Record has been deleted successfully";
                else if (result == (int)StatusCode1.NotAccess)
                {
                    strReturn = "You can not delete this infra .Use in attendance ";
                }
                else
                {
                    strReturn = "Some error occured.";
                }

            }
            return strReturn;

        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public JsonResult Getinfrastatus()
        {
            List<Getinfravalues> values = new List<Getinfravalues>();

            values = _infraservice.Getinfrastatus();

            return Json(new { data = values });
        }

        public JsonResult EditBindInfraTable(int RecordId)
        {
            List<GetInfraEditdetails> infraTypes = new List<GetInfraEditdetails>();
            infraTypes = _infraservice.EditBindInfraTable(RecordId);
            return Json(new { infraTypes = infraTypes });
        }

        public JsonResult GetInfraImage(int RecordId)
        {
            List<GetImageInfra> imageInfras = new List<GetImageInfra>();
            imageInfras = _infraservice.GetInfraImage(RecordId);
            //string image = "data:image/jpeg;base64";
            //string name = image + ',' + imageInfras;

            return Json(new { imageInfras = imageInfras });
        }

        public IActionResult InfraDetails()
        {
            return View();
        }

        [HttpPost]
        public string InfraDetails(GetInfraIssue modal)
        {
            modal.EmpID = MySession.EmpId;
            List<GetInfraIssue> infraIssues = new List<GetInfraIssue>();
            infraIssues = _infraservice.GetInfraIssue(modal.EmpID);
            var result = JsonConvert.SerializeObject(new { data = infraIssues });
            return result; ;
        }

        [HttpPost]
        public string SaveUploadExcelItems(IFormCollection formCollection)
        {
            Exceldata Obj = null;
            int result = (int)StatusMessageCode.StatusCode.Error;
            string strReturn = "Some error occured.";
            if (formCollection != null)
            {
                try
                {
                    var uploadFile = formCollection.Files["uploadEmployeeExcel"];

                    if (uploadFile != null && uploadFile.Length > 0)
                    {
                        // Validate file extension
                        string[] validFileTypes = { ".xls", ".xlsx" };
                        string fileExtension = Path.GetExtension(uploadFile.FileName);

                        if (validFileTypes.Contains(fileExtension))
                        {
                            // Define upload directory
                            string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploadEmployeeExcel");

                            // Create directory if it doesn't exist
                            if (!Directory.Exists(uploadDirectory))
                                Directory.CreateDirectory(uploadDirectory);

                            // Generate unique file name
                            string uniqueFileName = $"{DateTime.Now.Ticks}_{Path.GetFileName(uploadFile.FileName)}";
                            string filePath = Path.Combine(uploadDirectory, uniqueFileName);

                            // Save file to server
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                uploadFile.CopyTo(stream);
                            }

                            // Process uploaded Excel file
                            DataTable excelData = ReadExcelFile(filePath);

                            result = _infraservice.SaveUploadExcelItems(excelData);

                            if(result == 101)
                            {
                                strReturn = "File uploaded successfully!";
                            }
                            // Perform further operations with excelData as needed
                            // For example, you can pass the data to a service or repository for processing

                            
                        }
                        else if(result == 102)
                        {
                            strReturn = "Please upload files in .xls or .xlsx format only.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception appropriately
                    throw;
                }
            }
            else
            {
                return null;
            }

            return strReturn;
        }

        private DataTable ReadExcelFile(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                DataTable dt = new DataTable();
                foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }
                for (int rowNumber = 2; rowNumber <= worksheet.Dimension.End.Row; rowNumber++)
                {
                    var row = worksheet.Cells[rowNumber, 1, rowNumber, worksheet.Dimension.End.Column];
                    var newRow = dt.Rows.Add();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return dt;
            }
        }

        public JsonResult GetInfraPreviousDate()
        {
            var EmpID = MySession.EmpId;
            List<GetPreviousDate> getdate = new List<GetPreviousDate>();
            getdate = _infraservice.GetInfraPreviousDate(EmpID);

            return Json(new {getdate = getdate});
        }
    }
}