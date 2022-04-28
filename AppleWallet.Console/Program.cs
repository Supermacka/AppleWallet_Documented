// See https://aka.ms/new-console-template for more information

using AppleWallet.Console;

// Return API GET-call and send mail with ticket attachment
byte[]? pass = (byte[])await RequestHandler.GetPassAsync();
EmailHandler.Send(pass);
System.Console.WriteLine("Email was sent!");
