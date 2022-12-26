using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{ 

    [Tooltip("All possible players (units) in the game.")]
    public List<TargetObject> objects;
    public List<GameObject> players;


    public void Init()
    { 
        SetStatus(Status.ready);
    }


    private void OnEnable()
    {
        EventSystem.OnObjectTaken += CheckObjectNumber;
        EventSystem.OnNpcTag += CheckPlayerNumber;
        EventSystem.OnNewLevelLoad += OnNewLevelLoad;
    }

    private void OnDisable()
    {
        EventSystem.OnObjectTaken -= CheckObjectNumber;
        EventSystem.OnNpcTag -= CheckPlayerNumber;
        EventSystem.OnNewLevelLoad -= OnNewLevelLoad;
    }

    private void OnNewLevelLoad()
    {
        objects.Clear();
        objects = new List<TargetObject>();

        players.Clear();
        players = new List<GameObject>();
    }

    private void CheckObjectNumber(TargetObject targetObject)
    {
        objects.Remove(targetObject);
        Destroy(targetObject.gameObject);

        if (objects.Count == 0)
        {
            EventSystem.CallGameOver(GameResult.Lose);
        }
    }

    private void CheckPlayerNumber(Tagging tagged)
    {
        players.Remove(tagged.gameObject);

        if (players.Count == 0)
        {
            EventSystem.CallGameOver(GameResult.Win);
        }
    }

    public List<TargetObject> GetObjects()
    {
        return objects;
    }
}
