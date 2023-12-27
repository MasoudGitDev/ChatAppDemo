using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Messaging.GroupEntity.Models {
    public record GroupInfoUpdateModel {
        public string DisplayId { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
