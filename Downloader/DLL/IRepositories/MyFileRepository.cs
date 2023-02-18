using DLL.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.IRepository
{
    public class MyFileRepository : IRepository<MyFile>
    {
        private readonly EntityContext _entityContext;

        public MyFileRepository(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public void Creat(MyFile data)
        {
            _entityContext.MyFiles.Add(data);
            _entityContext.SaveChanges();
        }

        public void Delete(int Id)
        {
            var data = _entityContext.MyFiles.First(x => x.Id == Id);
            _entityContext.MyFiles.Remove(data);
            _entityContext.SaveChanges();
        }

        public IEnumerable<MyFile> GetFromCondition(Expression<Func<MyFile, bool>> expression)
        {
            return _entityContext.MyFiles.Where(expression).ToList();
        }

        public MyFile GetValue(int Id)
        {
            return _entityContext.MyFiles.First(x => x.Id == Id);
        }

        public void Update(int Id, MyFile data)
        {
            var oldData = _entityContext.MyFiles.Where(x => x.Id == Id).First();


            _entityContext.MyFiles.Remove(oldData);
            _entityContext.MyFiles.Add(data);

            _entityContext.SaveChanges();
        }
    }
}
