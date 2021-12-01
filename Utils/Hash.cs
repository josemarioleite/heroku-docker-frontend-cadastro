using desafio_pontotel_backend.Models;

namespace desafio_pontotel_backend.Utils
{
    public class Hash
    {
        public void HashPassword(UserRegister user) {
            if(user.PasswordString != null && !string.IsNullOrWhiteSpace(user.PasswordString)){
                using (var hmac = new System.Security.Cryptography.HMACSHA512()){
                    user.Password = hmac.Key;
                    user.Hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.PasswordString));
                }
            }
        }

        public bool HashVerifyPassword(User user, string password)
        {
            if (password != null && !string.IsNullOrWhiteSpace(password)) {
                if (user.Password.Length == 128 && user.Hash.Length == 64) {
                    using (var hmac = new System.Security.Cryptography.HMACSHA512(user.Password)) {
                        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                        for (int i = 0; i < computedHash.Length; i++) {
                            if (computedHash[i] != user.Hash[i]) {
                                return false;
                            }
                        }
                        return true;
                    }
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }
    }
}