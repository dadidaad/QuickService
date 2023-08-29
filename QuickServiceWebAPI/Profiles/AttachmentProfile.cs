using AutoMapper;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<Attachment, AttachmentDTO>();
            CreateMap<CreateUpdateAttachmentDTO, Attachment>();
            CreateMap<CreateAttachmentDTO, Attachment>().IgnoreAllNonExisting();
        }
    }
}
