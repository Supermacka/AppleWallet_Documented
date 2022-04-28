namespace AppleWallet.Library;

/// <summary>
/// Represents the ByteArray-data for images that are displayed in the pass.
/// </summary>

// TODO: Create struct
public class PassImageData
{
    public IconData? IconData { get; set; }
    public LogoData? LogoData { get; set; }
    public StripData? StripData { get; set; }
    public BackgroundData? BackgroundData { get; set; }
    public ThumbnailData? ThumbnailData { get; set; }
    public FooterData? FooterData { get; set; }
}

public class FooterData
{
    public string? Footer { get; set; } // NOTE: Only supported with type: Boarding-pass
    public string? Footer2X { get; set; } // NOTE: Only supported with type: Boarding-pass
    public string? Footer3X { get; set; } // NOTE: Only supported with type: Boarding-pass
}

public class ThumbnailData
{
    public string? Thumbnail { get; set; }
    public string? Thumbnail2X { get; set; }
    public string? Thumbnail3X { get; set; }
}

public class BackgroundData
{
    public string? Background { get; set; }
    public string? Background2X { get; set; }
    public string? Background3X { get; set; }
}

public class StripData
{
    public string? Strip { get; set; }
    public string? Strip2X { get; set; }
    public string? Strip3X { get; set; }
}

public class LogoData
{
    public string? Logo { get; set; }
    public string? Logo2X { get; set; }
    public string? Logo3X { get; set; }
}

public class IconData{
    public string? Icon { get; set; }
    public string? Icon2X { get; set; }
    public string? Icon3X { get; set; }
}