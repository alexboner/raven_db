namespace ravendb
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}