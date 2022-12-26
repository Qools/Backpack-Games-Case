using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Tagging : MonoBehaviour
{
    public bool isTagged = false;

    [SerializeField] private GameObject tagEffect;

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.CompareTag(PlayerPrefKeys.taggedPlayer))
        {

            if (!CompareTag(PlayerPrefKeys.frozen) && !CompareTag(PlayerPrefKeys.taggedPlayer))
            {
                tag = PlayerPrefKeys.frozen;

                tagEffect.SetActive(true);

                EventSystem.CallTagNpc(this);
            }
        }
    }
}
