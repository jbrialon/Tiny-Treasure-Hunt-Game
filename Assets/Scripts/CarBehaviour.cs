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
    private EnnemyBehaviour remainingEnnemy;
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

    private void RemovePackage () {
        remainingPackage = FindObjectOfType<PackageBehaviour>();
        Destroy(remainingPackage.gameObject);
    }

    private void RemoveEnnemy () {
        remainingEnnemy = FindObjectOfType<EnnemyBehaviour>();
        if (remainingEnnemy != null) {
            Destroy(remainingEnnemy.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var Package = other.GetComponent<PackageBehaviour>();
        var Ennemy = other.GetComponent<EnnemyBehaviour>();

        // We hit a Package
        if (Package != null)
        {
            if (HUD.isTimerRunning && !isEnnemyHit) {
                Destroy(other.gameObject);
                RemoveEnnemy();
                audioSource.PlayOneShot(successSound);
                HUD.IncreaseScore();
            }
        }

        // We hit an Ennemy
        if (Ennemy != null)
        {
            if (HUD.isTimerRunning && !isEnnemyHit) {
                isEnnemyHit = true;
                audioSource.PlayOneShot(failSound);
                scoreBoard.showGameOver();
                HUD.HideHUD();
            }
        }

    }
}
