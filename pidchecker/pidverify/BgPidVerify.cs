using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace pidverify
{
    public class BgPidVerify
    {
        public static bool Verify(string pid)
        {
            // Any other operation is unnecessary if the length is not exactly 10 digits
            if (pid.Length != 10 || !pid.All(char.IsDigit))
                return false;

            return CheckBirthdate(pid.Substring(0, 6)) && CheckLastDigit(pid);
        }


        /// <summary>
        /// Check if the characters are parsable date
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        private static bool CheckBirthdate(string birthDate)
        {
            int month = int.Parse(birthDate.Substring(2, 2));
            int year = month > 40 ? 20 : month > 20 ? 18 : 19;

            month = month > 40 ? month - 40 : month > 20 ? month - 20 : month;

            bool isParsed = false;

            DateTime date;

            isParsed = DateTime.TryParse(
                $"{year}{birthDate.Substring(0, 2)}/{month}/{birthDate.Substring(4, 2)}",
                    out date);

            return isParsed;
        }

        /// <summary>
        /// Check the last digit
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        private static bool CheckLastDigit(string pid)
        {
            List<int> weights = new List<int>() { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            int last = int.Parse(pid[9].ToString()); // The last digit

            // Calculate the remainder
            int remainder = (pid.Substring(0, 9)
                .Select((d, i) => (int)char.GetNumericValue(d) * (weights[i])).Sum()) % 11;

            remainder = remainder >= 10 ? 0 : remainder;

            return last == remainder;
        }
    }
}
