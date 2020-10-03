using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musix.Models
{
    public enum EAssetModifier
    {
        All = ~0,
        Faded = 0,
        Solid = 1,
        Transparent = 2,
        Hover = 3,
        Selected = 4
    }
}
