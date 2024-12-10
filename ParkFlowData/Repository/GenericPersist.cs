using ParkFlowData.DataBaseContext;
using ParkFlowData.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkFlowData.Repository;

public class GenericPersist<T> : IGenericPersist<T> where T : class
{
    private readonly ParkFlowContext context;

    public GenericPersist(ParkFlowContext context)
    {
        this.context = context;
    }
    public void Add(T entity)
    {
        context.Add(entity);
    }

    public void Delete(T entity)
    {
        context.Remove(entity);
    }

    public bool SaveChanges()
    {
       return context.SaveChanges() > 0;
    }
}
