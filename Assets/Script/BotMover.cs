using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BotMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stopDistance = 2f;
    [SerializeField] private float _botHeight;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _groundDrag;

    private Rigidbody _rigidbody;
    private bool _isGround;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SearchGroundUnderFeet();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float distanceToTarget = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToTarget <= _stopDistance)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }

        Vector3 currentVelocity = _rigidbody.velocity;
        Vector3 targetVelocity = (_player.transform.position - transform.position).normalized;
        targetVelocity *= _moveSpeed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = targetVelocity - currentVelocity;
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        Vector3.ClampMagnitude(velocityChange, _maxForce);

        _rigidbody.AddForce(velocityChange, ForceMode.Impulse);
    }

    private void SearchGroundUnderFeet()
    {
        float feetLength = 0.5f;
        float ground = 0.2f;

        _isGround = Physics.Raycast(transform.position, Vector3.down, _botHeight * feetLength + ground, _groundMask);

        if (_isGround)
        {
            _rigidbody.drag = _groundDrag;
        }
        else
        {
            _rigidbody.drag = 0;
        }
    }
}