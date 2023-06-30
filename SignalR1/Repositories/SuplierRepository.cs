using SignalR1.Models;

namespace SignalR1.Repositories
{
    public class SuplierRepository : ISuplierRepository
    {
        NorthWindContext _context;
        public SuplierRepository(NorthWindContext context)
        {
            _context = context;
        }

        public List<Supplier> GetSupliers()
        {
            return _context.Suppliers.ToList();
        }
    }
}
