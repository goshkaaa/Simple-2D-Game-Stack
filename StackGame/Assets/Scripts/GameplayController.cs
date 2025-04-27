using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : Sounds

{
    [Header("Sound Settings")]
    public AudioClip boxCollisionSound;

    [Header("Score Settings")]
    public int score;
    [SerializeField] Text scoreText;

    [Header("Game Settings")]
    public static GameplayController instance;
    public BoxSpawner box_Spawner;

    [HideInInspector]
    public BoxScript currentBox;

    public CameraFollow cameraScript;
    private int moveCount;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        box_Spawner.SpawnBox();
    }

    void DetectInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentBox.DropBox();
    
        }
    }

    public void SpawnNewBox()
    {
        score++;
        Invoke("NewBox", 0.5f);
    }

    void NewBox()
    {
        box_Spawner.SpawnBox();
    }

    public void MoveCamera()
    {
        moveCount++;

        if (moveCount == 3)
        {
            moveCount = 0;

            // Поднимаем камеру
            cameraScript.targetPos.y += 1f;
            box_Spawner.spawnY += 1f;
        }
    }

    
    public void RestartGame()
    {
        Debug.Log("RestartGame() in GameplayController called");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    void Update()
    {
        DetectInput();
        scoreText.text = score.ToString();
    }

}
