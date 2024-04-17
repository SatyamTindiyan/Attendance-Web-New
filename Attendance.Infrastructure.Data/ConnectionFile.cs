using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Data
{
    public static class ConnectionFile
    {
        // ANTS_PreProd_new ANTSDBTemp ANTS_PreProd
       // public static string db_NewApp = "Data Source=13.126.69.214;Initial Catalog=db_NewApp;User Id=sa;Password=sa@123#;Integrated Security=True;Trusted_Connection=False;";
        //// for server publish
        //   public static string db_ANTSDBTemp = "Data Source=203.122.13.198;Initial Catalog=ANTS_PreProd;User Id=SA;Password=G25@P201301;Integrated Security=True;Trusted_Connection=False;";
        ///// for local
        //public static string db_ANTSDBTemp = "Data Source=203.122.13.198;Initial Catalog=ANTSDBTemp;User Id=SA;Password=G25@P201301;Integrated Security=True;Trusted_Connection=False;";
        // public static string db_ANTSDBTemp = "Data Source=203.122.13.198;Initial Catalog=ANTS_PreProd;User Id=SA;Password=G25@P201301;Integrated Security=True;Trusted_Connection=False;";
        // public static string db_ANTSDBTemp = ConfigurationManager.AppSettings["Connection_ANTSDB"].ToString();
       // public static string Attendance = "Data Source=192.168.1.198;Initial Catalog=Ants_PreProd_test;User Id=SA;Password=G25@P201301;Integrated Security=True;Trusted_Connection=False;";
       // public static string Attendance = "Data Source=192.168.1.164;Initial Catalog=ANTS_DevMod;User Id=abhik;Password=Shiv$Cdb#9876;Integrated Security=True;Trusted_Connection=False;";
        public static string Attendance = "Data Source=192.168.1.164\\ATLCDBA;Initial Catalog=HRMS;User Id=Abhik;Password=SShiv$Cdb#9876;Integrated Security=True;Trusted_Connection=False;";
        // public static string db_Oracal = "Data Source = (description = (address = (protocol = tcp)(host = 192.168.1.203)(port = 1521))(connect_data = (service_name = orcl))); User ID = lqomms; Password=lqomms;";
    }
}
