using LogoTransfer.Core.DTOs.ProductDTOs;
using LogoTransfer.Core.DTOs.UserDTOs;
using LogoTransfer.Core.Entities;

namespace LogoTransfer.Service.Caching
{
    public class CacheData
    {
        public List<ProductMatchDto> ProductMatches { get; set; }
        public LogoUserDto LogoUser { get; set; }
        public Token Token { get; set; }
    }
}
