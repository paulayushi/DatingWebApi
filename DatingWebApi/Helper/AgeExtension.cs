using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebApi.Helper
{
    public static class AgeExtension
    {
        public static int CalculateAge(this DateTime dob)
        {
            var age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.AddYears(age) < DateTime.Now)
                age--;

            return age;
        }
    }
}
