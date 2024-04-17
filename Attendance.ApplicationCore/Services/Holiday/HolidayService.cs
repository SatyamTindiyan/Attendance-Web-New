using Attendance.ApplicationCore;
using Attendance.ApplicationCore.Interfaces.Holiday;
using Attendance.Domain;
using Attendance.Domain.Models.Holiday;
using Attendance.Domain.Models.Master;
using Attendance.Domain.Models.UserDetail;
using Attendance.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Attendance.Domain.Models.Infra;

namespace Attendance.ApplicationCore.Services.Holiday
{
    public class HolidayService : BaseService, IHolidayService
    {
        public HolidayService(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        public List<Holidays> GetHolidayList()
        {
            List<Holidays> lstholiday = null;

            try
            {
                lstholiday = new List<Holidays>();
                lstholiday = _holidayRepository.GetHolidayList();

            }
            catch
            {

            }
            return lstholiday;
        }
        public int SaveHolidayType(HolidayTypeSave modal)
        {
            return _holidayRepository.SaveHolidayType(modal);

        }

        public List<HoilidayType> GetHolidayDesc(int countryID , int userid, string usertype, string usersubtype)
        {
            List<HoilidayType> lstholidaytype = null;
            List<HoilidayType> hoilidaytype = null;
            lstholidaytype = new List<HoilidayType>();
            try
            {
                string empid = MySession.EmpId;
                if(countryID > 0)
                {
                    lstholidaytype = new List<HoilidayType>();
                    List<HoilidayType> basedoncountry =_holidayRepository.GetHolidayDesc(countryID ,  userid,  usertype,  usersubtype);
                    if (basedoncountry.Any<HoilidayType>())
                        hoilidaytype = basedoncountry.Select(x => new HoilidayType()
                        {

                            id = x.id,
                            Encryptid = Convert.ToString(Security.GetEncryptString(Convert.ToString(x.id).Trim())),
                            HOLIDAY_NAME = x.HOLIDAY_NAME,
                        }).OrderBy<HoilidayType, string>((Func<HoilidayType, string>)(x => x.HOLIDAY_NAME)).ToList<HoilidayType>();
                }
               
            }
            catch
            {

            }
            return hoilidaytype;
        }
        public List<HolidayMaster> GetstatelistbasedonCountryId(int countryID, int userid, string usertype, string usersubtype)
        {
            List<HolidayMaster> HoliMasterList = null;
            List<HolidayMaster> MasterList = null;

            HoliMasterList = new List<HolidayMaster>();
            try
            {
                string empid = MySession.EmpId;
                //countryID = MySession.Countryid;
                if (countryID > 0)
                {
                    HoliMasterList = new List<HolidayMaster>();
                    List<HolidayMaster> basedoncountryid = _holidayRepository.GetstatelistbasedonCountryId(countryID,userid,usertype,usersubtype);
                    if (basedoncountryid.Any<HolidayMaster>())
                        MasterList = basedoncountryid.Select(x => new HolidayMaster()
                        {
                            stateId = x.stateId,
                            EncryptstateId = Convert.ToString(Security.GetEncryptString(Convert.ToString(x.stateId).Trim())),
                            state = x.state,
                        }).OrderBy<HolidayMaster, string>((Func<HolidayMaster, string>)(x => x.state)).ToList<HolidayMaster>();
                }

            }
            catch
            {
            }
            return MasterList;
        }

        public List<HolidayTypeSave> GetHolidaymasterList(HolidayTypeSave modal, int userid, string usertype, string usersubtype)
        {
            List<HolidayTypeSave> lstholidaymaster = null;
            try
            {
                lstholidaymaster = new List<HolidayTypeSave>();
                lstholidaymaster = _holidayRepository.GetHolidaymasterList(modal,userid,usertype,usersubtype);

            }
            catch
            {

            }
            return lstholidaymaster;
        }

        //public void ExportToPdfSiteUploadError(DataTable myDataTable, out string returnFilePath)
        //{
        //    returnFilePath = "";
        //    DataTable dataTable = myDataTable;
        //    Font font1 = FontFactory.GetFont("ARIAL", 13f);
        //    Font font2 = FontFactory.GetFont("ARIAL", 18f);
        //    string str1 = Path.Combine("~/Download");
        //    string str2 = "/Error_" + DateTime.Now.Ticks.ToString() + ".pdf";
        //    string str3 = str2;
        //    string path = str1 + str3;
        //    if (File.Exists(path))
        //    {
        //        File.AppendAllText(path, "");
        //    }
        //    else
        //    {
        //        using (StreamWriter text = File.CreateText(path))
        //        {
        //            text.WriteLine("");
        //            text.Flush();
        //            text.Close();
        //        }
        //    }
        //    using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.None))
        //    {
        //        using (Document document = new Document(PageSize.LETTER))
        //        {
        //            using (PdfWriter instance = PdfWriter.GetInstance(document, (Stream)fileStream))
        //            {
        //                document.Open();
        //                if (dataTable.Rows.Count > 0)
        //                {
        //                    PdfPTable pdfPtable1 = new PdfPTable(1);
        //                    pdfPtable1.TotalWidth = 200f;
        //                    pdfPtable1.LockedWidth = true;
        //                    PdfPCell pdfPcell1 = new PdfPCell(new Phrase(new Chunk("Site Upload Error", font2)));
        //                    ((Rectangle)pdfPcell1).Border = 0;
        //                    pdfPtable1.AddCell(pdfPcell1);
        //                    this.DrawLine(instance, 25f, document.Top - 30f, document.PageSize.Width - 25f, document.Top - 30f, (BaseColor)null);
        //                    document.Add((IElement)pdfPtable1);
        //                    PdfPTable pdfPtable2 = new PdfPTable(dataTable.Columns.Count);
        //                    pdfPtable2.SpacingBefore = 20f;
        //                    for (int index = 0; index <= dataTable.Columns.Count - 1; ++index)
        //                    {
        //                        PdfPCell pdfPcell2 = new PdfPCell(new Phrase(new Chunk(dataTable.Columns[index].ColumnName, font2)));
        //                        pdfPtable2.AddCell(pdfPcell2);
        //                    }
        //                    for (int index = 0; index <= dataTable.Rows.Count - 1; ++index)
        //                    {
        //                        for (int columnIndex = 0; columnIndex <= dataTable.Columns.Count - 1; ++columnIndex)
        //                        {
        //                            PdfPCell pdfPcell3 = new PdfPCell(new Phrase(new Chunk(dataTable.Rows[index][columnIndex].ToString(), font1)));
        //                            pdfPtable2.AddCell(pdfPcell3);
        //                        }
        //                    }
        //                    document.Add((IElement)pdfPtable2);
        //                }
        //                document.Close();
        //            }
        //        }
        //    }
        //    returnFilePath = str2;
        //}
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo propertyInfo in properties)
                dataTable.Columns.Add(propertyInfo.Name);
            foreach (T obj in items)
            {
                object[] objArray = new object[properties.Length];
                for (int index = 0; index < properties.Length; ++index)
                    objArray[index] = properties[index].GetValue((object)obj, (object[])null);
                dataTable.Rows.Add(objArray);
            }
            return dataTable;
        }


