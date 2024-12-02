using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeOps.Logic.Helper
{
    public static class EmployeeIdGenerator
    {
        private static readonly Random _random = new Random();

        public static string GenerateEmployeeId()
        {
            const int idLength = 7;
            const string alphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var uniquePart = new string(Enumerable
                .Range(0, idLength)
                .Select(_ => alphanumericCharacters[_random.Next(alphanumericCharacters.Length)])
                .ToArray());

            return $"UI{uniquePart}";
        }
    }
}
