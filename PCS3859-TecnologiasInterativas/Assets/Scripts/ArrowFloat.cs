using Unity.VisualScripting;
using UnityEngine;

public class ArrowFloat : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool shouldStartActive = false;

    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private float amplitude = 0.5f;

    [SerializeField]
    private float frequency = 1f;

    [SerializeField]
    private EventScriptableObject eventToDisable;

    [SerializeField]
    private EventScriptableObject eventToEnable;

    void Start()
    {
        startPos = transform.position;
        gameObject.SetActive(shouldStartActive);
        eventToDisable?.AddListener(DisableArrow);
        eventToEnable?.AddListener(EnableArrow);
    }
    void FixedUpdate()
    {
        this.transform.Rotate(Vector3.left);

        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        this.transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void EnableArrow()
    {
        this.gameObject.SetActive(true);
    }

    private void DisableArrow()
    {
        this.gameObject.SetActive(false);
    }
}
