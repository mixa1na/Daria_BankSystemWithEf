using BankSystem.EF.Entities;

namespace BankSystem.Services.Services;

public class BaseService : IDisposable
{
    private bool disposed;

    public BaseService(BankContext bankContext)
    {
        this.Context = bankContext ?? throw new ArgumentNullException(nameof(bankContext));
    }

    protected BankContext Context { get; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.disposed)
        {
            return;
        }

        if (disposing)
        {
            this.Context.Dispose();
        }

        this.disposed = true;
    }
}
