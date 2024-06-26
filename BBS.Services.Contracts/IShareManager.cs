﻿using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IShareManager
    {
        Share InsertShare(Share share);
        Share GetShare(int id);
        List<Share> GetAllSharesForUser(int userLoginId);
        List<Share> GetCompletedSharesForUser(int userLoginId);
        List<Share> GetAllShares();
        Share UpdateShare(Share share);
        List<Share> GetSharesByUserLoginAndCompanyId(int userLoginId,string company);
     }
}
