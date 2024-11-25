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

    void Start()
    {
        startPos = transform.position;
        this.gameObject.SetActive(shouldStartActive);
    }

    private void FixedUpdate()
    {
        this.transform.Rotate(Vector3.up);

        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        this.transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
