using System;
using System.Linq;
using APP.DbAccess.Entities;
using APP.DbAccess.Repositories;
using APP.Business.Services.Models;
using APP.Framework.Util;
using AutoMapper;
using System.Threading.Tasks;

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

        public async Task<string> AddFileAsync(FileModel model)
        {
            var entity = _mapper.Map<File>(model);
            entity.Id = Guid.NewGuid().ToString(10);
            await _fileRepository.AddAsync(entity);
            await _fileRepository.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<ResultModel> DelFileAsync(string id)
        {
            _fileRepository.Remove(id);
            var rows = await _fileRepository.SaveChangesAsync();
            return new ResultModel
            {
                Status = rows > 0
            };
        }
        public async Task<FileModel> GetFile(string id)
        {
            var entity = await _fileRepository.GetByIdAsync(id);
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
