using Entities.Concrete.BaseEntities;
using System.Collections;
using System.Collections.Generic;

namespace Entities.Concrete.Entity
{
    public class UserType : BaseEntity
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public string UserTypeName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}