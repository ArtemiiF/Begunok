using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using BegunokApp.Models;

namespace BegunokApp.DB
{
    [Table("Begunok")]
    public class BegunokDB
    {
        public BegunokDB()
        {

        }

        public BegunokDB(IActivity activity)
        {
            Name = activity.Name;
            TimeInSeconds = Convert.ToInt32(activity.Time.TotalSeconds);
            Color = activity.Color.ToHex();
            Length = activity.Length;
            State = activity.State;
        }

        public BegunokDB(IActivity activity, int id) : this(activity)
        {
            Id = id;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public int TimeInSeconds { get; set; }
        public string Color { get; set; }
        public int Length { get; set; }
        public ActivityState State { get; set; }
    }
}
