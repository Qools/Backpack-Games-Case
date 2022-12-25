using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    const float POS_OFFSET = 0.3f;

    void OnTriggerEnter(Collider col)
    {
        if (gameObject.CompareTag(PlayerPrefKeys.topBoundary))
        {
            Vector3 oldPosition = col.gameObject.transform.position;
            float newZ = -oldPosition.z;
            col.gameObject.transform.position = new Vector3(oldPosition.x, oldPosition.y, newZ + POS_OFFSET);
        }
        else if (gameObject.CompareTag(PlayerPrefKeys.bottomBoundary))
        {
            Vector3 oldPosition = col.gameObject.transform.position;
            float newZ = -oldPosition.z;
            col.gameObject.transform.position = new Vector3(oldPosition.x, oldPosition.y, newZ - POS_OFFSET);
        }
        else if (gameObject.CompareTag(PlayerPrefKeys.rightBoundary))
        {
            Vector3 oldPosition = col.gameObject.transform.position;
            float newX = -oldPosition.x;
            col.gameObject.transform.position = new Vector3(newX + POS_OFFSET, oldPosition.y, oldPosition.z);
        }
        else if (gameObject.CompareTag(PlayerPrefKeys.leftBoundary))
        {

            Vector3 oldPosition = col.gameObject.transform.position;
            float newX = -oldPosition.x;
            col.gameObject.transform.position = new Vector3(newX - POS_OFFSET, oldPosition.y, oldPosition.z);
        }
    }
}
