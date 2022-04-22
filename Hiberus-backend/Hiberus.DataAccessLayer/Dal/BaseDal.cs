using Microsoft.EntityFrameworkCore;
using Hiberus.DataAccessLayer.Dal.Interfaces;
using Hiberus.DataAccessLayer.DataContext;
using System.Linq.Expressions;

namespace Hiberus.DataAccessLayer.Dal
{
    public class BaseDal<T> : IBaseDal<T> where T : class
    {
        protected readonly HiberusDbContext Context;

        public BaseDal(HiberusDbContext appDbContext)
        {
            this.Context = appDbContext;
        }
        /// <summary>
        /// Obtiene una colección de todos los objetos en la base de datos del modelo asociado
        /// </summary>
        /// <remarks>Sincrónico</remarks>
        /// <returns>Un ICollection del objeto en la base de datos.</returns>
        public ICollection<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
        /// <summary>
        /// Obtiene una colección de todos los objetos en la base de datos del modelo asociado
        /// </summary>
        /// <remarks>Asíncrono</remarks>
        /// <returns>Un ICollection del objeto en la base de datos.</returns>
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }
        /// <summary>
        /// Devuelve un solo objeto con la clave principal de la identificación proporcionada
        /// </summary>
        /// <remarks>Síncrono</remarks>
        /// <param name="id"></param>
        /// <returns>Un solo objeto con la clave principal proporcionada o un nulo</returns>
        public T? Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        /// <summary>
        /// Devuelve un solo objeto con la clave principal de la identificación proporcionada
        /// </summary>
        /// <remarks>Asíncrono</remarks>
        /// <param name="id"></param>
        /// <returns>Un solo objeto con la clave principal proporcionada o un nulo</returns>
        public async Task<T?> GetAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }
        /// <summary>
        /// Devuelve un único objeto que coincide con la expresión proporcionada
        /// </summary>
        /// <remarks>Sincrónico</remarks>
        /// <param name="match">Un filtro de expresión Linq para encontrar un solo resultado</param>
        /// <returns>Un único objeto que coincide con el filtro de expresión.
        /// Si se encuentra más de un objeto o si se encuentra cero, se devuelve nulo</returns>
        public T? Find(Expression<Func<T, bool>> match)
        {
            return Context.Set<T>().SingleOrDefault(match);
        }
        /// <summary>
        /// Devuelve un único objeto que coincide con la expresión proporcionada
        /// </summary>
        /// <remarks>Asincrónico</remarks>
        /// <param name="match">Un filtro de expresión Linq para encontrar un solo resultado</param>
        /// <returns>Un único objeto que coincide con el filtro de expresión.
        /// Si se encuentra más de un objeto o si se encuentra cero, se devuelve nulo</returns>

        public async Task<T?> FindAsync(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().SingleOrDefaultAsync(match);
        }
        /// <summary>
        /// Devuelve una colección de objetos que coinciden con la expresión proporcionada
        /// </summary>
        /// <remarks>Sincrónico</remarks>
        /// <param name="match"> Un filtro de expresión de linq para encontrar uno o más resultados</param>
        /// <returns>Una ICollection de objeto que coincide con el filtro de expresión</returns>
        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return Context.Set<T>().Where(match).ToList();
        }

        /// <summary>
        /// Devuelve una colección de objetos que coinciden con la expresión proporcionada
        /// </summary>
        /// <remarks>Asíncrono</remarks>
        /// <param name="match">Un filtro de expresión de linq para encontrar uno o más resultados</param>
        /// <returns>Una ICollection de objeto que coincide con el filtro de expresión</returns>
        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().Where(match).ToListAsync();
        }

        /// <summary>
        /// Inserta un solo objeto en la base de datos y confirma el cambio
        /// </summary>
        /// <remarks>Sincrónico</remarks>
        /// <param name="t">El objeto a insertar</param>
        /// <returns>El objeto resultante, incluida su clave principal después de la inserción</returns>
        public T Add(T t)
        {
            Context.Set<T>().Add(t);
            Context.SaveChanges();
            return t;
        }

        /// <summary>
        /// Inserta un solo objeto en la base de datos y confirma el cambio
        /// </summary>
        /// <remarks>Asincrónico</remarks>
        /// <param name="t">El objeto a insertar</param>
        /// <returns>El objeto resultante, incluida su clave principal después de la inserción</returns>
        public async Task<T> AddAsync(T t)
        {
            Context.Set<T>().Add(t);
            await Context.SaveChangesAsync();
            return t;
        }
        /// <summary>
        /// Actualiza un solo objeto en función de la clave principal proporcionada y confirma el cambio
        /// </summary>
        /// <remarks>Sincrónico</remarks>
        /// <param name="updated">El objeto actualizado para aplicar a la base de datos</param>
        /// <param name="key">La clave principal del objeto a actualizar</param>
        /// <returns>El objeto actualizado resultante</returns>

        public T? Update(T updated, int key)
        {
            if (updated == null)
                return null;

            T? existing = Context.Set<T>().Find(key);
            if (existing != null)
            {
                //CurrentValues.SetValues ​​solo actualizará las propiedades simples del objeto e ignorará cualquier propiedad de navegación del objeto que también pueda haber sido modificada.
                Context.Entry(existing).CurrentValues.SetValues(updated);
                Context.SaveChanges();
            }
            return existing;
        }
        /// <summary>
        /// Actualiza un solo objeto en función de la clave principal proporcionada y confirma el cambio
        /// </summary>
        /// <remarks>Asincrónico</remarks>
        /// <param name="updated">El objeto actualizado para aplicar a la base de datos</param>
        /// <param name="key">La clave principal del objeto a actualizar</param>
        /// <returns>El objeto actualizado resultante</returns>

        public async Task<T?> UpdateAsync(T updated, int key)
        {
            if (updated == null)
                return null;

            T? existing = await Context.Set<T>().FindAsync(key);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                await Context.SaveChangesAsync();
            }
            return existing;
        }

        /// <summary>
        /// Elimina un solo objeto de la base de datos y confirma el cambio
        /// </summary>
        /// <remarks>Sincrónico</remarks>
        /// <param name="t">El objeto a eliminar</param>
        public void Delete(T t)
        {
            T? existing = Context.Set<T>().Find(t);
            if (existing != null)
            {
                Context.Set<T>().Remove(existing);
                Context.SaveChanges();
            }
        }
        /// <summary>
        /// Elimina un solo objeto de la base de datos y confirma el cambio
        /// </summary>
        /// <remarks>Asincrónico</remarks>
        /// <param name="t">El objeto a eliminar</param>
        public async Task DeleteAsync(T t)
        {
            T? existing = await Context.Set<T>().FindAsync(t);
            if (existing != null)
            {
                Context.Set<T>().Remove(existing);
                await Context.SaveChangesAsync();
            }

        }
        /// <summary>
        /// Obtiene el conteo del número de objetos en la base de datos
        /// </summary>
        /// <remarks>Sincrónico</remarks>
        /// <returns>El conteo del número de objetos.</returns>
        public int Count()
        {
            return Context.Set<T>().Count();
        }
        /// <summary>
        /// Obtiene el conteo del número de objetos en la base de datos
        /// </summary>
        /// <remarks>Asincrónico</remarks>
        /// <returns>El conteo del número de objetos.</returns>
        public async Task<int> CountAsync()
        {
            return await Context.Set<T>().CountAsync();
        }
    }
}
