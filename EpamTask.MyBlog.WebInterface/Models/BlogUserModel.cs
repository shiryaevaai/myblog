namespace EpamTask.MyBlog.WebInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Security;

    using EpamTask.MyBlog.Logic;
    using EpamTask.MyBlog.Entities;

    public class BlogUserModel : IEquatable<BlogUserModel>
    {
        public static string DefaultImage = BlogUserAvatar.DefaultUserImage;
               
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

        //==================================
        [HiddenInput(DisplayValue = false)]
        public Guid ID { get; set; }

        //==================================
        public bool HasAvatar { get; set; }

        //==================================
        [Required(ErrorMessage = "Необходимо ввести имя пользователя!")]
        [Display(Name = "Имя пользователя")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Длина имени пользователя должна быть от 3 до 16 символов")]
        //[Remote("CheckAccountName", "Account")]
        public string BlogUserLogin { get; set; }

        //==================================
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 16 символов")]
        public string BlogUserPassword { get; set; }

        //==================================
        [Required(ErrorMessage = "Необходимо ввести дату рождения пользователя!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }

        //==================================
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationTime { get; set; }

        //==================================
        public string BlogUserEpigraph { get; set; }

        //==================================
        [Required(ErrorMessage = "Необходимо ввести адрес электронной почты!")]
        //[Remote("CheckEmail", "Account")]
        [RegularExpression(@"([a-z0-9]([a-z_0-9\.\-]*[a-z0-9])?)@([a-z0-9]([a-z_0-9\-]*)[a-z0-9]\.)+([a-z]{2,6})", 
            ErrorMessage = "Некорректный адрес электронной почты")]
        public string Email { get; set; }

        //==================================
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Длина логина в скайпе должна быть от 6 до 32 символов")]
        public string Skype { get; set; }

        //==================================
        [Display(Name = "О себе")]
        public string About { get; set; }

        //==================================
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        //==================================
        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Необходимо ввести пол!")]
        public bool Gender { get; set; }

        //==================================
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

        public static void CreateAccount(RegistrationModel model)
        {
            var account = new BlogUser()
            {
                ID = Guid.NewGuid(),
                BlogUserLogin = model.Login,
                BlogUserPassword = model.Password,
                BirthDate = model.BirthDate,
                Email = model.Email,
                RegistrationTime = DateTime.Now,
                Gender = model.Gender,
                HasAvatar = false,
            };

           // model.ID = account.ID;
            BusinessLogicHelper._logic.AddUser(account);
        }

        //public static bool CheckAccountName(string Username)
        //{
        //    var list = BusinessLogicHelper._logic.GetAllUsers();
        //    foreach (var user in list)
        //    {
        //        if (user.BlogUserLogin == Username)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //public static bool CheckEmail(string email)
        //{
        //    var list = BusinessLogicHelper._logic.GetAllUsers();
        //    foreach (var user in list)
        //    {
        //        if (user.Email == email)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        public static BlogUserModel GetUser(Guid id)
        {
            var user = BusinessLogicHelper._logic.GetUserByID(id);
            BlogUserModel model = new BlogUserModel()
            {
                ID = user.ID,
                BlogUserLogin = user.BlogUserLogin,
                BlogUserPassword = user.BlogUserPassword,
                BirthDate = user.BirthDate,
                Email = user.Email,
                RegistrationTime = user.RegistrationTime,
                Gender = user.Gender,
                HasAvatar = user.HasAvatar,
                About = user.About,
                BlogUserEpigraph = user.BlogUserEpigraph,
                Skype = user.Skype,
            };
            return model;
        }

        public static BlogUserModel GetUser(string username)
        {
            var user = BusinessLogicHelper._logic.GetUserByLogin(username);
            BlogUserModel model = new BlogUserModel()
            {
                ID = user.ID,
                BlogUserLogin = user.BlogUserLogin,
                BlogUserPassword = user.BlogUserPassword,
                BirthDate = user.BirthDate,
                Email = user.Email,
                RegistrationTime = user.RegistrationTime,
                Gender = user.Gender,
                HasAvatar = user.HasAvatar,
                About = user.About,
                BlogUserEpigraph = user.BlogUserEpigraph,
                Skype = user.Skype,
            };
            return model;
        }

        public static bool EditProfile(EditUserModel model)
        {
            var ac = BusinessLogicHelper._logic.GetUserByID(model.ID);

            model.RegistrationTime = ac.RegistrationTime;
            model.HasAvatar = ac.HasAvatar;

            if (String.IsNullOrWhiteSpace(model.About))
            {
                model.About = ac.About;
            }

            if (String.IsNullOrWhiteSpace(model.BlogUserEpigraph))
            {
                model.BlogUserEpigraph = ac.BlogUserEpigraph;
            }

            if (String.IsNullOrWhiteSpace(model.Email))
            {
                model.Email = ac.Email;
            }

            if (String.IsNullOrWhiteSpace(model.Skype))
            {
                model.Skype = ac.Skype;
            }

            if (String.IsNullOrWhiteSpace(model.BlogUserLogin))
            {
                model.BlogUserLogin = ac.BlogUserLogin;
            }

            if (String.IsNullOrWhiteSpace(model.BlogUserPassword))
            {
                model.BlogUserPassword = ac.BlogUserPassword;
            }

            if (DateTime.MinValue != model.BirthDate)
            {
                model.BirthDate = ac.BirthDate;
            }

            var account = new BlogUser()
            {
                ID = model.ID,
                BlogUserLogin = model.BlogUserLogin,
                BlogUserPassword = model.BlogUserPassword,
                BirthDate = model.BirthDate,
                Email = model.Email,
                RegistrationTime = model.RegistrationTime,
                Gender = model.Gender,
                HasAvatar = model.HasAvatar,
                Skype = model.Skype,
                BlogUserEpigraph = model.BlogUserEpigraph,
                About = model.About,
            };

            // model.ID = account.ID;
            return BusinessLogicHelper._logic.UpdateUser(account);
        }
    }
}