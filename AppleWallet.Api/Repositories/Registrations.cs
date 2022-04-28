using AppleWallet.Api.Entities;
using AppleWallet.Library;
using Microsoft.AspNetCore.Mvc;

namespace AppleWallet.Api.Repositories;

public class Registrations : IRegistrations
{
    private readonly IOneTimeUsePass _oneTimeUse;

    private readonly IFileHandler _fileHandler;
    // private readonly List<Device> _devices = new() { };
    // private readonly List<Pass> passes = new() { };

    public Registrations(IOneTimeUsePass oneTimeUse, IFileHandler fileHandler)
    {
        this._oneTimeUse = oneTimeUse;
        this._fileHandler = fileHandler;
    }
    
    public FileContentResult CreatePass(string companyName, PassImageData imagePaths, PassFieldData[] passFieldsData)
    {
        var passList = new List<byte[]>();
        foreach (var passData in passFieldsData)
        {
            var pass = _oneTimeUse.Create(companyName, imagePaths, passData);
            passList.Add(pass);
        }

        var passes = _fileHandler.GetFile(passList);
        return passes;
    }

    public Pass? GetPass(string passTypeId, string serialNumber)
    {
        // return _passes.Where((p) => p.PassTypeId == passTypeId && p.SerialNumber == serialNumber).FirstOrDefault();
        return new Pass()
        {

        };
    }
    
}