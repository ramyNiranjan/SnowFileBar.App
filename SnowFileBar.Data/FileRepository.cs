using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SnowFileBar.Domain;

namespace SnowFileBar.Data
{
    public class FileRepository : IFileRepository
    {
        private readonly FileDataContext fileDataContext;

        public FileRepository(FileDataContext fileDataContext)
        {
            this.fileDataContext = fileDataContext;
        }
        public void Add<T>(T entity) where T : class
        {
            fileDataContext.Add(entity);
        }

        public async Task<FileBarData[]> GetAllFilesData()
        {
            IQueryable<FileBarData> query = fileDataContext.FilesData;
            //query.OrderBy(c => c.ColourName);
            return await query.ToArrayAsync();
        }

        public async Task<FileBarData> GetFileDataById(int id)
        {
            IQueryable<FileBarData> query = fileDataContext.FilesData.Where(c => c.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await fileDataContext.SaveChangesAsync()) > 0;
        }
    }
}
