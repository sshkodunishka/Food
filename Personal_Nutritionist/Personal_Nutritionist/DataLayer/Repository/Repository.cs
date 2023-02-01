using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Nutritionist.DataLayer.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        DbContext _context;
        DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            try
            {
                _context = context;
                _dbSet = context.Set<TEntity>();
            }
            catch
            {
                MessageBox.Show("Can't create repository");
            }
        }

        public IEnumerable<TEntity> Get()
        {
            try
            {
                return _dbSet.AsNoTracking().ToList();
            }
            catch
            {
                MessageBox.Show("Can't get something from repository");
                return null;
            }

        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            try
            {
                return _dbSet.AsNoTracking().Where(predicate).ToList();
            }
            catch
            {
                MessageBox.Show("Can't get something from repository");
                return null;
            }
        }

        public TEntity FindById(int id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch
            {
                MessageBox.Show("Can't find such id in repository");
                return null;
            }
        }

        public void Create(TEntity item)
        {
            try
            {
                _dbSet.Add(item);
                _context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Can't add something into repository");
            }
        }

        public void CreateRange(List<TEntity> items)
        {
            try
            {
                foreach (var item in items)
                    _dbSet.Add(item);
                _context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Can't add range into repository");
            }
        }

        public void Update(TEntity item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                _context.Entry(item).State = EntityState.Detached;
            }
            catch
            {
                MessageBox.Show("Can't update value in repository");
            }
        }

        public void Save(TEntity item)
        {
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Can't save value in repository");
            }
        }

        public void Remove(TEntity item)
        {
            try
            {
                _dbSet.Remove(item);
                _context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Can't remove value from repository");
            }
        }

        public void RemoveDetached(TEntity item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Can't remove value in repository");
            }
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                return Include(includeProperties).ToList();
            }
            catch
            {
                MessageBox.Show("Can't get value from repository");
                return null;
            }
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                var query = Include(includeProperties);
                return query.Where(predicate).ToList();
            }
            catch
            {
                MessageBox.Show("Can't get value from repository");
                return null;
            }
        }

        public IQueryable<TEntity> GetQuery()
        {
            try
            {
                return _dbSet.AsNoTracking();
            }
            catch
            {
                MessageBox.Show("Can't get value from repository");
                return null;
            }
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet.AsNoTracking();
                return includeProperties
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            catch
            {
                MessageBox.Show("Can't include value into repository");
                return null;
            }
        }
    }
}
