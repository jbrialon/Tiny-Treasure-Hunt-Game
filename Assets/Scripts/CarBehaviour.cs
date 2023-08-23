using System.Collections;
using UnityEngine;

/**
 * The car will track the CrossHair and collide with a PackageBehaviour.
 */
public class CarBehaviour : MonoBehaviour
{
    [SerializeField] AudioClip successSound;

    public GameObject CrossHair;
    public float Speed = 1.2f;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        var trackingPosition = CrossHair.transform.position;
        if (Vector3.Distance(trackingPosition, transform.position) < 0.1)
        {
            return;
        }

        var lookRotation = Quaternion.LookRotation(trackingPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        transform.position = Vector3.MoveTowards(transform.position, trackingPosition, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var Package = other.GetComponent<PackageBehaviour>();
        if (Package != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(successSound);
            }
            Destroy(other.gameObject);
        }
    }
}
