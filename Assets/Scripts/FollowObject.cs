using UnityEngine;
using UnityEngine.UI;

public class FollowObject : MonoBehaviour
{
    public Text remainingShotsLabel;

    private void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(transform.position);
        remainingShotsLabel.transform.position = namePos;
    }
}
