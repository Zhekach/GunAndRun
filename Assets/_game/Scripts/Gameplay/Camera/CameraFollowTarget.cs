using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private bool _findPlayerOnStart = true;

    [Header("Follow Axes")]
    [SerializeField] private bool _followX;
    [SerializeField] private bool _followY;
    [SerializeField] private bool _followZ = true;

    private Vector3 _offset;

    private void Awake()
    {
        TryFindTarget();
        UpdateOffset();
    }

    private void LateUpdate()
    {
        if (_target == null)
        {
            TryFindTarget();
            return;
        }

        Vector3 position = transform.position;
        Vector3 targetPosition = _target.position + _offset;

        if (_followX)
            position.x = targetPosition.x;

        if (_followY)
            position.y = targetPosition.y;

        if (_followZ)
            position.z = targetPosition.z;

        transform.position = position;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        UpdateOffset();
    }

    private void TryFindTarget()
    {
        if (_target != null || _findPlayerOnStart == false)
            return;

        PlayerRunner player = FindObjectOfType<PlayerRunner>();

        if (player != null)
            _target = player.transform;
    }

    private void UpdateOffset()
    {
        _offset = _target != null ? transform.position - _target.position : Vector3.zero;
    }
}
