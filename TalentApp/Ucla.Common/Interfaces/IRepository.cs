using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucla.Common.BaseClasses;

namespace Ucla.Common.Interfaces
{
    public interface IRepository<T> where T : DomainBase
    {
        IEnumerable<T> Fetch(object criteria = null);
        T Persist(T item);
    }
}
