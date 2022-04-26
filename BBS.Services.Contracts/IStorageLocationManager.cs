
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IStorageLocationManager
    {
        StorageLocation InsertStorageLocation(StorageLocation storageLocation);
        List<StorageLocation> GetAllStorageLocations();
    }
}
