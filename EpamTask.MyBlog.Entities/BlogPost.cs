namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BlogPost : IEquatable<BlogPost>
    {
        private string postTitle;

        private string postContent;

        private List<string> postTagList;

        private List<string> postCommentList;

        private List<Guid> likesList;

        private List<Guid> dislikesList;

        private string privacy;

        public BlogPost() 
        { 
        }

        public BlogPost(Guid userID, string title, string content)
        {
            this.PostID = Guid.NewGuid();
            this.AuthorID = userID;
            this.PostTitle = title;
            this.PostContent = content;
            this.PostCreationTime = DateTime.Now;
        }

        [Required]
        public DateTime PostCreationTime { get; set; }
        [Required]
        public Guid PostID { get; set; }

        [Required]
        public Guid AuthorID { get; set; }

        public string PostTitle 
        {
            get
            {
                return this.postTitle;
            }

            set
            {
                if (value.Length <= 100)
                {
                    this.postTitle = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public string PostContent
        {
            get
            {
                return this.postContent;
            }

            set
            {
                this.postContent = value;
            }
        }

        public string Privacy
        {
            get
            {
                return this.privacy;
            }

            set
            {
                this.privacy = value;
            }
        }
        
        bool IEquatable<BlogPost>.Equals(BlogPost other)
        {
            return Equals(other);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }                

            BlogPost postObj = obj as BlogPost;
            if (postObj == null)
            {
                return false;
            }
            else
            {

                if (this.PostID == postObj.PostID)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override int GetHashCode()
        {
            int res = 0;
            string g = this.PostID.ToString();
            for (int i = 0; i < g.Length; i++)
            {
                if (Char.IsDigit(g[i]))
                {
                    res += Int16.Parse(g[i].ToString());
                }
            }

            return res;
        }
    }
}
