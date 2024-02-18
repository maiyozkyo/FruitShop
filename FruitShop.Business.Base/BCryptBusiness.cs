

namespace FShop.Business.Base
{
    public class BCryptBusiness
    {
        public static string Hash(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText)) {
                plainText =  BCrypt.Net.BCrypt.HashPassword(plainText);
            }
            return plainText;
        }

        public static bool Verify(string inputPass, string hashedPass)
        {
            var isSame = false;
            if (!string.IsNullOrEmpty(inputPass) && !string.IsNullOrEmpty(hashedPass))
            {
                isSame = BCrypt.Net.BCrypt.Verify(inputPass, hashedPass);
            }
            return isSame;
        }
    }
}
