using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float dampingSpeed;

    private float currentShakeDuration;
    private Vector3 initialPosition;

    void OnEnable()
    {
        initialPosition = gameObject.transform.localPosition;
    }

    private void Update()
    {
        if (currentShakeDuration > 0)
        {
            gameObject.transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            currentShakeDuration = 0f;
            gameObject.transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        currentShakeDuration = shakeDuration;
    }
}
