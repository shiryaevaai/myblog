namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BlogPost
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
                this.postTitle = value;
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

        public void LikePost(Guid userID)
        {
            this.likesList.Add(userID);
        }

        public void DislikePost(Guid userID)
        {
            this.dislikesList.Add(userID);
        }

        public void AddTag(string tag)
        {
            this.postTagList.Add(tag);
        }

        public void AddComment(string comment)
        {
            this.postCommentList.Add(comment);
        }
    }
}
