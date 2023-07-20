namespace UITests.Configuration;

public enum SampleType
{
    Forms,
    Maui
}
public class AutomationConfiguration
{
    public SampleType SampleToRun { get; set; }
    public IosMacAutomationConfiguration IosMac { get; set; } = null!;
}