namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PostComment : IEquatable<PostComment>
    {
        private string commentText;

        public PostComment()
        {
        }

        public PostComment(Guid userID, Guid postID, string text)
        {
            this.CommentID = Guid.NewGuid();
            this.AuthorID = userID;
            this.PostID = postID;
            this.CommentText = text;
            this.CommentCreationTime = DateTime.Now;
        }

        public Guid CommentID { get; set; }

        public Guid AuthorID { get; set; }
        
        public Guid PostID { get; set; }

         public DateTime CommentCreationTime { get; set; }

        public string CommentText
        {
            get
            {
                return this.commentText;
            }

            set
            {             
                if (value.Length >= 3 && value.Length <= 250)
                {
                    this.commentText = value;
                }
                else
                {
                    throw new ArgumentException("Длина текста комментария должна быть от 3 до 250 символов");
                }
            }
        }

        bool IEquatable<PostComment>.Equals(PostComment other)
        {
            return Equals(other);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            PostComment comObj = obj as PostComment;
            if (comObj == null)
            {
                return false;
            }
            else
            {

                if (this.CommentID == comObj.CommentID)
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
            string g = this.CommentID.ToString();
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
