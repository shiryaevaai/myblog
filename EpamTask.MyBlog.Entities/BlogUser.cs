namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BlogUser
    {
        [Required]
        private string blogUserLogin;

        [Required]
        private string blogUserPassword;

        [Required]
        private DateTime birthDate;

        private string blogUserEpigraph;

        [Required]
        private string email;

        private string skype;

        private string about;

        private List<string> tagCloud;

        public BlogUser() 
        { 
        }

        public BlogUser(string login, string password, DateTime birth, string e_mail)
        {
            this.BlogUserLogin = login;
            this.BlogUserPassword = password;
            this.BirthDate = birth;
            this.Email = e_mail;
            this.RegistrationTime = DateTime.Now;
            this.HasAvatar = false;
        }

        [Required]
        public Guid ID { get; set; }

        public bool HasAvatar { get; set; }

        public string BlogUserLogin
        {
            get
            {
                return this.blogUserLogin;
            }

            set
            {
                this.blogUserLogin = value;
            }
        }

        public string BlogUserPassword
        {
            get
            {
                return this.blogUserPassword;
            }

            set
            {
                this.blogUserPassword = value;
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return this.birthDate;
            }

            set
            {
                this.birthDate = value;
            }
        }

        [Required]
        public DateTime RegistrationTime { get; private set; }

        public string BlogUserEpigraph
        {
            get
            {
                return this.blogUserEpigraph;
            }

            set
            {
                this.blogUserEpigraph = value;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                this.email = value;
            }
        }

        public string Skype
        {
            get
            {
                return this.skype;
            }

            set
            {
                this.skype = value;
            }
        }

        public int Age { get; set; }

        public bool Sex { get; set; }

        //public void CreatePost(string content, List<string> taglist)
        //{


        //}

        //public void UpdatePost(Guid postID, string content, List<string> taglist)
        //{


        //}
    }
}
