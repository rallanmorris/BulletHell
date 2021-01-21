using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float mainThrust = 50f;
    [SerializeField] float rcsThrust = 100f;

    Rigidbody rigidBody;
    AudioSource boosterAudio;


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

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("okie dokie"); //todo replace
                break;
            default:
                print("dead");
                break;
        }
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
