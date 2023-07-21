using AutoMapper;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _repository;
        private readonly IMapper _mapper;
        public AttachmentService(IAttachmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<AttachmentDTO> GetAttachments()
        {
            var attachments = _repository.GetAttachments();
            return attachments.Select(attachment => _mapper.Map<AttachmentDTO>(attachment)).ToList();
        }

        public async Task<AttachmentDTO> GetAttachmentById(string attachmentId)
        {
            var attachment = await _repository.GetAttachmentById(attachmentId);
            return _mapper.Map<AttachmentDTO>(attachment);
        }

        public async Task CreateAttachment(CreateUpdateAttachmentDTO createUpdateAttachmentDTO)
        {
            var attachment = _mapper.Map<Attachment>(createUpdateAttachmentDTO);
            attachment.AttachmentId = await GetNextId();
            attachment.CreatedAt = DateTime.Now;
            await _repository.AddAttachment(attachment);
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
            string businessHourId = IDGenerator.GenerateAttachmentId(id);
            return businessHourId;
        }
    }
}
