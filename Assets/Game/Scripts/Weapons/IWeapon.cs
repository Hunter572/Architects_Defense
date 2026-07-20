using Cysharp.Threading.Tasks;

public interface IWeapon
{
    float FireRate { get; }
    bool CanShoot { get; }
    void Fire();
}