using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Common.Utils
{
    public class ScheduleUtil
    {
        private const int DayPeriodId = 1;
        private const int WeekPeriodId = 2;
        private const int MonthPeriodId = 3;
        private const int YearPeriodId = 4;

        public static int GetPeriodDays(Schedule sch)
        {
            if (sch.DAYS != null)
                return 7;

            switch (sch.PERIOD_ID)
            {
                case DayPeriodId:
                    return (int)(sch.PERIOD ?? 0);
                case WeekPeriodId:
                    return (int)(sch.PERIOD ?? 0) * 7;
                default:
                    return 0;
            }
        }

        public static DateTime GetStartDate(Schedule sch, DateTime carlendarStartDate)
        {
            var result = sch.START_DATE.Value;
            var span = GetPeriodDays(sch);
            if (span == 0) return DateTime.MaxValue;

            switch (sch.PERIOD_ID)
            {
                case null:

                    // find normal start date               
                    var startDate = GetStartDate(carlendarStartDate, sch.START_DATE.Value, span);

                    // find previous day
                    var dayDiff = GetDayOfWeek(sch.DAYS) - (int)startDate.DayOfWeek;

                    return startDate.AddDays(dayDiff ?? 0);
                case YearPeriodId:
                    var diffYear = carlendarStartDate.Year - sch.START_DATE.Value.Year;
                    var yearToAdd = GetRepeatTime(diffYear, sch.PERIOD ?? 0);
                    return result.AddYears(yearToAdd);
                case MonthPeriodId:
                    var diffMonth = carlendarStartDate.Year - sch.START_DATE.Value.Year;
                    var monthToAdd = GetRepeatTime(diffMonth, sch.PERIOD ?? 0);
                    return result.AddMonths(diffMonth);
                default: // day or week                   
                    return GetStartDate(carlendarStartDate, sch.START_DATE.Value, span);
            }
        }

        public static int? GetDayOfWeek(int? day)
        {
            switch (day)
            {
                case null:
                    return null;
                case 7: // sunday
                    return 0;
                default:
                    return day;

            }
        }

        public static DateTime GetStartDate(DateTime carlendarStartDate, DateTime startDate, int span)
        {
            // หาจำนวนวันจนถึงวันแรกของปฏิทิน
            var diff = carlendarStartDate - startDate;

            // หาว่าภายในจำนวนวันที่ได้ สามารถทำได้กี่ครั้ง
            var repeatTime = GetRepeatTime(diff.Days, span);

            // หาวันที่ต้องบวกเพิ่มจนถึงวันแรกในปฏิทิน
            var dayToAdd = repeatTime * span;

            return startDate.AddDays((int)dayToAdd);
        }

        public static int GetRepeatTime(int totalUnit, decimal unitSpan)
        {
            return (int)Math.Ceiling(totalUnit / unitSpan);

        }

        public static DateTime GetNextDate(Schedule sch, DateTime? PLAN_DATE, DateTime? JOBDATE)
        {
            if (JOBDATE == null)
                return GetNextDate(sch, PLAN_DATE);
            else if (PLAN_DATE == null)
                return GetNextDate(sch, JOBDATE);
            else if (PLAN_DATE > JOBDATE)
                return GetNextDate(sch, PLAN_DATE);
            else
                return GetNextDate(sch, JOBDATE);
        }

        public static DateTime GetNextDate(Schedule sch, DateTime? finishDate)
        {
            if (finishDate == null)
                return DateTime.MaxValue;

            var result = finishDate.Value;
            switch (sch.PERIOD_ID)
            {
                case null:
                    var dayDiff = GetDayOfWeek(sch.DAYS) - (int)result.DayOfWeek;
                    if (dayDiff < 1)
                        dayDiff += 7;
                    return result.AddDays(dayDiff ?? 0);

                case YearPeriodId:
                    return result.AddYears((int?)sch.PERIOD ?? 0);

                case MonthPeriodId:
                    return result.AddMonths((int?)sch.PERIOD ?? 0);

                case WeekPeriodId:
                    return result.AddDays(7 * (int?)sch.PERIOD ?? 0);

                case DayPeriodId:
                    return result.AddDays((int?)sch.PERIOD ?? 0);

                default:
                    return result.AddDays(7);
            }
        }
    }

    public class Schedule
    {
        public int? DAYS { get; set; }
        public decimal? PERIOD { get; set; }
        public int? PERIOD_ID { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
    }
}

