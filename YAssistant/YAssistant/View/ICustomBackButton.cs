using System;
using System.Collections.Generic;
using System.Text;

namespace YAssistant.View
{
    public interface ICustomBackButton
    {
        Action CustomBackButtonAction { get; set; }
    }
}
