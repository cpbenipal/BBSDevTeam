using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace BBS.Utils
{
    public class GetBursaFeesUtil
    {
        private readonly IConfiguration _configuration;
        public GetBursaFeesUtil(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Fetch()
        { 
            return Convert.ToString(_configuration["AppSettings:BusraFee"]);
        }
    }
}
