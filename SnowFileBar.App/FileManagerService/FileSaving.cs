using AutoMapper;
using SnowFileBar.App.Model;
using SnowFileBar.Data;
using SnowFileBar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFileBar.App.FileManagerService
{
    public class FileSaving
    {
        private readonly IMapper mapper;
        private readonly IFileRepository fileRepository;

        public FileSaving(IMapper mapper, IFileRepository fileRepository)
        {
            this.mapper = mapper;
            this.fileRepository = fileRepository;
        }
        public async  Task SavingFileDataToDataBase(string filePath)
        {
            var fileParseManager = new FileParse();
            var fileBarDataModel = new FileBarDataModel();
            //parsing file data
            var resultFileBar = fileParseManager.ProcessFile(filePath);
            foreach (var item in resultFileBar)
            {

                fileBarDataModel.ColourName = item.ColourName;
                fileBarDataModel.Size = item.Size;
                var parseFileBarData = mapper.Map<FileBarData>(fileBarDataModel);
                fileRepository.Add(parseFileBarData);
                await fileRepository.SaveChangesAsync();

            }

        }
    }
}
