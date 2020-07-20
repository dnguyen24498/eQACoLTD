using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.System.Role.Queries
{
    public class RoleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (!(obj is RoleResponse))
                throw new ArgumentException("obj is not an AssignUserViewModel");
            var usr = obj as RoleResponse;
            if (usr == null)
                return false;
            return this.Id.Equals(usr.Id);
        }
    }
}
