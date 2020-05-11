using System;
using System.Collections.Generic;
using System.Text;

namespace ageCalc
{
    public interface IAgeCalculation
    {
        bool Is18(DateTime dob);
    }
}
