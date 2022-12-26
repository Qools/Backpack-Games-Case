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
    private float currentSpeed;
    public bool hasTarget;

    private Rigidbody mRigidBody; 
    private GameController gController;

    Quaternion lookWhereYoureGoing;
    Vector3 goalFacing;
    public float rotationSpeedRads = 4.0f;

    void Start()
    {
        mRigidBody = GetComponent<Rigidbody>();
        gController = GameController.Instance;
        gController.players.Add(this.gameObject);

        currentSpeed = 0f;
    }

    void Update()
    {
        if (!GameManager.Instance.isGameStarted)
            return;

        SeekBehavior();
        AlignBehavior();
    }

    private void OnEnable()
    {
        EventSystem.OnStartGame += OnGameStart;
        EventSystem.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventSystem.OnStartGame -= OnGameStart;
        EventSystem.OnGameOver -= OnGameOver;
    }

    private void OnGameStart()
    {
        currentSpeed = speed;
    }

    private void OnGameOver(GameResult gameResult)
    {
        currentSpeed = 0;
        mRigidBody.velocity = Vector3.zero;
    }

    private void SeekBehavior()
    {
        if (CompareTag(PlayerPrefKeys.taggedPlayer))
        {
            mRigidBody.velocity = Vector3.zero;
        }
        else if (CompareTag(PlayerPrefKeys.notFrozen))
        {
            FindFrozenTarget();
            if (target)
            {
                mRigidBody.velocity = ((target.transform.position - transform.position).normalized * currentSpeed);
            }
        }
    }

    private void FindFrozenTarget()
    {
        Vector3 OldDistanceToPlayer = Vector3.zero;
        Vector3 distanceToPlayer = Vector3.zero;

        foreach (TargetObject _targetObject in gController.GetObjects())
        {
            if (hasTarget && !_targetObject.isTarget)
            {
                return;
            }

            if (OldDistanceToPlayer != Vector3.zero)
            {
                distanceToPlayer = (_targetObject.transform.position - transform.position).normalized;
                if (distanceToPlayer.magnitude < OldDistanceToPlayer.magnitude)
                {
                    target = _targetObject.gameObject;
                    _targetObject.isTarget = false;
                }
            }
            else
            {
                OldDistanceToPlayer = (_targetObject.transform.position - transform.position).normalized;
                target = _targetObject.gameObject;
                _targetObject.isTarget = false;
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

