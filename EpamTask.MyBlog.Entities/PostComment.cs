namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PostComment
    {
        private string commentText;

        private List<Guid> likesList;

        private List<Guid> dislikesList;

        public PostComment()
        {
        }

        public PostComment(Guid userID, string text)
        {
            this.CommentID = Guid.NewGuid();
            this.AuthorID = userID;
            this.CommentText = text;
            this.CommentCreationTime = DateTime.Now;
        }

        [Required]
        public Guid CommentID { get; set; }

        [Required]
        public Guid AuthorID { get; set; }

        [Required]
        public DateTime CommentCreationTime { get; private set; }

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
    }
}
