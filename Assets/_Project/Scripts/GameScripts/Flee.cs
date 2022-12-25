using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    [Tooltip("The target to flee from.")]
    public GameObject target;

    public float speed = 1.0f;

    private float fleeDistanceThreshold = 1.0f;

    private float angleThreshold = 1.0f;

    private Rigidbody mRigidBody;

    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        KinematicFlee();

    }

    private void KinematicFlee()
    {
        target = GameObject.FindGameObjectWithTag(PlayerPrefKeys.taggedPlayer);
        Vector3 fleeDir = transform.position - target.transform.position;
        if (fleeDir.magnitude < fleeDistanceThreshold)
        {
            transform.Translate(fleeDir.normalized * speed * Time.deltaTime, Space.World);
        }
        else 
        {
            if (Vector3.Angle(transform.forward, fleeDir) <= angleThreshold)
            {
                transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            }
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(fleeDir), 180.0f * Time.deltaTime);
    }
}