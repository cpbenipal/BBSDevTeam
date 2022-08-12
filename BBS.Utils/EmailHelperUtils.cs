using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using System.Text;

namespace BBS.Utils
{
    public class EmailHelperUtils
    {
        private readonly IWebHostEnvironment _env;
        public EmailHelperUtils(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string FillEmailContents(
            object dataToFill, 
            string fileName, 
            string personFirstName, 
            string personLastName
        )
        {

            var templateContent = File.ReadAllText(
                Path.Combine(_env.ContentRootPath, "email_templates/" +  fileName + ".html")
            );


            templateContent = templateContent.Replace("@PersonFirstName", personFirstName);
            templateContent = templateContent.Replace("@PersonLastName", personLastName);


            foreach (PropertyInfo prop in dataToFill.GetType().GetProperties())
            {
                var propName = "@" + Convert.ToString(prop.Name);
                var propValue = Convert.ToString(prop.GetValue(dataToFill, null)!);
                templateContent = templateContent.Replace(propName, propValue);
            }

            templateContent = templateContent.Replace("True", "Yes");
            templateContent = templateContent.Replace("False", "No");

            return templateContent;
        }
        public string FillDynamicEmailContents(Dictionary<string, string> dataToFill,string fileName,string personFirstName,string personLastName) 
        {
            var templateContent = File.ReadAllText(
                Path.Combine(_env.ContentRootPath, "email_templates/" + fileName + ".html")
            );
             

            templateContent = templateContent.Replace("@PersonFirstName", personFirstName);
            templateContent = templateContent.Replace("@PersonLastName", personLastName);

            StringBuilder stringBuilder = new StringBuilder();
            string Title = "<td width=\"33%\" style=\"-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; mso-line-height-rule:exactly;\"><p style=\"margin: 0px; font-size: 14px; text-align: center; color: #333; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-line-height-rule:exactly; line-height:1.5;\">@Title</p></td>";
            string Content = "<td style=\"-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt; mso-line-height-rule:exactly;\"><p style=\"margin: 0px; font-size: 14px; text-align: center; color: #333; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-line-height-rule:exactly; line-height:1.5;\">@Content</p></td>";

            foreach (KeyValuePair<string, string> ele2 in dataToFill)
            {
                //   Console.WriteLine("{0} and {1}", ele2.Key, ele2.Value);

                stringBuilder.Append("<tr>");
                stringBuilder.Append(Title.Replace("@Title", ele2.Key));
                stringBuilder.Append(Content.Replace("@Content", ele2.Value));
                stringBuilder.Append("</tr>");
            }

            templateContent = templateContent.Replace("@PrimaryOfferContent", stringBuilder.ToString());

            templateContent = templateContent.Replace("True", "Yes");
            templateContent = templateContent.Replace("False", "No");

            return templateContent;
        }
    }
}
