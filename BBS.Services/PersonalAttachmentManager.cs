using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class PersonalAttachmentManager : IPersonalAttachmentManager
    {
        private readonly IGenericRepository<Attachment> _repositoryBase;

        public PersonalAttachmentManager(IGenericRepository<Attachment> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public Attachment? GetAttachementByPerson(int personId)
        {
            return _repositoryBase.GetAll().FirstOrDefault(a => a.PersonId == personId);
        }

        public Attachment InsertPersonalAttachment(Attachment p)
        {
            var addedPersonalAttachment = _repositoryBase.Insert(p);
            _repositoryBase.Save();
            return addedPersonalAttachment;
        }
    }
}
