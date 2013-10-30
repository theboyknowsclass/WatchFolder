using System.Configuration;
using TheBoyKnowsClass.Common.Desktop.Models.Configuration;
using TheBoyKnowsClass.Schedules.Common.Models;

namespace TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings
{
    [ConfigurationCollection(typeof(ScheduleActivitySetting), AddItemName = "inactive")]
    public class ScheduleSettings : ConfigurationElementCollection<ScheduleActivitySetting>
    {
        public Schedule GetSchedule()
        {
            var schedule = new Schedule();

            foreach (ScheduleActivitySetting scheduleActivitySetting in this)
            {
                for(int i = scheduleActivitySetting.StartTime.Hours; i < scheduleActivitySetting.EndTime.Hours; i++)
                {
                    schedule.ScheduleDays[scheduleActivitySetting.DayOfWeek].SetActivityStatus(i, false);
                }
            }

            return schedule;
        }
    }


}
