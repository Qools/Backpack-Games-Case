using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [HideInInspector] public static GameController Instance;

    [Tooltip("All possible players (units) in the game.")]
    public GameObject[] players;

    public static int numNotFrozenExceptTagged = 4;
    public static int MAX_NUM_PLAYERS_EXCEPT_TAGGED = 4;

    public static GameObject lastFrozenCharacter = null;
    public static GameObject lastTaggedCharacter = null;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        int TaggedPlayer = Random.Range(1, players.Length);
        players[TaggedPlayer - 1].tag = PlayerPrefKeys.taggedPlayer;

    }

    void Update()
    {
        if (numNotFrozenExceptTagged == 0 && lastFrozenCharacter)
        {
            lastFrozenCharacter.tag = PlayerPrefKeys.taggedPlayer;
            lastFrozenCharacter.transform.position = new Vector3(0.0f, lastFrozenCharacter.transform.position.y, 5.0f);
            lastTaggedCharacter.transform.position = new Vector3(0.0f, lastTaggedCharacter.transform.position.y, -5.0f);
            lastFrozenCharacter.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            lastTaggedCharacter.tag = PlayerPrefKeys.notFrozen;
            lastTaggedCharacter.GetComponent<Seek>().target = null;
            numNotFrozenExceptTagged = 1;
            lastFrozenCharacter = null;
            lastTaggedCharacter = null;
        }
    }

    public GameObject[] GetPlayers()
    {
        return players;
    }
}
