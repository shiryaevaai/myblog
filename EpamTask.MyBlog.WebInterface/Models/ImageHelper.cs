namespace EpamTask.MyBlog.WebInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    public class ImageHelper
    {
        public static string FileName { get; private set; }

        public static void SetUserAvatar(HttpPostedFileBase file, Guid userID)
        {
            file.SaveAs(Path.Combine("D:\\", userID.ToString()));
            BusinessLogicHelper._logic.SetUserAvatar(userID, File.ReadAllBytes(Path.Combine("D:\\", userID.ToString())));
            FileName = file.FileName;
        }

        public static void SaveFile(HttpPostedFileBase file)
        {
            file.SaveAs("D:\\myfile");
            FileName = file.FileName;
        }

        public static byte[] GetUserAvatar(Guid userID)
        {
            return BusinessLogicHelper._logic.GetUserAvatar(userID);
        }

        public static byte[] GetFile()
        {
            return File.ReadAllBytes(FileName);
        }
            
    }
}