        public int SavecmDataFromExcel(HolidayTypeSave obj, string returnFilePath, string jsondata , int Countryid)
        {
            int result = (int)StatusCodes.Status500InternalServerError;
            int num = 0;

            DataTable dataTable = new DataTable();
            DataSet dataSet = new DataSet();
            try
            {
                VMUserDetail vmUserDetail1 = new VMUserDetail();
                //HolidayTypeSave lstholiday = new HolidayTypeSave();
                result = _holidayRepository.SavecmDataFromExcel(obj, vmUserDetail1.UserType, vmUserDetail1.UserSubType, vmUserDetail1.Id, returnFilePath, jsondata , Countryid);

            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public List<HolidayMaster> GetHolidayMasterDetails( int countryID)
        {
            List<HolidayMaster> lstholidaydtl = null;

            try
            {
                lstholidaydtl = new List<HolidayMaster>();
                lstholidaydtl = _holidayRepository.GetHolidayMasterDetails( countryID);

            }
            catch
            {

            }
            return lstholidaydtl;
        }

        public DataTable HolidayToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo propertyInfo in properties)
                dataTable.Columns.Add(propertyInfo.Name);
            foreach (T obj in items)
            {
                object[] objArray = new object[properties.Length];
                for (int index = 0; index < properties.Length; ++index)
                    objArray[index] = properties[index].GetValue((object)obj, (object[])null);
                dataTable.Rows.Add(objArray);
            }
            return dataTable;
        }
        public int SaveHolidayDataFromExcel(HolidayTypeSave obj, string returnFilePath, string jsondata, int Countryid, string userid, string usertype, string usersubtype)
        {
            int result = (int)StatusCodes.Status500InternalServerError;
            int num = 0;

            DataTable dataTable = new DataTable();
            DataSet dataSet = new DataSet();
            try
            {
                VMUserDetail vmUserDetail1 = new VMUserDetail();
                //HolidayTypeSave lstholiday = new HolidayTypeSave();
                result = _holidayRepository.SaveHolidayDataFromExcel(obj, vmUserDetail1.UserType, vmUserDetail1.UserSubType, vmUserDetail1.Id, returnFilePath, jsondata , Countryid ,  userid,  usertype, usersubtype);

            }
            catch (Exception ex)
            {
            }
            return result;

        }

