using Microsoft.AspNetCore.Hosting;
using System.Reflection;

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
    }
}
