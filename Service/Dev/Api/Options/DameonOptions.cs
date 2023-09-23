namespace Me.JieChen.Lens.Api.Options;

class DameonOptions
{
    public ActivityAuditOptions ActivityAudit { get; set; } = new ActivityAuditOptions();
}

class ActivityAuditOptions
{
    // secondly
    public uint RealTimeIntervalInSecond { get; set; } = 1;

    // hourly
    public uint HistoricalIntervalInSecond { get; set; } = 3600;
}