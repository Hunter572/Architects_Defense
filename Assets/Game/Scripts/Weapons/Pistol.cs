using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Pool;
using DG.Tweening;

public class Pistol : MonoBehaviour, IWeapon
{
    [Header("Weapon Stats")]
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("Visual Juice")]
    [SerializeField] private Transform visualTransform; 

    private bool _canShoot = true;
    private IObjectPool<Projectile> _projectilePool;


    public float FireRate => fireRate;
    public bool CanShoot => _canShoot;

    private void Awake()
    {
        _projectilePool = new ObjectPool<Projectile>(
            createFunc: CreateProjectile,
            actionOnGet: OnGetProjectile,
            actionOnRelease: OnReleaseProjectile,
            actionOnDestroy: OnDestroyProjectile,
            collectionCheck: true,
            defaultCapacity: 20,
            maxSize: 100
        );
    }

    public void Fire()
    {
        if (_canShoot)
        {
            ShootAsync().Forget();
        }
    }

    private async UniTaskVoid ShootAsync()
    {
        _canShoot = false;

        Projectile bullet = _projectilePool.Get();
        bullet.Initialize(firePoint.position, firePoint.rotation, ReleaseProjectile);

        if (visualTransform != null)
        {
            visualTransform.DOComplete(); 
            visualTransform.DOLocalMoveZ(-0.15f, 0.05f).OnComplete(() =>
            {
                visualTransform.DOLocalMoveZ(0f, 0.1f);
            });
        }

        await UniTask.Delay(System.TimeSpan.FromSeconds(fireRate), cancellationToken: this.GetCancellationTokenOnDestroy());

        _canShoot = true;
    }


    private Projectile CreateProjectile()
    {
        return Instantiate(projectilePrefab);
    }

    private void OnGetProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    private void OnReleaseProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    private void ReleaseProjectile(Projectile projectile)
    {
        _projectilePool.Release(projectile);
    }
}