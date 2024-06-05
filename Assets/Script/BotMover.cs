using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BotMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stopDistance = 2f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private float groundCheckRadius = 0.8f;

    private Rigidbody _rigidbody;
    private bool _isGrounded;

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
        if (_isGrounded)
        {
            Vector3 direction = _player.transform.position - transform.position;
            direction.y = 0;

            if (direction.magnitude > _stopDistance)
            {
                Vector3 move = direction.normalized * _moveSpeed * Time.fixedDeltaTime;
                _rigidbody.MovePosition(_rigidbody.position + move);
            }
        }
    }

    private void SearchGroundUnderFeet()
    {
        if (Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out RaycastHit hit, groundCheckDistance, _groundMask))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
}