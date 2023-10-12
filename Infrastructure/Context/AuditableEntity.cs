
using System;

namespace Infrastructure.Context
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime lastModified { get; set; }
    }
}