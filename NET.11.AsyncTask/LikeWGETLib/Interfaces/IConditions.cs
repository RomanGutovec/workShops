using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikeWGETLib
{
    public interface IConditions
    {
        bool IsValidByExtension(Uri item);

        bool IsValidByDomain(Uri item);
    }
}
