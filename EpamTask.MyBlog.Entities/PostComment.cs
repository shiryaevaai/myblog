namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PostComment : IEquatable<PostComment>
    {
        private string commentText;

        private List<Guid> likesList;

        private List<Guid> dislikesList;

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

        [Required]
        public Guid CommentID { get; set; }

        [Required]
        public Guid AuthorID { get; set; }

        [Required]
        public Guid PostID { get; set; }

        [Required]
        public DateTime CommentCreationTime { get; set; }

        public string CommentText
        {
            get
            {
                return this.commentText;
            }

            set
            {
                this.commentText = value;
            }
        }

        public void LikeComment(Guid userID)
        {
            this.likesList.Add(userID);
        }

        public void DislikeComment(Guid userID)
        {
            this.dislikesList.Add(userID);
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
