using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaiThikHu.EasyData.Identity.Abstractions
{
    public interface IEasyIdentityRoleClaim<TKey> : IEasyIdentityClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey RoleId { get; set; }
    }
}
