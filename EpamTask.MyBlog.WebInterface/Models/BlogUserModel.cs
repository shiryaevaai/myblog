namespace EpamTask.MyBlog.WebInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Security;

    using EpamTask.MyBlog.Logic;
    using EpamTask.MyBlog.Entities;

    public class BlogUserModel : IEquatable<BlogUserModel>
    {
        public static string DefaultImage = BlogUserAvatar.DefaultUserImage;

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

        // private List<RoleModel> roleList;

        public BlogUserModel() 
        { 
        }

        public BlogUserModel(string login, string password, DateTime birth, string e_mail)
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

        public bool Sex { get; set; }

        bool IEquatable<BlogUserModel>.Equals(BlogUserModel other)
        {
            return Equals(other);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            BlogUserModel userObj = obj as BlogUserModel;
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

        //=================
        internal bool TryToLogin(string login, string password)
        {
            BlogUser account = BusinessLogicHelper._logic.GetUserByLogin(login);

            if (account != null)
            {
                if (account.BlogUserLogin == login && account.BlogUserPassword == password)
                {
                    FormsAuthentication.RedirectFromLoginPage(login, createPersistentCookie: true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public static void CreateAccount(BlogUserModel model)
        {
            var account = new BlogUser()
            {
                ID = Guid.NewGuid(),
                BlogUserLogin = model.blogUserLogin,
                BlogUserPassword = model.BlogUserPassword,
                BirthDate = model.BirthDate,
                Email = model.Email,
                RegistrationTime = DateTime.Now,
                HasAvatar = false,
            };

            BusinessLogicHelper._logic.AddUser(account);
        }

        public static bool CheckAccountName(string Username)
        {
            var list = BusinessLogicHelper._logic.GetAllUsers();
            foreach (var user in list)
            {
                if (user.BlogUserLogin == Username)
                {
                    return false;
                }
            }
            return true;
        }
    }
}