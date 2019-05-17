using SnowFileBar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFileBar.Data
{
    public interface IFileRepository
    {
        void Add<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<FileBarData> GetFileDataById(int id);
        Task<FileBarData[]> GetAllFilesData();
    }
}
