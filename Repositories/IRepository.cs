namespace url_shortener.Repositories
{
    public interface IRepository<TEntityType, TKeyType> where TEntityType : class
    {
        Task<IEnumerable<TEntityType>> GetAll();

        Task<TEntityType> Get(TKeyType id);

        Task<TEntityType> Add(TEntityType entity);

        Task<TKeyType> Delete(TKeyType id);
    }
}
