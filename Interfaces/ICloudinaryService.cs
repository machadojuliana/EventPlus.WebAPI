namespace EventPlus.WebAPI.Interfaces
{
    public interface ICloudinaryService
    {
        // IFormFile : arquivo binario que chega no multipart/form-data
        // é a imagem
        Task<string> UploadImagem(IFormFile arquivo);
    }
}
