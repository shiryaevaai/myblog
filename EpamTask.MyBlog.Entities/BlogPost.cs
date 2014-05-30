
namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class BlogPost
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

        public BlogPost(string login, string password, DateTime birth, string e_mail)
        {
            //this.BlogUserLogin = login;
            //this.BlogUserPassword = password;
            //this.BirthDate = birth;
            //this.Email = e_mail;
            //this.RegistrationTime = DateTime.Now;
            //this.HasAvatar = false;
        }

        public DateTime PostCreationTime { get; private set; }
        [Required]
        public Guid PostID { get; private set; }

        public Guid AuthorID { get; private set; }

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

        private string PostContent
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

        public void AddTag(string tag)
        {
            this.postTagList.Add(tag);
        }

        public void AddComment(string comment)
        {
            this.postCommentList.Add(comment);
        }

        public void LikePost(Guid userID)
        {
            this.likesList.Add(userID);
        }

        public void DislikePost(Guid userID)
        {
            this.dislikesList.Add(userID);
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
    }
}
