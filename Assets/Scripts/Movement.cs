using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 50f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainRocketParticles;
    [SerializeField] ParticleSystem leftBoostParticles;
    [SerializeField] ParticleSystem rightBoostParticles;

    private Rigidbody rbody;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Main controls
        ProcessThrust();
        ProcessRotation();       
    }        

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!mainRocketParticles.isPlaying)
        {
            mainRocketParticles.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void StopThrusting()
    {
        mainRocketParticles.Stop();
        audioSource.Stop();
    }    
        
    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightBoostParticles.isPlaying)
        {
            rightBoostParticles.Play();
        }
    }      

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftBoostParticles.isPlaying)
        {
            leftBoostParticles.Play();
        }
    }

    void StopRotating()
    {
        rightBoostParticles.Stop();
        leftBoostParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rbody.freezeRotation = true; //freeze rotation to manual rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rbody.freezeRotation = false; //Let physics system take over again
    }
}
