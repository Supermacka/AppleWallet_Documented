using AppleWallet.Api.Entities;
using AppleWallet.Library;
using Microsoft.AspNetCore.Mvc;

namespace AppleWallet.Api.Repositories;

public interface IRegistrations
{
    public FileContentResult CreatePass(string companyName, PassImageData imagePaths, PassFieldData[] passFieldsData);
    public Pass? GetPass(string passTypeId, string serialNumber);
}