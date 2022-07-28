using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaiThikHu.EasyData.Abstractions;

namespace MaiThikHu.EasyData.Identity.Abstractions
{
    public interface IEasyIdentityClaim<TKey> : IEasyEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        string? ClaimType { get; set; }

        string? ClaimValue { get; set; }
    }
}
