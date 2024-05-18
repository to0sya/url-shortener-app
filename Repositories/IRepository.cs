namespace url_shortener.Repositories
{
    public interface IRepository<TEntityType, TKeyType> where TEntityType : class
    {
        Task<IEnumerable<TEntityType>> GetAll();

        Task<TEntityType> Get(TKeyType param);

        Task<TEntityType> Add(TEntityType entity);

        Task<int> Delete(int id);
    }
}
