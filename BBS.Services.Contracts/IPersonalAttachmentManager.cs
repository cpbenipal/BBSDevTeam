using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IPersonalAttachmentManager
    {
        Attachment InsertPersonalAttachment(Attachment personalAttachment);
        Attachment? GetAttachementByPerson(int personId);
    }
}
