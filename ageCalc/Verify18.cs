using System;

namespace ageCalc
{
    public class Verify18 : IAgeCalculation
    {
        private DateTime _dob;
        private static int _age;


        public bool Is18(DateTime dob)
        {
            _dob = dob;
            CalcAge();
            if (_age > 18 || _age.Equals(18))
            {
                return true;
            }
            return false;
        }


        private void CalcAge()
        {
            int age = 0;

            age = DateTime.Now.Year - _dob.Year;
            
            if (DateTime.Now.DayOfYear < _dob.DayOfYear)
            {
                age = age - 1;
            }

            _age = age;
        }
    }
}