        public List<HolidayTypemaster> BindHolidaytable(HolidayTypemaster model, int Countryid)
        {
            List<HolidayTypemaster> lstholiday = null;
            try
            {
                lstholiday = new List<HolidayTypemaster>();
                lstholiday = _holidayRepository.BindHolidaytable(model,Countryid);

            }
            catch
            {

            }
            return lstholiday;
        }

        public int DeleteBindHolidayTable(int recordId)
        {
            int num = 0;
            try
            {
                num = this._holidayRepository.DeleteBindHolidayTable(recordId);
            }
            catch
            {
            }
            return num;
        }

        public List<HolidayTypeSave> EditBindHolidayTable(int recordId)
        {
            List<HolidayTypeSave> holidayTypeSaves = new List<HolidayTypeSave>();
            holidayTypeSaves = _holidayRepository.EditBindHolidayTable(recordId);

            return holidayTypeSaves;
            
        }

        public List<HoilidayType> GetHolidaydList(string holidayname, int userid, string usertype, string usersubtype)
        {
            List<HoilidayType> lstholidaytype = null;
            List<HoilidayType> hoilidaytype = null;
            lstholidaytype = new List<HoilidayType>();
            try
            {
                string empid = MySession.EmpId;
                if (holidayname != null)
                {
                    lstholidaytype = new List<HoilidayType>();
                    List<HoilidayType> basedoncountry = _holidayRepository.GetHolidaydList(holidayname, userid, usertype, usersubtype);
                    if (basedoncountry.Any<HoilidayType>())
                        hoilidaytype = basedoncountry.Select(x => new HoilidayType()
                        {

                            //id = x.id,
                            //Encryptid = Convert.ToString(Security.GetEncryptString(Convert.ToString(x.id).Trim())),
                            HOLIDAY_DESC = x.HOLIDAY_DESC,
                        }).OrderBy<HoilidayType, string>((Func<HoilidayType, string>)(x => x.HOLIDAY_DESC)).ToList<HoilidayType>();
                }

            }
            catch
            {

            }
            return hoilidaytype;
        }

        public List<HolidayDetails> HolidayList()
        {
            List<HolidayDetails> holidayTypeSaves = new List<HolidayDetails>();
            holidayTypeSaves = _holidayRepository.HolidayList();

            return holidayTypeSaves;

        }
    }
}
