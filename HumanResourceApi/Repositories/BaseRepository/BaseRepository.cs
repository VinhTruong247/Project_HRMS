using HumanResourceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceApi.Repositories.BaseRepository
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly HRMSContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository()
        {
            _context = _context = new HRMSContext();
            _dbSet = _context.Set<TEntity>();
        }

        public ICollection<TEntity> GetAll()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception($"Error getting entity: {ex.Message}", ex);
            }
        }

        public TEntity GetById(int id) 
        {
            try
            {
                var entity = _dbSet.Find(id);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting entity: {ex.Message}", ex);
            }
        }

        public void Add(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception($"Error adding entity: {ex.Message}", ex);
            }

        }

        public bool Delete(int id)
        {
            try
            {
                TEntity entity = _dbSet.Find(id);
                var tracker = _context.Attach(entity);
                tracker.State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entity: {ex.Message}");
                return false;
            }
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
