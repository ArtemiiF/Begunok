using System.Collections.Generic;

namespace YAssistant.Models
{
    public interface IBegunok
    {
        int ActivityCount { get; set; }

        List<IActivity> Activities { get; set; }
    }
}
