using BBS.Constants;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace BBS.Utils
{
    public class GenerateHtmlCertificate
    {
        [Obsolete]
        private IHostingEnvironment _env;

        [Obsolete]
        public GenerateHtmlCertificate(IHostingEnvironment env)
        {
            _env = env;
        }

        [Obsolete]
        public string Execute(CertificateContent certificateContents)
        {
          
            var templateContent = File.ReadAllText(Path.Combine(_env.ContentRootPath, "certificate/" + "index.html"));

            string nameAdded = templateContent.Replace("@Name", certificateContents.Name);
            string numberOfShareAdded = nameAdded.Replace(
                "@noofshares", 
                certificateContents.NumberOfShares.ToString()
            );
            string shareNameAdded = numberOfShareAdded.Replace(
                "@shareName",
                certificateContents.NumberOfShares.ToString()
            );
            string grantTimeAdded = numberOfShareAdded.Replace(
                "@grantTime",
                certificateContents.GrantTime
            );
            string signatureAdded = grantTimeAdded.Replace(
                "@signature", 
                certificateContents.Signature
            );
            string final = signatureAdded.Replace(
                "@companyName",
                certificateContents.CompanyName.ToString()
            );

            return final;
        }
    }


}
