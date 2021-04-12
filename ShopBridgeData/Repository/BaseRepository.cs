using ShopBridgeData.Model;
using ShopBridgeData.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeData.Repository
{

    /// <summary>
    /// Base repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> where TEntity : class
    {
        private readonly InventoryContext _dbContext;
        private DbSet<TEntity> _dbSet;
        public BaseRepository()
        {
            _dbContext = new InventoryContext();
            this._dbSet = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Add new record in database
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            Save();
        }

        /// <summary>
        ///  Delete record from database
        /// </summary>
        /// <param name="Id">Id to delete record</param>
        public void Delete(int Id)
        {
            TEntity entityToDelete = _dbSet.Find(Id);
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            Save();
        }

        /// <summary>
        ///  Get all records from database
        /// </summary>
        /// <returns>Return collection</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Get record by ID from database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Return matching record</returns>
        public TEntity GetById(int Id)
        {
            return _dbSet.Find(Id);
        }

        /// <summary>
        /// Update record from database
        /// </summary>
        /// <param name="entityToUpdate">Record to update</param>
        public void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// To save changes in database
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
