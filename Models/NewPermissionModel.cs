namespace Vchd.Permission.Client.Models;

public class NewPermissionModel
{
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime FromAt { get; set; }
    public DateTime UntillAt { get; set; }
    public EPermissionGive PermissionGive { get; set; }
}
public enum EPermissionGive
{
    None = 0,
    Vchd = 1,
    VchdG=2,
    VchdZam=3,
    VchdKadr=4,
    VchdGlBux=5,
    VchdGlEko=6
}