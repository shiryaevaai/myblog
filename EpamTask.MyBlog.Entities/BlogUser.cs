namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class BlogUser : IEquatable<BlogUser>
    {
        private string blogUserLogin;

        private string blogUserPassword;

        private DateTime birthDate;

        private string blogUserEpigraph = "";

        private string email;

        private string skype = "";

        private string about = "";

        private bool hasAvatar = false;

        public BlogUser() 
        { 
        }
      
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
                if (value.Length >= 3 && value.Length <= 16)
                {
                    this.blogUserLogin = value;
                }
                else
                {
                    throw new ArgumentException("Длина имени пользователя должна быть от 3 до 16 символов");
                }
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
                if (value.Length >= 6 && value.Length <= 16)
                {
                    this.blogUserPassword = value;
                }
                else
                {
                    throw new ArgumentException("Длина пароля должна быть от 6 до 16 символов");
                }                
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
                if (DateTime.Now < value)
                {
                    throw new ArgumentException("Неверный ввод даты рождения");
                }
                else
                if (DateTime.Now.Year - value.Year > 150)
                {
                    throw new ArgumentException("Неверный ввод даты рождения");
                }
                else
                {
                    this.birthDate = value;
                }                
            }
        }

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
                Regex rgx = new Regex(@"([a-z0-9]([a-z_0-9\.\-]*[a-z0-9])?)@([a-z0-9]([a-z_0-9\-]*)[a-z0-9]\.)+([a-z]{2,6})");
                MatchCollection matches = rgx.Matches(value);

                if (matches.Count == 1)
                {
                    this.email = value;
                }
                else
                {
                    throw new ArgumentException("Некорректный адрес электронной почты");
                }   
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

        public bool Gender { get; set; }

        bool IEquatable<BlogUser>.Equals(BlogUser other)
        {
            return this.Equals(other);
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
