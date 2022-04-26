﻿using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IShareManager
    {
        Share InsertShare(Share share);
        List<Share> GetAllSharesForUser(int userLoginId);
    }
}