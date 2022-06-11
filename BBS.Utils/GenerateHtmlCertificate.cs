using BBS.Constants;
using Microsoft.AspNetCore.Hosting;

namespace BBS.Utils
{
    public class GenerateHtmlCertificate
    {
        private readonly IWebHostEnvironment _env;
        public GenerateHtmlCertificate(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string Execute(CertificateContent certificateContents)
        {
            var templateContent = File.ReadAllText(Path.Combine(_env.ContentRootPath, "certificate/" + "index.html"));
            string side1added = templateContent.Replace("@Side1", certificateContents.Side1);
            string side2added = side1added.Replace("@Side2", certificateContents.Side2);
            string nameAdded = side2added.Replace("@Name", certificateContents.Name);
            string numberOfShareAdded = nameAdded.Replace("@noofshares",certificateContents.NumberOfShares.ToString());
            string shareNameAdded = numberOfShareAdded.Replace("@shareName",certificateContents.NumberOfShares.ToString());
            string grantTimeAdded = shareNameAdded.Replace("@grantTime",certificateContents.GrantTime);
            string final = grantTimeAdded.Replace("@companyName",certificateContents.CompanyName);
            string signature = final.Replace("@signature", certificateContents.Signature);
            return signature;
        }
        
    }
}