using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaiThikHu.EasyData.Identity.Abstractions
{
    public interface IEasyIdentityUserClaim<TKey> : IEasyIdentityClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        //int Id { get; set; }
        
        TKey UserId { get; set; }
    }
}
