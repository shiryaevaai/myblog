namespace EpamTask.MyBlog.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class Role : IEquatable<Role>
    {
        public Role()
        {
        }

        public Guid ID { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 255 символов")]
        public string RoleName { get; set; }

        bool IEquatable<Role>.Equals(Role other)
        {
            return this.Equals(other);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Role roleObj = obj as Role;
            if (roleObj == null)
            {
                return false;
            }
            else
            {
                if (this.ID == roleObj.ID)
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
