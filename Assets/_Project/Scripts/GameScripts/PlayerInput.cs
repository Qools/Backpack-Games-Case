using UnityEngine;
using System.Collections;

// accepts input and animates the player
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;

    public float moveSpeed, rotateSpeed;
    Vector3 direction;

    bool isMouseDown;

    void Update()
    {
        if (!GameManager.Instance.isGameStarted)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;

            _rigidbody.isKinematic = false;

            _animator.ResetTrigger(PlayerPrefKeys.stopTrigger);
            _animator.SetTrigger(PlayerPrefKeys.runTrigger);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            Stop();
        }
    }

    private void FixedUpdate()
    {

        if (!isMouseDown) return;

        direction = new Vector3(Joystick.current.GetAxis("Horizontal"), 0f, Joystick.current.GetAxis("Vertical"));

        Rotate();

        MoveForward();
    }

    private void OnEnable()
    {
        EventSystem.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventSystem.OnGameOver -= OnGameOver;
    }

    private void OnGameOver(GameResult gameResult)
    {
        Stop();
    }

    public void Rotate()
    {
        character.rotation = Quaternion.RotateTowards(
            character.rotation,
            Quaternion.LookRotation(direction),
            rotateSpeed * Time.fixedDeltaTime);
    }

    public void MoveForward()
    {
        Vector3 newVelocity = Joystick.current.knobDistance * moveSpeed * Time.fixedDeltaTime * direction;
        newVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = newVelocity;
    }

    public void Stop()
    {
        _animator.ResetTrigger(PlayerPrefKeys.runTrigger);
        _animator.SetTrigger(PlayerPrefKeys.stopTrigger);

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.isKinematic = true;
    }
}
