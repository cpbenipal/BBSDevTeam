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
            var htmlContent = "<html lang=\"en\">" +
                "<body>" +
                "<div>" +
                "@Name" + certificateContents.Name + "\n" +
                "@noofshares" + certificateContents.NumberOfShares + "\n" +
                "@shareName" + certificateContents.ShareName + "\n" +
                "@companyName" + certificateContents.CompanyName + "\n" +
                "</div>" +
                "</body>" +
                "</html>";

            return htmlContent;
        }
    }


}
