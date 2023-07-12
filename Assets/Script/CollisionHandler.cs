
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delay = 1.0f;
    [SerializeField] AudioClip collide;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem collideParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    bool isChanging =false;
    bool isCollision = false;

    private void Start()
    {
        
       audioSource = GetComponent<AudioSource>();

     
    }
    private void Update()
    {
        RespondToDebugs();
    }
    private void RespondToDebugs()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            isCollision = !isCollision;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isChanging || isCollision)  { return; }
        switch(collision.gameObject.tag)
        {
            
                 
            case "Start":

                Debug.Log("Start zone");

                break;
            case "Finish":

                SuccessSequence(); //Invoke takes string and time to delay the method

                break;

            case "Untagged":
              
                Crash();
              

                break;
              

        }
   


    }

    private void SuccessSequence()
    {
        successParticles.Play(); //Particles added
        audioSource.Stop();
        isChanging = true;
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;

       
        Invoke("LoadNextLevel", delay);
     
    }

    private void Crash()
    {
        collideParticles.Play();//Particles added
        audioSource.Stop();
        isChanging = true;
        audioSource.PlayOneShot(collide);

        GetComponent<Movement>().enabled = false;
      

        Invoke("ReloadLevel", delay);    //delay system
      
    }
    

    private void LoadNextLevel()// to load next level
    {
        
            
            
        int currenScene = SceneManager.GetActiveScene().buildIndex;

         SceneManager.LoadScene(currenScene + 1);
        int nextScene = currenScene + 1;
        if(nextScene==SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene); 
         
        
    }



    private void ReloadLevel()
    {
        int currentActiveScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentActiveScene);//to load the present active scene,You can use SceneManager.loadScene(0) as well



    }





}
