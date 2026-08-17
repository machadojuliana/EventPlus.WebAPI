namespace EventPlus.WebAPI.Utils
{
    //metodo estatico
    // criptografia e hashing de senhas na API
    public static class Criptografia
    {
        public static string GerarHash(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public static bool CompararHash(string senhaInformada, string senhaBanco)
        {
            if (string.IsNullOrEmpty(senhaInformada) || string.IsNullOrEmpty(senhaBanco))
            {
                return false;
            }
            try
            {
                return BCrypt.Net.BCrypt.Verify(senhaInformada, senhaBanco);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                return false;
            }
        }
    }
}
