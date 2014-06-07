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
    public class RegistrationModel
    {
        public RegistrationModel() 
        { 
        }

        [Required(ErrorMessage = "Необходимо ввести имя пользователя!")]
        [Display(Name = "Имя пользователя")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Длина имени пользователя должна быть от 3 до 16 символов")]
        [Remote("CheckAccountName", "Account")]
        public string Login { get; set; }

        //==================================
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 16 символов")]
        public string Password { get; set; }

        //==================================
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Required]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 16 символов")]
        [Remote("ConfirmPassword", "Account", AdditionalFields = "Password")]
        public string PasswordConfirmation { get; set; }

        //==================================
        [Required(ErrorMessage = "Необходимо ввести дату рождения пользователя!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }

        //==================================
        [Required(ErrorMessage = "Необходимо ввести адрес электронной почты!")]
        [Remote("CheckRegEmail", "Account")]       
        [RegularExpression(@"([a-z0-9]([a-z_0-9\.\-]*[a-z0-9])?)@([a-z0-9]([a-z_0-9\-]*)[a-z0-9]\.)+([a-z]{2,6})",
            ErrorMessage = "Некорректный адрес электронной почты")]
        public string Email { get; set; }

        //==================================
        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Необходимо ввести пол!")]
        public bool Gender { get; set; }

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

        public static bool CheckEmail(string email)
        {
            var list = BusinessLogicHelper._logic.GetAllUsers();
            foreach (var user in list)
            {
                if (user.Email == email)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ConfirmPassword(string password, string passwordConfirmation)
        {
            if (password == passwordConfirmation)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}