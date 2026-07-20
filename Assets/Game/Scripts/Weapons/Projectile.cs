using UnityEngine;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 2f;

    private Rigidbody _rb;
    private System.Action<Projectile> _onReleaseToPool;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 position, Quaternion rotation, System.Action<Projectile> onReleaseToPool)
    {
        transform.position = position;
        transform.rotation = rotation;
        _onReleaseToPool = onReleaseToPool;

        _rb.linearVelocity = transform.forward * speed;

        ReleaseAfterLifetime().Forget();
    }

    private async UniTaskVoid ReleaseAfterLifetime()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(lifeTime), cancellationToken: this.GetCancellationTokenOnDestroy());

        if (gameObject.activeSelf)
        {
            _onReleaseToPool?.Invoke(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _onReleaseToPool?.Invoke(this);
    }
}