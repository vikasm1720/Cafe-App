namespace CafeOps.API.Utils
{
    public class EmployeeValidator
    {

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length == 8 && (phoneNumber.StartsWith("9") || phoneNumber.StartsWith("8"));
        }
    }
   }
