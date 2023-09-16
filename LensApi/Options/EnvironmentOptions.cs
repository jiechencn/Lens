namespace Me.JieChen.Lens.Api.Options;

class EnvironmentOptions
{
    public bool Debug { get; set; } = false;

    public bool LoadKeyVault { get; set; } = true;

    public string KeyVault { get; set; } = string.Empty;
}