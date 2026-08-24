namespace EventPlus.WebAPI.Interfaces
{
    public interface IModerationService
    {
        // retorna true se o texto foi reprovado (flagged) pela moderacao
        Task<bool> ModerarTexto(string texto);
    }
}
