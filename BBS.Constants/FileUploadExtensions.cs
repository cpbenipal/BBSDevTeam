using System.Collections.Generic;

namespace BBS.Constants
{
    public static class FileUploadExtensions
    {
        public static List<string> IMAGE { get; } = new List<string>
        {
            ".jpg", ".jpeg",".png",".PNG", ".JPEG", ".JPG"
        };

        public static List<string> PDF { get; } = new List<string>
        {
            ".pdf",".PDF"
        };

        public static List<string> DOCUMENT { get; } = new List<string>
        {
            ".jpg", ".jpeg",".png",".PNG", ".JPEG", ".JPG",".pdf",".PDF"
        };
    }
}
