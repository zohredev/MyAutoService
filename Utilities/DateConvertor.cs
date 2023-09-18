using System.Globalization;

namespace MyAutoService.Utilities
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(dateTime) + "/" +
                  persianCalendar.GetMonth(dateTime).ToString("00") + "/" +
                  persianCalendar.GetDayOfMonth(dateTime).ToString("00");
        }
    }
}
