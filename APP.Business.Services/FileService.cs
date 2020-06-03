using System;
using System.Linq;
using APP.DbAccess.Entities;
using APP.DbAccess.Repositories;
using APP.Business.Services.Models;
using APP.Framework.Util;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public async Task<bool> DelFileAsync(string id)
        {
            _fileRepository.Remove(id);
            var rows = await _fileRepository.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<FileModel> GetFileAsync(string id)
        {
            var entity = await _fileRepository.GetByIdAsync(id);
            return _mapper.Map<FileModel>(entity);
        }
        public async Task<FileModel> GetFileByMd5Async(string md5)
        {
            var entity = await _fileRepository.GetAll().FirstOrDefaultAsync(f => f.Md5 == md5);
            return _mapper.Map<FileModel>(entity);
        }
        public async Task<bool> IsMultipleOwnerAsync(string id, string ownerId)
        {
            return await _fileRepository.GetAll().Where(f => f.Id == id && f.OwnerId != ownerId).AnyAsync();
        }
    }
}
