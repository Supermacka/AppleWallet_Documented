using System.Security.Cryptography.X509Certificates;
using System.Text;
using Passbook.Generator;
using Passbook.Generator.Fields;

namespace AppleWallet.Library;

public class OneTimeUsePass : IOneTimeUsePass
{
    private static string CurrentPathImages { get; set; } =
        @"/Users/colinfarkas/RiderProjects/EntryEvent.MobileTicket.AppleWallet/AppleWallet.Library/Images/Tosselilla/";

    public byte[] Create(string companyName, PassImageData imageData, PassFieldData fieldData)
    {
        PassGenerator generator = new PassGenerator();
    
        PassGeneratorRequest request = new PassGeneratorRequest();
        
        // Toplevel Keys
        request.PassTypeIdentifier = TopLevelKeys.PassTypeIdentifier; 
        request.TeamIdentifier = TopLevelKeys.TeamIdentifier;
        
        // Updatable (makes the pass updatable)
        // request.AuthenticationToken = "vxwxd7J8AlNNFPS8k0a0FfUFtq0ewzFdc"; // IMPORTANT
        // request.WebServiceUrl = "https://localhost:7161/Passes/";

        request.SerialNumber = fieldData.BarcodeData!.SerialNumber;
        request.Description = fieldData.Description;
        request.OrganizationName = companyName;
        request.LogoText = fieldData.LogoText; // TODO: PROBLEM! Logo takes too much space

        // Design
        request.BackgroundColor = fieldData.Colors.BakgoundColor;
        request.LabelColor = fieldData.Colors.LabelColor;
        request.ForegroundColor = fieldData.Colors.ForegroundColor;

        // Certification
        string certPassword = "EntryEventPassCert12";
        request.AppleWWDRCACertificate = new X509Certificate2(@"/Users/colinfarkas/Desktop/AppleWWDRCA.cer");
        request.PassbookCertificate = new X509Certificate2(@"/Users/colinfarkas/Desktop/Certificate.p12", certPassword);

        // Icon *REQUIRED (shows on notification)
        request.Images.Add(PassbookImage.Icon, File.ReadAllBytes(CurrentPathImages + @"icon.png")); // Take bytearray parameteer
        request.Images.Add(PassbookImage.Icon2X, File.ReadAllBytes(CurrentPathImages + @"icon@2x.png"));
        
        var logoData = imageData.LogoData!.Logo;
        request.Images.Add(PassbookImage.Logo, Encoding.Latin1.GetBytes(imageData.LogoData.Logo));
        request.Images.Add(PassbookImage.Logo2X, Encoding.Latin1.GetBytes(imageData.LogoData.Logo2X));
        
        // Strip (banner)
        request.Images.Add(PassbookImage.Strip, Encoding.Latin1.GetBytes(imageData.StripData.Strip));
        request.Images.Add(PassbookImage.Strip2X, Encoding.Latin1.GetBytes(imageData.StripData.Strip2X));
        
        request.Style = PassStyle.EventTicket;

        request.AddSecondaryField(new StandardField("pass-name", "", fieldData.Title));
        
        request.AddAuxiliaryField(new StandardField("pass-type", "Biljettyp", fieldData.PassType));
        request.AddAuxiliaryField(new StandardField("entry-date", "10:00-17:00", "04/08-22"));
        // request.AddAuxiliaryField(new StandardField("pass-owner-name", "Ägare", "Colin Farkas"));

        // Doc https://developer.apple.com/library/archive/documentation/UserExperience/Reference/PassKit_Bundle/Chapters/LowerLevel.html#//apple_ref/doc/uid/TP40012026-CH3-SW3
        request.AddBarcode(type: BarcodeType.PKBarcodeFormatQR, message: $"{fieldData.BarcodeData.SerialNumber}", encoding: "ISO-8859-1", alternateText: $"{fieldData.BarcodeData.DisplaySerialNumber}");

        byte[] generatedPass = generator.Generate(request);
        return generatedPass;
    }
}