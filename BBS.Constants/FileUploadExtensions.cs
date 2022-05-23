using System.Collections.Generic;

namespace BBS.Constants
{
    public class FileUploadExtensions
    {
        public static List<string> IMAGE = new List<string>
        {
            ".jpg", ".jpeg",".png"
        };

        public static List<string> PDF = new List<string>
        {
            ".pdf"
        };
        public static List<string> DOCUMENT = new List<string>
        {
            ".jpg", ".jpeg",".png",".pdf"
        };
    }
}
