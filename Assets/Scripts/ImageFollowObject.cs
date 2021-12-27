using UnityEngine;
using UnityEngine.UI;

public class ImageFollowObject : MonoBehaviour
{
    [SerializeField] private Image imageToFollow;

    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(transform.position);
        imageToFollow.transform.position = namePos;
        if (Input.GetButtonDown("Cancel"))
        {
            imageToFollow.enabled = !imageToFollow.enabled;
        }
    }
}
