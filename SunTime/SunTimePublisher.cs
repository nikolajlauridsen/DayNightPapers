using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunData
{
    interface SunTimePublisher
    {
        void RegisterSubscriber(SunTimeSubscriber subscriber);
        void SunTimeChanged();
    }
}
