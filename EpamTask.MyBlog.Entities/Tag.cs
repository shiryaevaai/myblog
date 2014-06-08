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
        public string Title { get; set; }
        
        public Guid AuthorID { get; set; }

        //=============================================
        public Guid PostID { get; set; }

        public Tag()
        {

        }
    }
}
