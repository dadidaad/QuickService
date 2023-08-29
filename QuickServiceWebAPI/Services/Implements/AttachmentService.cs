using AutoMapper;
using Microsoft.Extensions.Options;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.Helpers;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;
using System.Text.RegularExpressions;

namespace QuickServiceWebAPI.Services.Implements
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly AzureStorageConfig _storageConfig;
        private readonly ILogger<AttachmentService> _logger;

        public AttachmentService(IAttachmentRepository repository, IMapper mapper,
            IOptions<AzureStorageConfig> storageConfig, ILogger<AttachmentService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _storageConfig = storageConfig.Value;
            _logger = logger;
        }

        public List<AttachmentDTO> GetAttachments()
        {
            var attachments = _repository.GetAttachments();
            return attachments.Select(attachment => _mapper.Map<AttachmentDTO>(attachment)).ToList();
        }

        public async Task<AttachmentDTO> GetAttachmentById(string attachmentId)
        {
            var attachment = await _repository.GetAttachmentById(attachmentId);
            if (attachment == null)
            {
                throw new AppException("Attachment not found");
            }
            return _mapper.Map<AttachmentDTO>(attachment);
        }

        public async Task<Attachment> CreateAttachment(IFormFile file)
        {
            var attachment = await UploadAttachment(file);
            await _repository.AddAttachment(attachment);
            return attachment;
        }

        public async Task UpdateAttachment(string attachmentId, CreateUpdateAttachmentDTO createUpdateAttachmentDTO)
        {
            Attachment attachment = await _repository.GetAttachmentById(attachmentId);
            if (attachment == null)
            {
                throw new AppException("Attachment not found");
            }
            attachment = _mapper.Map(createUpdateAttachmentDTO, attachment);
            await _repository.UpdateAttachment(attachment);
        }

        public async Task DeleteAttachment(string attachmentId)
        {
            Attachment attachment = await _repository.GetAttachmentById(attachmentId);
            if (attachment == null)
            {
                throw new AppException("Attachment not found");
            }
            await CloudHelper.DeleteBlob(attachment.FilePath, _storageConfig);
            await _repository.DeleteAttachment(attachment);
        }
        public async Task<string> GetNextId()
        {
            Attachment lastAttachment = await _repository.GetLastAttachment();
            int id = 0;
            if (lastAttachment == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastAttachment.AttachmentId) + 1;
            }
            string attachmentId = IDGenerator.GenerateAttachmentId(id);
            return attachmentId;
        }

        public async Task<Attachment> UploadAttachment(IFormFile attachmentFile)
        {
            var attachment = new Attachment();
            try
            {
                if (_storageConfig.AccountKey == string.Empty || _storageConfig.AccountName == string.Empty)
                    throw new AppException("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                if (_storageConfig.ImageContainer == string.Empty)
                    throw new AppException("Please provide a name for your attachment container in the azure blob storage");


                if (attachmentFile.Length > 0 && attachmentFile.Length <= 4194304)
                {
                    using (Stream stream = attachmentFile.OpenReadStream())
                    {
                        attachment.AttachmentId = await GetNextId();
                        attachment.Filename = attachmentFile.FileName;
                        //string fileNameStore = attachment.AttachmentId + GetFileExtension(attachmentFile);
                        string fileNameStore = $"{attachment.AttachmentId}_{RemoveVietnameseTone(attachmentFile.FileName)}";
                        attachment.FilePath = await CloudHelper.UploadFileToStorage(stream, fileNameStore, _storageConfig, _storageConfig.AttachmentContainer);
                        attachment.FileSize = Convert.ToInt32(attachmentFile.Length);
                        attachment.CreatedAt = DateTime.Now;
                    }
                }
                else
                {
                    throw new AppException("File size is not valid");
                }


                if (attachment.FilePath != null)
                {
                    return attachment;
                }
                else
                {
                    throw new AppException("Error when try to upload attachment!!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new AppException("Error when try to upload image!!");
            }
        }

        public string GetFileExtension(IFormFile file)
        {
            // Get the file name from the ContentDisposition header
            var fileName = file.FileName;

            // Get the file extension
            var fileExtension = Path.GetExtension(fileName);

            return fileExtension;
        }
        public string RemoveVietnameseTone(string text)
        {
            string result = text.ToLower();
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }
    }
}
