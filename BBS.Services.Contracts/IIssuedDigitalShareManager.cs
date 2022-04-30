﻿using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IIssuedDigitalShareManager
    {
        IssuedDigitalShare InsertDigitallyIssuedShare(IssuedDigitalShare issuedShare);
        List<IssuedDigitalShare> GetIssuedDigitalSharesByShareIdAndCompanyId(int shareId, int companyId);
    }
}