using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{

    [Tooltip("The target to seek.")]
    public GameObject target;

    private GameObject[] players;

    public float speed = 2.0f;
    public bool hasTarget;

    private Rigidbody mRigidBody; 
    private GameController gController;

    Quaternion lookWhereYoureGoing;
    Vector3 goalFacing;
    public float rotationSpeedRads = 4.0f;

    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
        gController = GameObject.Find(PlayerPrefKeys.gameController).GetComponent<GameController>();
    }

    void Update()
    {
        SeekBehavior();
        AlignBehavior();
    }

    private void SeekBehavior()
    {
        if (tag == PlayerPrefKeys.taggedPlayer)
        {
            if (!target)
            {
                FindTarget();
            }
            target.tag = PlayerPrefKeys.target;
            mRigidBody.velocity = ((target.transform.position - transform.position).normalized * speed);
        }
        else if (tag == PlayerPrefKeys.notFrozen)
        {
            FindFrozenTarget();
            if (target)
            {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * speed);
            }
        }
    }

    private void FindTarget()
    {
        Vector3 OldDistanceToPlayer = Vector3.zero;
        Vector3 distanceToPlayer = Vector3.zero;

        foreach (GameObject player in gController.GetPlayers())
        {
            if (player != gameObject && !player.gameObject.CompareTag(PlayerPrefKeys.frozen))
            {
                if (OldDistanceToPlayer != Vector3.zero)
                {
                    distanceToPlayer = (player.transform.position - transform.position).normalized;
                    if (distanceToPlayer.magnitude < OldDistanceToPlayer.magnitude)
                    {
                        target = player;
                    }
                }
                else
                {
                    OldDistanceToPlayer = (player.transform.position - transform.position).normalized;
                    target = player;
                }
            }
        }
    }

    private void FindFrozenTarget()
    {
        Vector3 OldDistanceToPlayer = Vector3.zero;
        Vector3 distanceToPlayer = Vector3.zero;

        foreach (GameObject player in gController.GetPlayers())
        {
            if (player != gameObject && player.gameObject.CompareTag(PlayerPrefKeys.frozen))
            {
                if (OldDistanceToPlayer != Vector3.zero)
                {
                    distanceToPlayer = (player.transform.position - transform.position).normalized;
                    if (distanceToPlayer.magnitude < OldDistanceToPlayer.magnitude)
                    {
                        target = player;
                    }
                }
                else
                {
                    OldDistanceToPlayer = (player.transform.position - transform.position).normalized;
                    target = player;
                }
            }
        }
    }

    private void AlignBehavior()
    {

        if (target)
        {
            hasTarget = true;
        }
        else
        {
            hasTarget = false;
        }

        if (hasTarget)
        {
            goalFacing = (target.transform.position - transform.position).normalized;
            lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
        }
        else
        {

            goalFacing = mRigidBody.velocity.normalized;
            lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookWhereYoureGoing, rotationSpeedRads);
        }
    }
}

