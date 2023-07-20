namespace UITests.Configuration;

public class IosMacAutomationConfiguration
{
    public string PlatformVersion { get; set; } = null!;
    public string DeviceName { get; set; } = null!;
    public bool ShowXcodeLog { get; set; }
    public bool ShowIosLog { get; set; }
    public bool NoReset { get; set; }
    public bool FullReset { get; set; }
}