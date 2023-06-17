using LogoTransfer.Core.DTOs.ProductDTOs;
using LogoTransfer.Core.DTOs.UserDTOs;

namespace LogoTransfer.Service.Caching
{
    public class CacheData
    {
        public List<ProductMatchDto> ProductMatches { get; set; }
        public LogoUserDto LogoUser { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public DateTime? Created { get; set; }
    }
}
