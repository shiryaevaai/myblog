namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Tag
    {
        private string title;

        public Tag()
        {
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (value.Length >= 0 && value.Length <= 50)
                {
                    this.title = value;
                }
                else
                {
                    throw new ArgumentException("Длина тэга должна быть не больше 50 символов");
                }
            }
        }
        
        public Guid AuthorID { get; set; }

        // =============================================
        public Guid PostID { get; set; }
    }
}
