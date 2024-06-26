﻿using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class StorageLocationManager : IStorageLocationManager
    {
        private readonly IGenericRepository<StorageLocation> _repositoryBase;

        public StorageLocationManager(IGenericRepository<StorageLocation> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<StorageLocation> GetAllStorageLocations()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public StorageLocation GetStorageLocation(int id)
        {
            return _repositoryBase.GetById(id);
        }

        public StorageLocation InsertStorageLocation(StorageLocation storageLocation)
        {
            var addedStorageLocation = _repositoryBase.Insert(storageLocation);
            _repositoryBase.Save();
            return addedStorageLocation;
        }
    }
}
