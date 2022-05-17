using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF.Data;
using WCF.Data.Models;

// 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“DService”。
public class DService : IDService
{
    public bool AddResource(WCF.Data.Models.Resource r)
    {
        using (ResourceContext db = new ResourceContext())
        {
            var Rs = db.Resources.Add(r);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool EditResource(WCF.Data.Models.Resource r)
    {
        using (ResourceContext db = new ResourceContext())
        {
            DbEntityEntry<Resource> entry = db.Entry<Resource>(r);

            //**如果使用 Entry 附加 实体对象到数据容器中，则需要手动 设置 实体包装类的对象 的 状态为 Unchanged**
            //**如果使用 Attach 就不需要这句
            entry.State = System.Data.Entity.EntityState.Unchanged;
            //0.2标识 实体对象 某些属性 已经被修改了
            entry.Property("DepID").IsModified = true;
            entry.Property("OrgID").IsModified = true;
            entry.Property("ResNO").IsModified = true;
            entry.Property("ResName").IsModified = true;
            entry.Property("ResDesc").IsModified = true;
            entry.Property("Status").IsModified = true;
            entry.Property("Type").IsModified = true;
            entry.Property("ResType").IsModified = true;
            entry.Property("IsDefault").IsModified = true;
            entry.Property("ResGroupNo").IsModified = true;
            //3.跟新到数据库
            if (db.SaveChanges() > 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool DelResource(int ResID)
    {
        using (ResourceContext db = new ResourceContext())
        {
            var entity = db.Set<Resource>().Find(ResID);
            db.Set<Resource>().Remove(entity);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool DelResources(List<int> ids)
    {
        using (ResourceContext db = new ResourceContext())
        {
            foreach (int id in ids)
            {
                var entity = db.Set<Resource>().Find(id);
                db.Set<Resource>().Remove(entity);
            }
            if (db.SaveChanges() > 0)
            {
                return true;
            }
        }
        return false;
    }


    public List<Resource> QueryResource(string Where)
    {
        List<Resource> list = null;
        //ResourceContext context = new ResourceContext();
        using (ResourceContext db = new ResourceContext())
        {
            string sql = "select * from Resource";
            if (!string.IsNullOrEmpty(Where))
            {
                sql += " where " + Where;
            }
            list =
            db.Database.SqlQuery<Resource>(
                sql).ToList<Resource>();
        }
        return list;
    }

    public decimal GetMaxID()
    {
        using (ResourceContext db = new ResourceContext())
        {
            string sql = "select max(ResID) from Resource";
            var r= db.Database.SqlQuery<decimal>(sql);
            try
            {
                return r.ElementAt(0);
            }
            catch
            {
                return 0m;
            }
            
        }
        return 0m;
    }
}
