namespace Vchd.Permission.Client.Models;

public class PermissionModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime FromAt { get; set; }
    public DateTime UntillAt { get; set; }
    public EPermissionGive PermissionGive { get; set; }
    public EPermissionStatus Status { get; set; }
}
public enum EPermissionStatus
{
    Jarayonda = 0,
    Tasdiqlandi= 1,
    Tasdiqlanmadi = 2,
    Korilmadi = 3
}