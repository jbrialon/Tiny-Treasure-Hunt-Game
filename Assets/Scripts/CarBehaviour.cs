using System.Collections;
using UnityEngine;

/**
 * The car will track the CrossHair and collide with a PackageBehaviour.
 */
public class CarBehaviour : MonoBehaviour
{
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip failSound;

    public GameObject CrossHair;
    public float Speed = 1.2f;
    private AudioSource audioSource;
    private UIHud HUD;
    private UIScoreboard scoreBoard;
    private PackageBehaviour remainingPackage;
    private bool isEnnemyHit = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        HUD = FindObjectOfType<UIHud>();
        scoreBoard = FindObjectOfType<UIScoreboard>();
    }

    private void Update()
    {
        var trackingPosition = CrossHair.transform.position;
        if (Vector3.Distance(trackingPosition, transform.position) < 0.2)
        {
            return;
        }

        var lookRotation = Quaternion.LookRotation(trackingPosition - transform.position);
        lookRotation.x = 0f;
        lookRotation.z = 0f;
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        transform.position = Vector3.MoveTowards(transform.position, trackingPosition, Speed * Time.deltaTime);
    }
    private void removePackage () {
        remainingPackage = FindObjectOfType<PackageBehaviour>();
        Destroy(remainingPackage.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var Package = other.GetComponent<PackageBehaviour>();
        var Ennemy = other.GetComponent<EnnemyBehaviour>();

        // we hit a Package
        if (Package != null)
        {
            if (HUD.isTimerRunning && !isEnnemyHit) {
                Destroy(other.gameObject);
                audioSource.PlayOneShot(successSound);
                HUD.IncreaseScore();
            }
        }

        // we hit an Ennemy
        if (Ennemy != null)
        {
            if (HUD.isTimerRunning) {
                isEnnemyHit = true;
                audioSource.PlayOneShot(failSound);
                scoreBoard.showGameOver();
                removePackage();
            }
        }

    }
}
