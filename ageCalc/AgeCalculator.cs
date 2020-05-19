using System;

namespace ageCalc
{
    public class AgeCalculator : IAgeCalculation
    {
        private DateTime _dob;
        private int _age;


        public bool Is18(DateTime dob)
        {
            _dob = dob;
            CalcAge();
            if (_age >= 18)
            {
                return true;
            }
            return false;
        }


        private void CalcAge()
        {
            int age = DateTime.Now.Year - _dob.Year;
            if (DateTime.IsLeapYear(DateTime.Now.Year))
            {
                if (DateTime.Now.DayOfYear <= _dob.DayOfYear)
                {
                    age -= 1;
                }
            }
            else
            {
                if (DateTime.Now.DayOfYear < _dob.DayOfYear)
                {
                    age -= 1;
                }
            }

            _age = age;
        }
    }
}
