using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibraray.API.Helpers
{
    public static class DateTimeOffsetExtensions
    {

        /// <summary>
        /// GetCurrentAge
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTimeOffset.Year;
            if (currentDate < dateTimeOffset.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
}
