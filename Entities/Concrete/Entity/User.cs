using Entities.Abstract.Enum;
using Entities.Concrete.BaseEntities;

namespace Entities.Concrete.Entity
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserTypeID { get; set; }

        public virtual UserType UserType { get; set; }
    }
}