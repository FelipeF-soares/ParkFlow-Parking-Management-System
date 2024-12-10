using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkFlowData.Repository.Interfaces;

public interface IGenericPersist<T> where T : class
{
    void Add(T entity);
    void Delete(T entity);
    bool SaveChanges();
}
