using Microsoft.AspNetCore.Mvc.Rendering;


namespace Attendance.Domain.Helper
{
   
    public class Helper
    {
        public static SelectList GetYearsList(int? iSelectedYear)
        {
            int CurrentYear = DateTime.Now.Year;
            var ddlYears = new List<SelectListItem>();
            for (int i = 2021; i <= CurrentYear; i++)
            {
                ddlYears.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
            return new SelectList(ddlYears, "Value", "Text", iSelectedYear);
        }


    }
}
