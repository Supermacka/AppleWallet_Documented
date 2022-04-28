using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AppleWallet.Library;

public interface IFileHandler
{
    public FileContentResult GetFile(List<byte[]> pass);
    public FileContentResult GetFileBundle(List<byte[]> passes);
}