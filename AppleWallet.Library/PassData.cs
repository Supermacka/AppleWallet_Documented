namespace AppleWallet.Library;

/// <summary>
/// Represents the overall data to create a pass
/// </summary>
public class PassData
{
    public PassImageData? ImageData { get; set; }
    public PassFieldData[]? PassFieldData { get; set; } 
}