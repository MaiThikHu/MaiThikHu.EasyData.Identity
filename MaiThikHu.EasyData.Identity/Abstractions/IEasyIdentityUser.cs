using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaiThikHu.EasyData.Abstractions;

namespace MaiThikHu.EasyData.Identity.Abstractions
{
    public interface IEasyIdentityUser<TKey> : IEasyEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        string UserName { get; set; }

        string NormalizedUserName { get; set; }

        string Email { get; set; }

        string NormalizedEmail { get; set; }

        bool EmailConfirmed { get; set; }
    }
}
