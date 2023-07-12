using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // put parameters in top
    // second comes cache
    // third comes private instance variables
    [SerializeField] float zAxis = 1f ;
    [SerializeField] float thrust = 1f ;
    [SerializeField] AudioClip engineSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineBoosters;
    [SerializeField] ParticleSystem rightEngineBoosters;

    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    
       
        ProcessIThrust();
        ProcessRotation();
       
    }
    void ProcessIThrust() // to provide thrust to the rocket
    {
        if (Input.GetKey (KeyCode.Space))
        {
            ThrustOn();
        }
        else
       {
            audioSource.Stop();
            mainEngineParticles.Stop();
       }
       

    }

    private void ThrustOn()
    {
        rb.AddRelativeForce(0, thrust * Time.deltaTime, 0);//You can do vector3.up to add force in y axis instead of (0,y,0)
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineSound);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();

        }
    }

    void ProcessRotation() // to provide rotation to the rocket
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateInRight(-zAxis);// you can use vector3.forward instead of (0,0,z) to change in z axis similary, vector3.right for x axis.
            rightEngineBoosters.Play();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateInRight(zAxis);
            leftEngineBoosters.Play();
        }

    }

  void RotateInRight(float rotationValue)
    {
        rb.freezeRotation = true; //freeze rotation so we can manually rotate
        transform.Rotate(0, 0, rotationValue*Time.deltaTime);
        rb.freezeRotation=false;  //unfreezing rotation so physics can take over
    }
}
