using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Utils;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace EventPlus.WebAPI.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinarySettings> options)
        {
            // desempacota a config (CloudName, ApiKey e ApiSecret
            var credenciais = options.Value;


            // account : carteira com as 3 credenciais q autenticam na conta do Cloudinary
            var account = new Account(credenciais.CloudName, credenciais.ApiKey, credenciais.ApiSecret);

            // cria o cliente, ja autenticado com as credenciais
            _cloudinary = new Cloudinary(account);

            // definindo que as URLs(da imagem) geradas precisam ser https (seguras)
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadImagem(IFormFile arquivo)
        {
            using var stream = arquivo.OpenReadStream();

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(arquivo.FileName, stream),
                Folder = "eventplus/eventos"
            };

            // envia a imagem para o cloudinary e aguarda a resposta com os dados do upload 
            var resultado = await _cloudinary.UploadAsync(uploadParams);
            return resultado.SecureUrl.AbsoluteUri;
        }

    }
}
