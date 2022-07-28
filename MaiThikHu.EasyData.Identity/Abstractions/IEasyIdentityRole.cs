using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaiThikHu.EasyData.Abstractions;

namespace MaiThikHu.EasyData.Identity.Abstractions
{
    public interface IEasyIdentityRole<TKey> : IEasyEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        string? Name { get; set; }

        string? NormalizedName { get; set; }
    }
}
