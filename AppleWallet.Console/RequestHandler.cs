using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AppleWallet.Library;
using Newtonsoft.Json;

namespace AppleWallet.Console;

public static class RequestHandler
{    
    /// <summary>
    /// Creates a connection to the .pkpass API and returns its POST endpoint
    /// </summary>
    /// <returns>byte[]</returns>
    public static async Task<byte[]> GetPassAsync()
    {
        HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.apple.pkpass"));

        var LogoArray = System.IO.File.ReadAllBytes(@"/Users/colinfarkas/RiderProjects/EntryEvent.MobileTicket.AppleWallet/AppleWallet.Library/Images/Tosselilla/icon.png");
        string LogoArrayResult = System.Text.Encoding.Latin1.GetString(LogoArray); // IMPORTANT: Input must be Latin1

        var bodyData = new PassData() {
            ImageData = new PassImageData(){
                LogoData = new LogoData(){
                    Logo = LogoArrayResult,
                    Logo2X = LogoArrayResult,
                },
                StripData = new StripData {
                    Strip = LogoArrayResult,
                    Strip2X = LogoArrayResult,
                },
                BackgroundData = new BackgroundData(){
                    Background = "string",
                    Background2X = "string",
                },
                IconData = new IconData(){
                    Icon = "string",
                    Icon2X = "string",
                },
                ThumbnailData = new ThumbnailData(){
                    Thumbnail = "string",
                    Thumbnail2X = "string",
                },
                FooterData = new FooterData(){
                    Footer = "string",
                    Footer2X = "string",
                }
            },
            PassFieldData = new PassFieldData[]{
                new(){
                    BarcodeData = new BarcodeData(){
                        SerialNumber = "BBB222BBB",
                        DisplaySerialNumber = "BBB222BBB"
                    },
                    Description = "Entrébiljett till Astrid Lindgrens Värld",
                    LogoText = "ALV",
                    Title = "Astrid Lindgrens Värld",
                    PassType = "Dagsbiljett",
                    Colors = new PassColor(){
                        BakgoundColor = "rgb(250,216,88)",
                        LabelColor = "rgb(156,129,27)",
                        ForegroundColor = "rgb(83,67,6)",
                    }
                },
                new(){
                    BarcodeData = new BarcodeData(){
                        SerialNumber = "AAA111AAA",
                        DisplaySerialNumber = "AAA111AAA"
                    },
                    Description = "Entrébiljett till Astrid Lindgrens Värld",
                    LogoText = "ALV",
                    Title = "Astrid Lindgrens Värld",
                    PassType = "Dagsbiljett",
                    Colors = new PassColor(){
                        BakgoundColor = "rgb(250,216,88)",
                        LabelColor = "rgb(156,129,27)",
                        ForegroundColor = "rgb(255,255,255)",
                    }
                }
            }
        };
        
        HttpResponseMessage response = await client.PostAsJsonAsync(
        "https://localhost:7161/Passes/ALV", bodyData);
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadAsByteArrayAsync();

        return result;
    }
}