using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaiThikHu.EasyData.Abstractions;

namespace MaiThikHu.EasyData.Identity.Abstractions
{
    public interface IEasyIdentityUserRole<TKey> : IEasyEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey UserId { get; set; }

        TKey RoleId { get; set; } 
    }
}
