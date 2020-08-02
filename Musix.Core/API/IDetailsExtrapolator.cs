using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Musix.Core.Models;

namespace Musix.Core.API
{
    public interface IDetailsExtrapolator
    {
        ExtrapResult ExtrapolateDetails(string Term);
    }
}
