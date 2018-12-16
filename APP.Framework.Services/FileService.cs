using System;
using System.Collections.Generic;
using System.Linq;
using APP.DbAccess.Entities;
using APP.DbAccess.Repositories;
using APP.Framework.Services.Models;
using AutoMapper;

namespace APP.Framework.Services
{
    public class FileService : IFileServicer
    {
        private readonly IMapper _mapper;
        private readonly IFileRepository _fileRepository;

        public FileService(IMapper mapper, IFileRepository fileRepository)
        {
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public string AddFile(FileModel model)
        {
            var entity = _mapper.Map<File>(model);
            entity.Id = Guid.NewGuid().ToString();
            _fileRepository.Add(entity);
            _fileRepository.SaveChanges();
            return entity.Id;
        }
        public ResultModel DelFile(string id)
        {
            _fileRepository.Remove(id);
            var rows = _fileRepository.SaveChanges();
            return new ResultModel
            {
                Status = rows > 0
            };
        }
        public FileModel GetFileByMd5(string md5)
        {
            var entity = _fileRepository.GetAll().FirstOrDefault(f => f.Md5 == md5);
            return _mapper.Map<FileModel>(entity);
        }
    }
}
