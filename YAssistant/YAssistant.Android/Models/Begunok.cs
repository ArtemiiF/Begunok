using System.Collections.Generic;
using YAssistant.Models;

namespace YAssistant.Droid.Models
{
    class Begunok : IBegunok
    {
        public Begunok()
        {
            ActivityCount = 0;
            Activities = new List<IActivity>();          
        }

        public int ActivityCount { get; set; }
        public List<IActivity> Activities { get; set; }
    }
}