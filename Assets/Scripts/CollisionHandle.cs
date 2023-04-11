using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandle : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip finishLevel;

    [SerializeField] ParticleSystem explodeParticles;
    [SerializeField] ParticleSystem succesParticles;

    private AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            RespondToDebugKeys();
        }        
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }                
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //collision toggle
        }        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hit friendly object");
                break;
            case "Fuel":
                Debug.Log("Fuel found");
                break;
            case "Finish":
                StartFinishLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
                
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        explodeParticles.Play();
        audioSource.PlayOneShot(explosion);
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartFinishLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        succesParticles.Play();
        audioSource.PlayOneShot(finishLevel);
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
