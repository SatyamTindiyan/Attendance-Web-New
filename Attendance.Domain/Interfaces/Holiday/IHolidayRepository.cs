using Attendance.Domain.Models.Holiday;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain.Interfaces.Holiday
{
    public interface IHolidayRepository
    {

        List<Holidays> GetHolidayList();
        List<HolidayTypeSave> GetHolidaymasterList(HolidayTypeSave modal, int userid, string usertype, string usersubtype);
        List<HoilidayType> GetHolidayDesc(int countryID, int userid, string usertype, string usersubtype);
        List<HolidayMaster> GetstatelistbasedonCountryId(int countryID, int userid, string usertype, string usersubtype);
        int SavecmDataFromExcel(HolidayTypeSave obj, string userType, string userSubType, int id, string returnFilePath, string jsondata , int Countryid);

        //int SaveHolidayType(HolidayMaster modal);
        int SaveHolidayType(HolidayTypeSave modal);
        List<HolidayMaster> GetHolidayMasterDetails( int countryID);
        int SaveHolidayDataFromExcel(HolidayTypeSave obj, string userType, string userSubType, int id, string returnFilePath, string jsondata, int Countryid, string userid, string usertype, string usersubtype);
        List<HolidayTypemaster> BindHolidaytable(HolidayTypemaster model, int Countryid);
        int DeleteBindHolidayTable(int recordId);
        List<HolidayTypeSave> EditBindHolidayTable(int recordId);
        List<HoilidayType> GetHolidaydList(string holidayname, int userid, string usertype, string usersubtype);
        List<HolidayDetails> HolidayList();
    }
}
