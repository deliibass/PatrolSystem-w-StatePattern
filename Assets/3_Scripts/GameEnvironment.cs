using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public sealed class GameEnvironment
{
    public List<GameObject> Checkpoints { get { return _checkpoints; } }
    private static GameEnvironment _instance;
    private List<GameObject> _checkpoints = new List<GameObject>();
    
    public static GameEnvironment Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameEnvironment();
                _instance.Checkpoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));

                _instance._checkpoints = _instance._checkpoints.OrderBy(wayPoint => wayPoint.name).ToList();
            }
            return _instance;
        }
    }
}
