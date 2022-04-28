namespace AppleWallet.Api.Entities;

public record Device
{
    public Guid Id { get; init; }
    public string? DeviceLibraryIdentifier { get; set; }
    public string? PushToken { get; set; }
}