using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private InputPlayer _input;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_characterController != null)
        {
            Vector3 playerSpeed = _input.Move;
            playerSpeed *= Time.deltaTime * _speed;

            if (_characterController.isGrounded)
            {
                _characterController.Move(playerSpeed + Physics.gravity);
            }
            else
            {
                _characterController.Move(_characterController.velocity + Physics.gravity * Time.deltaTime);
            }
        }
    }
}