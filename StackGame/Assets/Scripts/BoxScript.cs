using UnityEngine;

public class BoxScript : MonoBehaviour
{


    [SerializeField] private float minX = -2.2f;
    [SerializeField] private float maxX = 2.2f;
    [SerializeField] private float moveSpeed = 2f;


    private bool canMove;
    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    private Rigidbody2D myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.gravityScale = 0f;
    }

    private void Start()
    {
        canMove = true;

        if (Random.Range(0, 2) > 0)
        {
            moveSpeed *= -1f;
        }

        if (GameplayController.instance != null)
        {
            GameplayController.instance.currentBox = this;
        }
    }

    private void Update()
    {
        if (canMove)
        {
            MoveBox();
       
        }
    }

    private void MoveBox()
    {
        Vector3 temp = transform.position;
        temp.x += moveSpeed * Time.deltaTime;

        if (temp.x > maxX || temp.x < minX)
        {
            moveSpeed *= -1f;
        }

        transform.position = temp;
    }

    public void DropBox()
    {
        canMove = false;
        myBody.gravityScale = Random.Range(2, 4);
    }

    private void Landed()
    {
        if (gameOver) return;

        ignoreCollision = true;
        ignoreTrigger = true;

        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.MoveCamera();
       
    }

    private void RestartGame()
    {
        Debug.Log("RestartGame() from BoxScript");

        if (GameplayController.instance != null)
        {
            GameplayController.instance.RestartGame();
        }
        else
        {
            Debug.LogWarning("GameplayController.instance is NULL");
        }
    }


    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision) return;

        if (target.gameObject.CompareTag("Platform") || target.gameObject.CompareTag("Box"))
        {
            GameplayController.instance.PlaySound(GameplayController.instance.boxCollisionSound);

            Invoke(nameof(Landed), 1f); // или даже 0.2f
            ignoreCollision = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
            return;

        if (target.gameObject.CompareTag("GameOver"))
        {
            Debug.Log("GameOver Triggered");

            canMove = false;
            moveSpeed = 0f;
            myBody.linearVelocity = Vector2.zero;
           

            CancelInvoke(nameof(Landed));
            gameOver = true;
            ignoreTrigger = true;

            Invoke(nameof(RestartGame), 2f);
        }
    }



}
