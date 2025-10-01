using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;   // drag your Player object here in the Inspector
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
