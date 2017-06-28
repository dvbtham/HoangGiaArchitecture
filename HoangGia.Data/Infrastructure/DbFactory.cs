namespace HoangGia.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private HoangGiaDbContext _dbContext;

        public HoangGiaDbContext Init()
        {
            return _dbContext ?? (_dbContext = new HoangGiaDbContext());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}