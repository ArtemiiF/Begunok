using System.Collections.Generic;
using YAssistant.Models;

namespace YAssistant.Droid.Models
{
    class Begunok : IBegunok
    {
        public List<IActivity> begunok { get; set; }
    }
}