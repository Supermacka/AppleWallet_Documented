using System.Collections.Generic;
using System.IO.Compression;

using Microsoft.AspNetCore.Mvc;

namespace AppleWallet.Library;

public class FileHandler : IFileHandler
{
    // Distributing mutlipe passes - https://developer.apple.com/documentation/walletpasses/distributing_and_updating_a_pass

    /// <summary>
    /// Creates a .pkpass file
    /// </summary>
    /// <param name="pass">List<byte[]> pass</param>
    /// <returns>FileContentResult</returns>
    public FileContentResult GetFile(List<byte[]> pass)
    {
        // foreach (var pass in passes)
        // {
        //     return new FileContentResult(pass, "application/vnd.apple.pkpass");
        // }

        return new FileContentResult(pass.First(), "application/vnd.apple.pkpass");
    }

    public FileContentResult GetFileBundle(List<byte[]> passes)
    {
        using (var compressedFileStream = new MemoryStream())
        {
            //Create an archive and store the stream in memory.
            using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false)) {
                for (int i = 0; i < passes.Count; i++)
                {
                    // Create a .zip file containing the .pkpass files for the passes that are part of the bundle.
                    var zipEntry = zipArchive.CreateEntry("ALV-" + i.ToString() + ".pkpass");
    
                    //Get the stream of the attachment
                    using (var originalFileStream = new MemoryStream(passes.ElementAt(i)))
                    using (var zipEntryStream = zipEntry.Open()) 
                    {
                        //Copy the attachment stream to the zip entry stream
                        originalFileStream.CopyTo(zipEntryStream);
                    }
                }
            }
    
            // NOTE: Change the extension of the .zip file to .pkpasses. (NOT WORKING)
            // You can distribute a bundle of passes the same way you distribute a single pass. The MIME type for a bundle of passes is "application/vnd.apple.pkpasses".
            return new FileContentResult(compressedFileStream.ToArray(), "application/vnd.apple.pkpasses") { FileDownloadName = "ALV.pkpasses" };
        }
    }
}
