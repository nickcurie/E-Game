using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float dampTime;

    private Camera cameraComponent;
    private Vector3 velocity;

    private void Awake()
    {
        cameraComponent = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        Vector3 playerPos = cameraComponent.WorldToViewportPoint(player.transform.position);
        Vector3 delta = player.transform.position - cameraComponent.ViewportToWorldPoint(new Vector3(0.45f, 0.5f, playerPos.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        //Debug.Log(transform.position);
    }
}
