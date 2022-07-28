using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaiThikHu.EasyData.Abstractions;

namespace MaiThikHu.EasyData.Identity.Abstractions
{
    public interface IEasyIdentityUserLogin<TKey> : IEasyEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        string LoginProvider { get; set; }

        string ProviderKey { get; set; }

        string ProviderDisplayName { get; set; }

        TKey UserId { get; set; }
    }
}
