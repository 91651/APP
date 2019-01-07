using System;
using System.Linq;
using APP.DbAccess.Entities;
using APP.DbAccess.Repositories;
using APP.Business.Services.Models;
using APP.Framework.Util;
using AutoMapper;

namespace APP.Business.Services
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
            entity.Id = Guid.NewGuid().ToString(10);
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
        public FileModel GetFile(string id)
        {
            var entity = _fileRepository.GetById(id);
            return _mapper.Map<FileModel>(entity);
        }
        public FileModel GetFileByMd5(string md5)
        {
            var entity = _fileRepository.GetAll().FirstOrDefault(f => f.Md5 == md5);
            return _mapper.Map<FileModel>(entity);
        }
        public bool IsMultipleOwner(string id, string ownerId)
        {
            return _fileRepository.GetAll().Where(f => f.Id == id && f.OwnerId != ownerId).Any();
        }
    }
}
