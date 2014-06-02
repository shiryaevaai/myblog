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

    public class EditUserModel : IEquatable<EditUserModel>
    {
        public EditUserModel() 
        { 
        }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public Guid ID { get; set; }

        public bool HasAvatar { get; set; }

        //[Required(ErrorMessage = "Необходимо ввести имя пользователя!")]
        [Display(Name = "Имя пользователя")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Длина имени пользователя должна быть от 3 до 16 символов")]
        [Remote("CheckAccountName", "Account")]
        public string BlogUserLogin { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        //[Required]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 16 символов")]
        public string BlogUserPassword { get; set; }

       // [Required(ErrorMessage = "Необходимо ввести дату рождения пользователя!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationTime { get; set; }

        public string BlogUserEpigraph { get; set; }

        //[Required(ErrorMessage = "Необходимо ввести адрес электронной почты!")]
        [Remote("CheckEmail", "Account", AdditionalFields = "ID")]
        [RegularExpression(@"([a-z0-9]([a-z_0-9\.\-]*[a-z0-9])?)@([a-z0-9]([a-z_0-9\-]*)[a-z0-9]\.)+([a-z]{2,6})", 
        ErrorMessage = "Некорректный адрес электронной почты")]
        public string Email { get; set; }

        [StringLength(32, MinimumLength = 6, ErrorMessage = "Длина логина в скайпе должна быть от 6 до 32 символов")]
        public string Skype { get; set; }

        [Display(Name = "О себе")]
        public string About { get; set; }

        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Необходимо ввести пол!")]
        public bool Gender { get; set; }

        bool IEquatable<EditUserModel>.Equals(EditUserModel other)
        {
            return Equals(other);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            EditUserModel userObj = obj as EditUserModel;
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
      
        public static bool CheckEmail(string email, Guid ID)
        {
            var list = BusinessLogicHelper._logic.GetAllUsers();
            foreach (var user in list)
            {
                if (user.Email == email&&user.ID != ID)
                {
                    return false;
                }
            }
            return true;
        }        
        
    }
}