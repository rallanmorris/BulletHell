using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource boosterAudio;
    public int thrust = 3;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        boosterAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))//Can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * thrust);

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
        //Cannot rotate both ways at the same time
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-(Vector3.forward));
        }
    }

}
