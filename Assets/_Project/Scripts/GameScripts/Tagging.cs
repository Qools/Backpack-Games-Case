using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Tagging : MonoBehaviour
{


    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == PlayerPrefKeys.taggedPlayer)
        {

            if (!CompareTag(PlayerPrefKeys.frozen) && !CompareTag(PlayerPrefKeys.taggedPlayer))
            {
                tag = PlayerPrefKeys.frozen;

                col.GetComponent<Seek>().target = null;
                

                GameController.numNotFrozenExceptTagged--;
                GameController.lastFrozenCharacter = gameObject;
                GameController.lastTaggedCharacter = col.gameObject;
            }
        }

        else if (col.gameObject.CompareTag(PlayerPrefKeys.notFrozen) && CompareTag(PlayerPrefKeys.frozen))
        {

            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            tag = PlayerPrefKeys.notFrozen;

            col.GetComponent<Seek>().target = null;

            GameController.numNotFrozenExceptTagged++;
            if (GameController.lastFrozenCharacter == gameObject)
            {
                GameController.lastFrozenCharacter = null;
            }
        }
    }
}
