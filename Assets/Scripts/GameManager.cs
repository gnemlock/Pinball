using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using Utility = GameManagerUtility;
using UnityEditor;
#endif

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;

    //TODO: We could turn the playerPrefab into an array, and store multiple variations of the player ball, to spawn "special balls".
    //TODO: If we make a playerPrefab array, it might be useful to make a dictionary, to identify balls by name rather than index.
    public PlayerGeneric playerPrefab;
    public int playerMaximum = 10;
    public Text scoreTextBox;

    public int score { get; private set;}

    private static GameObjectPool playerPool;
    private List<PlayerGeneric> playerInstances;

    public delegate void FlipperAction();
    /// <summary>The trigger left flippers.</summary>
    public FlipperAction TriggerLeftFlipper;
    /// <summary>The trigger right flippers.</summary>
    public FlipperAction TriggerRightFlipper;
    /// <summary>The release left flippers.</summary>
    public FlipperAction ReleaseLeftFlipper;
    /// <summary>The release right flippers.</summary>
    public FlipperAction ReleaseRightFlipper;

    //TODO:General object pooling
    //TODO:Player object pooling
    //TODO:Points

    private void Start()
    {
        #region Setup Singleton Functionality
        if(instance == null)    
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);

            #if UNITY_EDITOR
            Utility.MultipleGameManagersFound(this);
            #endif
        }
        #endregion
        #region Setup Player Pool
        playerInstances = new List<PlayerGeneric>();
        playerPool = new GameObjectPool(playerPrefab);
        #endregion
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreTextBox.text = "Score: " + score;
    }

    public void SpawnPlayer(Vector3 position, Quaternion rotation, int count = 1, 
        bool ignoreMaximum = false)
    {
        if(count > 0 && (ignoreMaximum || playerInstances.Count < playerMaximum))
        {
            for(int i = 0; i < count; i++)
            {
                PlayerGeneric newPlayer = playerPool.GetObject() as PlayerGeneric;

                newPlayer.transform.position = position;
                newPlayer.transform.rotation = rotation;
            }
        }
    }

    public void SpawnPlayer(Vector3 position, int count = 1, bool ignoreMaximum = false)
    {
        SpawnPlayer(position, Quaternion.identity, count, ignoreMaximum);
    }

    public void SpawnPlayer(Quaternion rotation, int count = 1, bool ignoreMaximum = false)
    {
        SpawnPlayer(Vector3.zero, rotation, count, ignoreMaximum);
    }

    public void SpawnPlayer()
    {
        SpawnPlayer(Vector3.zero, Quaternion.identity);
    }
}

public class GameManagerUtility
{
    private const string multipleGameManagersFound = "Found existing GameManager class. "
        + "Destroying new instance to retain Singleton structure.";

    public static void MultipleGameManagersFound(GameManager newInstance)
    {
        Debug.Log(multipleGameManagersFound, newInstance);
    }
}

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameManager gameManager = target as GameManager;

        DrawDefaultInspector();

        if(GUILayout.Button("Spawn Player"))
        {
            gameManager.SpawnPlayer();
        }
    }
}
