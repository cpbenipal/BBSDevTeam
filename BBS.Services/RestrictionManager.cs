﻿using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class RestrictionManager : IRestrictionManager
    {
        private readonly IGenericRepository<Restriction> _repositoryBase;

        public RestrictionManager(IGenericRepository<Restriction> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<Restriction> GetAllRestrictions()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public Restriction GetRestriction(int id)
        {
            return _repositoryBase.GetById(id);
        }

        public Restriction InsertRestriction(Restriction restriction)
        {
            var addedRestriction = _repositoryBase.Insert(restriction);
            _repositoryBase.Save();
            return addedRestriction;
        }
    }
}
