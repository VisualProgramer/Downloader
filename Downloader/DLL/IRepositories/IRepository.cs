using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Creat(T data);
        void Update(int Id, T data);
        void Delete(int Id);
        T GetValue(int Id);
        IEnumerable<T> GetFromCondition(Expression<Func<T, bool>> expression);
    }
}
