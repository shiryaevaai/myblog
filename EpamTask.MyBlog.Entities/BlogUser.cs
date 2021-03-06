﻿namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BlogUser : IEquatable<BlogUser>
    {
        [Required]
        private string blogUserLogin;

        [Required]
        private string blogUserPassword;

        [Required]
        private DateTime birthDate;

        private string blogUserEpigraph="";

        [Required]
        private string email;

        private string skype="";

        private string about="";

        private bool hasAvatar = false;

        private List<string> tagCloud;

        private List<Role> roleList;

        public BlogUser() 
        { 
        }

        //public BlogUser(string login, string password, DateTime birth, string e_mail)
        //{
        //    this.ID = Guid.NewGuid();
        //    this.BlogUserLogin = login;
        //    this.BlogUserPassword = password;
        //    this.BirthDate = birth;
        //    this.Email = e_mail;
        //    this.RegistrationTime = DateTime.Now;
        //    this.HasAvatar = false;
        //}

        [Required]
        public Guid ID { get; set; }

        public bool HasAvatar
        {
            get
            {
                return this.hasAvatar;
            }

            set
            {
                this.hasAvatar = value;
            }
        }

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
        public DateTime RegistrationTime { get; set; }

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

        public string About
        {
            get
            {
                return this.about;
            }

            set
            {
                this.about = value;
            }
        }

        public int Age { get; set; }

        public bool Gender { get; set; }

        // public void CreatePost(string content, List<string> taglist)
        //{
        //}

        // public void UpdatePost(Guid postID, string content, List<string> taglist)
        //{
        //}

        bool IEquatable<BlogUser>.Equals(BlogUser other)
        {
            return Equals(other);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            BlogUser userObj = obj as BlogUser;
            if (userObj == null)
            {
                return false;
            }
            else
            {

                if (this.ID == userObj.ID)
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
            string g = this.ID.ToString();
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
