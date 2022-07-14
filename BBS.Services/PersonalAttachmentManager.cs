using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class PersonalAttachmentManager : IPersonalAttachmentManager
    {
        private readonly IGenericRepository<PersonalAttachment> _repositoryBase;

        public PersonalAttachmentManager(IGenericRepository<PersonalAttachment> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public PersonalAttachment? GetAttachementByPerson(int personId)
        {
            return _repositoryBase.GetAll().FirstOrDefault(a => a.PersonId == personId);
        }

        public PersonalAttachment InsertPersonalAttachment(PersonalAttachment personalAttachment)
        {
            var addedPersonalAttachment = _repositoryBase.Insert(personalAttachment);
            _repositoryBase.Save();
            return addedPersonalAttachment;
        }
    }
}
