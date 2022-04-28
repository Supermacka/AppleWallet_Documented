using Passbook.Generator.Fields;

namespace AppleWallet.Library;

/// <summary>
/// Represents the specified information and design the pass will display.
/// </summary>
public class PassFieldData
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? LogoText { get; set; }
    public string? PassType { get; set; } // TODO: Artikeltyp
    
    public PassColor Colors { get; set; }

    public BarcodeData? BarcodeData { get; set; }
}

/// <summary>
/// Represents the colors used for the pass.
/// </summary>
public class PassColor
{
    public string BakgoundColor { set; get; } = "rgb(0,0,0)";
    public string LabelColor { set; get; } = "rgb(230,230,230)";
    public string ForegroundColor { set; get; } = "rgb(255,255,255)";
}

/// <summary>
/// Represents the data used to display a barcode for the pass.
/// </summary>
public class BarcodeData
{
    public BarcodeData()
    {
        DisplaySerialNumber = this.SerialNumber;
    }

    public string SerialNumber { get; set; } = Guid.NewGuid().ToString();
    public string? DisplaySerialNumber { get; set; }
}
