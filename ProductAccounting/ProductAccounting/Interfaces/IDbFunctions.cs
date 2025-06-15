using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Interfaces
{
    public interface IDbFunctions
    {
        Task DeleteItem<T>(T itemForDel, Expression<Func<T, bool>> predicate) where T: class;
        Task AddData<T>(T dataObject) where T : class;
        Task ChangeData<T>(T dataObject) where T : class;
    }
}
