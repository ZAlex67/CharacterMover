using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BotMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stopDistance = 2f;
    [SerializeField] private float _stepHeight = 0.5f;
    [SerializeField] private LayerMask _groundMask;

    private Rigidbody _rigidbody;
    private bool _isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        CheckGround();

        if (!_isGrounded)
        {
            _rigidbody.velocity += Physics.gravity * Time.fixedDeltaTime;
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToTarget <= _stopDistance)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }

        Vector3 direction = (_player.transform.position - transform.position).normalized;
        float line = 0.5f;
        float radius = 1f;
        float stair = 0.1f;

        Vector3 cast = transform.position + Vector3.up * stair;
        if (Physics.SphereCast(cast, radius, direction, out RaycastHit hit, line))
        {
            Vector3 stepCheck = transform.position + direction * line;
            if (!Physics.Raycast(stepCheck + Vector3.up * (_stepHeight + stair), Vector3.down, out RaycastHit stepHit, _stepHeight, _groundMask))
            {
                _rigidbody.velocity = direction + Vector3.up * _stepHeight * Time.fixedDeltaTime - Physics.gravity;
            }
        }

        Vector3 move = direction * _moveSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + move);
    }

    private void CheckGround()
    {
        float distance = 1.1f;
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, distance, _groundMask);
    }
}