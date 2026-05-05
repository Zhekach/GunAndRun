using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerRunner : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed = 6f;
    [SerializeField] private float _sideSpeed = 5f;
    [SerializeField] private float _xLimit = 3f;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(
            horizontal * _sideSpeed,
            0f,
            _forwardSpeed
        );

        _controller.Move(move * Time.deltaTime);

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -_xLimit, _xLimit);
        transform.position = position;
    }
}