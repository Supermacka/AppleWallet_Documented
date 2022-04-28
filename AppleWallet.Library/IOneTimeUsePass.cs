namespace AppleWallet.Library;

public interface IOneTimeUsePass
{
    public byte[] Create(string companyName, PassImageData imagePaths, PassFieldData passFieldData);
}