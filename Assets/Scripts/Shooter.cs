using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ArmMover _armMover;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _projectileLoadPoint;

    private bool _isCharhed;

    public void Shoot()
    {
        if (_isCharhed)
        {
            _isCharhed = false;
            _armMover.ReadyToRelease -= LoadProjectile;
            _armMover.Release();
        }
    }

    public void Load()
    {
        _armMover.ReadyToRelease ??= LoadProjectile;
        _armMover.PullDown();
    }

    private void LoadProjectile()
    {
        if (_isCharhed == false)
        {
            Instantiate(_projectilePrefab, _projectileLoadPoint.position, _projectileLoadPoint.rotation);
            _isCharhed = true;
        }
    }
}
