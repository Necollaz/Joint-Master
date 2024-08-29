using System.Collections;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    [SerializeField] private Projectile _prefab;
    [SerializeField] private Transform _spoon;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private Transform _anchorStart;
    [SerializeField] private Transform _anchorEnd;
    [SerializeField] private float _springForce = 100f;
    [SerializeField] private float _delay = 1.5f;

    private SpringJoint _springJoint;
    private Projectile _currentProjectile;
    private bool _isLoaded = true;

    private void Awake()
    {
        _springJoint = _spoon.GetComponent<SpringJoint>();
    }

    private void Start()
    {
        _springJoint.connectedAnchor = _anchorStart.position;
        _springJoint.spring = _springForce;
        _springJoint.damper = 50.0f;
        _springJoint.autoConfigureConnectedAnchor = false;
    }

    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isLoaded)
        {
            Fire();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Fire()
    {
        if (_isLoaded && _currentProjectile != null)
        {
            _springJoint.connectedAnchor = _anchorEnd.position;
            _isLoaded = false;
        }
    }

    private void Reload()
    {
        _springJoint.connectedAnchor = _anchorStart.position;
        StartCoroutine(SpawnProjectile());
    }

    private IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(_delay);
        _currentProjectile = Instantiate(_prefab, _projectileSpawnPoint.position, Quaternion.identity);
        _isLoaded = true;
    }
}