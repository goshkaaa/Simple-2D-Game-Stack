using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [HideInInspector]
    public Vector3 targetPos;

    private float smoothMove = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothMove * Time.deltaTime);
    }
}
