using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Api.ViewModels
{
    public class PostLoginRequest
    {
        [Required]
        public string? User { get; init; }
    }
}
