using System;

namespace HoangGia.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        HoangGiaDbContext Init();
    }
}