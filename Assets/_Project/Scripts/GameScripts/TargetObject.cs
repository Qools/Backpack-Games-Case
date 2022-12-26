using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public bool isTarget = true;

    private void Start()
    {
        GameController.Instance.objects.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerPrefKeys.notFrozen))
        {
            EventSystem.CallObjectTaken(this);
        }
    }
}
