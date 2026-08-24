namespace EventPlus.WebAPI.Utils
{
    public class CloudinarySettings
    {
        // nome da conta no cloudinary
        public string CloudName { get; set; } = string.Empty;

        // chave publica de identificacao da API
        public string ApiKey { get; set; } = string.Empty;

        // chave secreta que assina/autentica as requisicoes
        public string ApiSecret { get; set; } = string.Empty;
    }
}
