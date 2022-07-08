using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cmms.Core.Domain
{

    public class Audit
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public string PrimaryKey { get; set; }
    }

    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        [Key]
        public Guid Id { get; set; }
        public EntityEntry Entry { get; }
        public string userName { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public string AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();
        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.UserName = userName;
            audit.Type = AuditType.ToString();
            audit.TableName = TableName;
            audit.DateTime = DateTime.Now;
            audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }


}
