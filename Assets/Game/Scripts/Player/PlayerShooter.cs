using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private IWeapon _activeWeapon;

    private void Awake()
    {
        
        _activeWeapon = GetComponentInChildren<IWeapon>();

        if (_activeWeapon == null)
        {
            Debug.LogWarning("IWeapon not found");
        }
    }

    public void TryShoot()
    {
        _activeWeapon?.Fire();
    }
}