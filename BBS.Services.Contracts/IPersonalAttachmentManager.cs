using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IPersonalAttachmentManager
    {
        PersonalAttachment InsertPersonalAttachment(PersonalAttachment personalAttachment);
    }
}
