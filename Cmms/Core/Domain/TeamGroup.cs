using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{
    public class TeamGroup
    {
        public TeamGroup()
        {
        }
        public Guid Id { get; set; }
        public string TeamGroupName { set; get; }
        public string Description { set; get; }


        public IReadOnlyCollection<Guid> Members => _members;
        private List<Guid> _members = new List<Guid>();


        public void ClearMembers()
        {
            _members.Clear();
        }

        public void AddMembers(List<Guid> ids)
        {
               _members.AddRange(ids);  
        }
        


 
    }
}
