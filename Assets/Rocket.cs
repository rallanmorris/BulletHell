using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float mainThrust = 50f;
    [SerializeField] float rcsThrust = 100f;

    Rigidbody rigidBody;
    AudioSource boosterAudio;
    public AudioSource deathAudio;

    enum State {Alive, Dying, Trancending};
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        boosterAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
        Thrust();
        Rotate();
        }
        else if(state == State.Dying)
        {
            if (boosterAudio.isPlaying)
            {
                boosterAudio.Stop();
            }

            if (!deathAudio.isPlaying)
            {
                deathAudio.Play();
            }
            
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive) {return;}

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Finish":
                state = State.Trancending;
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 2f);
                break;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); //todo allow for more than 2 levels
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))//Can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);

            if (!boosterAudio.isPlaying)
            {
                boosterAudio.Play();
            }
        }
        else
        {
            boosterAudio.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //Ignore game physics while rotating

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        //Cannot rotate both ways at the same time
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-(Vector3.forward) * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //Resume Physics
    }

}
