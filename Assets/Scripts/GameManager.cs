using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public Vector3 playerSpawnPosition;
    public GameObject player;
    public string playerName;
    [Tooltip("GameObject holding high score Texts")] public GameObject highScoresParent;

    public int score;// { get; private set;}

    private static GameObjectPool playerPool;
    private List<PlayerGeneric> playerInstances;

    public delegate void FlipperAction();
    /// <summary>The trigger left flippers.</summary>
    public static FlipperAction TriggerLeftFlipper;
    /// <summary>The trigger right flippers.</summary>
    public static FlipperAction TriggerRightFlipper;
    /// <summary>The release left flippers.</summary>
    public static FlipperAction ReleaseLeftFlipper;
    /// <summary>The release right flippers.</summary>
    public static FlipperAction ReleaseRightFlipper;

    public List<Flipper> leftFlippers = new List<Flipper>();
    public List<Flipper> rightFlippers = new List<Flipper>();
    public AudioSource flipperAudio;
    public Text timeText;
    public bool playing;
    public float releaseTime;

    [Tooltip("The last label is the first player")]
    public Text[] highScoreNameTexts;
    [Tooltip("The last label is the first player")]
    public Text[] highScoreTexts;
    public Vector3 spawnPosition;

    private static string highScoreName = "Name";
    private static string[] highScoreKeys = {
        "PlayerOne", 
        "PlayerTwo", 
        "PlayerThree", 
        "PlayerFour", 
        "PlayerFive"
    };
    //TODO:Setup player prefs, high scores

    public void Play(bool playing = true)
    {
        playing = playing;
        player.SetActive(playing);
        Debug.Log(!playing);
        highScoresParent.SetActive(!playing);

        if(playing)
        {
            player.transform.position = spawnPosition;
        }
    }
    static private void CreateDefaultHighScores()
    {
        int[] playerScore = { 25000, 50000, 75000, 100000, 200000 };
        string[] playerName = { "bob", "garry", "ted", "tom", "harry" };
        ScoreName[] highScores = new ScoreName[5];

        for(int i = 0; i < 5; i++)
        {
            highScores[i].score = playerScore[i];
            highScores[i].name = playerName[i];
        }

        SetHighScores(highScores);
    }
    private ScoreName[] GetHighScores()
    {
        int[] scores = new int[5];
        string[] names = new string[5];
        ScoreName[] scoreNames = new ScoreName[5];

        if(!PlayerPrefs.HasKey(highScoreKeys[0]))
        {
            CreateDefaultHighScores();
        }

        for(int i = 0; i < 5; i++)
        {
            scores[i] = PlayerPrefs.GetInt(highScoreKeys[i]);
            names[i] = PlayerPrefs.GetString(highScoreKeys[i] + highScoreName);
            string playerName = GetPlayerName();
        
            highScoreNameTexts[i].text = names[i].ToString();
            highScoreTexts[i].text = scores[i].ToString();
        
            scoreNames[i] = new ScoreName(scores[i], names[i]);
        }

        return scoreNames;
    }

    private static void SetHighScores(ScoreName[] highScores)
    {
        for(int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt(highScoreKeys[i], highScores[i].score);
            PlayerPrefs.SetString(highScoreKeys[i] + highScoreName, highScores[i].name);
        }
    }

    private string GetPlayerName()
    {
        return playerName;
        //TODO:Player enter their name; initials, side scroller keyboard
    }

    [System.Serializable]
    public struct ScoreName
    {
        public int score;
        public string name;

        public ScoreName(int score, string name)
        {
            this.score = score;
            this.name = name;
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        ScoreName[] highScores = GetHighScores();
        int place = 0;
        for(int i = 0; i < 5; i++)
        {
            Debug.Log("Processing " + highScores[i].name + " : " + highScores[i].score);
            if(score > highScores[i].score)
            {
                Debug.Log("Place");
                place++;
            }
            else
            {
                i = 5;
            }
        }

        if(place > 0)
        {
            HighScore(place, ref highScores);
        }

        DisplayHighScores(highScores);
        Play(false);
    }

    private void DisplayHighScores(ScoreName[] highScores)
    {
        if(highScores.Length != 5)
        {
            Debug.Log("highscores length is " + highScores.Length);
            return;
        }
        else
        {
            for(int i = 0; i < 5;i++)
            {
                highScoreNameTexts[i].text = highScores[i].name;
                highScoreTexts[i].text = highScores[i].score.ToString();
            }
        }
    }

    private void HighScore(int place, ref ScoreName[] highScores)
    {
        string playerName = GetPlayerName();
        // For all scores leading up to the new place
        for(int i = 0; i < place - 1; i++)
        {
            highScores[i] = highScores[i + 1];
        }

        highScores[place - 1] = new ScoreName(score, playerName);

        SetHighScores(highScores);
    }
    //TODO:Recover lost materials
    //TODO:General object pooling
    //TODO:Player object pooling
    //TODO:Points
    //TODO:Clean GameManager
    //TODO:Identify memory leaks
    private void Start()
    {
        playing = false;
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
        #region Display High Scores
        DisplayHighScores(GetHighScores());
        #endregion
        #if UNITY_EDITOR
        /*for(int i = 0; i < 5; i++)
        {
            Debug.Log(PlayerPrefs.GetString(highScoreKeys[i] + highScoreName) + " : " + PlayerPrefs.GetInt(highScoreKeys[i]).ToString());
        }*/
        #endif
    }

    public void AddLeftFlipper(Flipper flipper)
    {
        leftFlippers.Add(flipper);
    }

    public void AddRightFlipper(Flipper flipper)
    {
        rightFlippers.Add(flipper);
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        scoreTextBox.text = "Score: " + score;
    }

    public void TriggerRight()
    {
        foreach(Flipper flipper in rightFlippers)
        {
            flipper.Flip();
        }
        flipperAudio.Play();
    }

    public void TriggerLeft()
    {
        foreach(Flipper flipper in leftFlippers)
        {
            flipper.Flip();
        }
        flipperAudio.Play();
    }

    public void ReleaseRight()
    {
        foreach(Flipper flipper in rightFlippers)
        {
            flipper.Release();
        }
    }

    public void ReleaseLeft()
    {
        foreach(Flipper flipper in leftFlippers)
        {
            flipper.Release();
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RightRelease()
    {
        ReleaseRightFlipper();
    }

    #region SpawnPlayer
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
        SpawnPlayer(playerSpawnPosition, rotation, count, ignoreMaximum);
    }

    public void SpawnPlayer()
    {
        SpawnPlayer(playerSpawnPosition, Quaternion.identity);
    }
    #endregion
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

#if UNITY_EDITOR
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
#endif