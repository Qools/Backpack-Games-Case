using UnityEngine;
using System.Collections;

// accepts input and animates the player
public class PlayerInput : MonoBehaviour
{
	Animator _animator;

	public float m_maxVelocity;
	public float m_acceleration;
	public float m_deccelerationMultiplier;
	public float rotationDegreesPerSecond = 360;

	float _velocity;
	Vector2 _input = Vector2.zero;


    void Start ()
    {
		_animator = GetComponent<Animator> ();
    }

    void Update ()
    {
        float horizontal = Input.GetAxis (PlayerPrefKeys.horizontal);
        float vertical = Input.GetAxis (PlayerPrefKeys.vertical);
        
		_input.x = horizontal;
		_input.y = vertical;
		float inputMag = _input.magnitude;


		if (!Mathf.Approximately (vertical, 0.0f) || !Mathf.Approximately (horizontal, 0.0f)) {
			Vector3 direction = new Vector3 (horizontal, 0.0f, vertical);
			direction = Vector3.ClampMagnitude (direction, 1.0f);

			if (_velocity < m_maxVelocity) {
				_velocity += m_acceleration * Time.deltaTime;
				if (_velocity > m_maxVelocity)
					_velocity = m_maxVelocity;
			}

			transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (direction), rotationDegreesPerSecond * Time.deltaTime);


		} else if (_velocity > 0){
			
			_velocity -= m_acceleration * m_deccelerationMultiplier * Time.deltaTime;
			if (_velocity < 0)
				_velocity = 0;
		}

		transform.position += transform.forward * Time.deltaTime * _velocity;

		_animator.SetFloat ("Blend", _velocity / m_maxVelocity);

    }
}
