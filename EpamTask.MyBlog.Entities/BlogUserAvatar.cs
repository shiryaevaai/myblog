namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BlogUserAvatar
    {
        private static string defaultUserImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "UserImages", "default.jpg");
     
        public BlogUserAvatar()
        {
        }

        public BlogUserAvatar(Guid id, byte[] img)
        {
            this.UserID = id;
            this.Image = img;            
        }

        public static string DefaultUserImage
        {
            get
            {
                return defaultUserImage;
            }

            private set
            {
                defaultUserImage = value;
            }
        }

        public Guid UserID { get; private set; }

        public byte[] Image { get; private set; }       
    }
}
