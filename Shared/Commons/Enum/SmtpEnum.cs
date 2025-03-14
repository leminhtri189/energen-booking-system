using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Commons.Enum
{
    public enum DefaultSmtpServer
    {
        [Description("smtp.gmail.com")]
        Google = 587,

        [Description("smtp-mail.outlook.com")]
        Outlook = 587,
    }
}
