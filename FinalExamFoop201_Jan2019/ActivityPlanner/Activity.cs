using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner
{
    public class Activity : IComparable
    {
        public string Name { get; set; }
        public DateTime ActivityDate { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }

        public enum ActivityType { Air, Water, Land };

        public ActivityType Type { get; set; }

        public int CompareTo(object obj)
        {
            Activity that = obj as Activity;
            return ActivityDate.CompareTo(that.ActivityDate);
        }
        public Activity(string name, DateTime activityDate, decimal cost, string description, ActivityType type)
        {
            Name = name;
            ActivityDate = activityDate;
            Cost = cost;
            Description = description;
            Type = type;
        }

        public override string ToString()
        {
            return string.Format($"{ Name } - {ActivityDate.ToShortDateString()}");
        }
    }
}
