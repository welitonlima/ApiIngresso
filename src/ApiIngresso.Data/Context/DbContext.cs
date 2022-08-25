using Microsoft.Extensions.Configuration;

namespace ApiIngresso.Data.Context
{
    public class DbContext
    {
        public IConfiguration _configuration;
        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            return _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }
    }
}
