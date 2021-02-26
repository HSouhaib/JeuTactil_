using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float gravityPower = 5f;
    [SerializeField] private float bouncePower = 1f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private GameObject shattared;
    [SerializeField] private ParticleSystem bounceParticales;
    [SerializeField] private GameObject splashParticles;
    [SerializeField] private float stretchAndSquashFactor = 3f;
    [SerializeField] private AudioClip collisionAC;
    



    private Vector3 motionDirection;
    private float diameter;
    private float myRaduis;
    private Vector3 lastPosition;
    private bool dashing;
    private bool controllable = true;
    private bool won;
    private bool destroyed;
    private AudioSource myAS;
    private int platformsSinceLastSound;
   


     void Start()
    {
        diameter = transform.localScale.x;
        myRaduis = diameter / 2f;
        lastPosition = transform.position;

        myAS = GetComponent<AudioSource>();
        myAS.clip = collisionAC;

    }

     void Update()
    {
        if (destroyed) return;
        
        transform.position += motionDirection * Time.deltaTime;
            
        if (controllable)
        {
            if (Input.GetMouseButtonDown(0))
                dashing = true;
            if (Input.GetMouseButtonUp(0))
                dashing = false;
        }
        

        if (!dashing)
            Idle();
        else
            Dash();

        float yScale = diameter + (transform.position.y - lastPosition.y) * stretchAndSquashFactor;
        transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);

        lastPosition = transform.position;
    }

     void Idle()
    {
        motionDirection.y -= gravityPower * Time.deltaTime;
         
        //Detection 
        if (Physics.Linecast(lastPosition, transform.position - new Vector3(0, myRaduis, 0), out RaycastHit hit))
        {
            motionDirection.y = bouncePower;
            transform.position = new Vector3(transform.position.x, hit.point.y + myRaduis, transform.position.z);

            if (hit.transform.CompareTag("Finish Line"))
                ReachedFinishLine();

            bounceParticales.transform.position = hit.point;
            bounceParticales.Play();
            
            Instantiate(splashParticles, hit.point + new Vector3(0, 0.01f, 0), splashParticles.transform.rotation, hit.transform);

            myAS.pitch = Random.Range(0.9f, 1.1f);
            myAS.Play();
            
        } 
    }

    void Dash()
    {
        motionDirection.y = -dashSpeed;
        if (Physics.Linecast(lastPosition, transform.position - new Vector3(0, myRaduis, 0), out RaycastHit hit))
        {
            if(hit.transform.CompareTag("Damage tile"))
            {
                DestroyMe();
                return;

            }

          if (hit.transform.CompareTag("Normal tile"))
            {
                hit.transform.parent.GetComponent<Platforms>().DestroyPlatform();

                GameManager.Instance.AddScore(1);

                if (platformsSinceLastSound == 3)
                {
                    myAS.pitch += 0.09f;
                    myAS.PlayOneShot(collisionAC);
                    platformsSinceLastSound =  3 ; 
                }
                else
                    platformsSinceLastSound++;
            }
                
            
          if (hit.transform.CompareTag("Finish Line"))
                ReachedFinishLine();
        }
    }

    void ReachedFinishLine()
    {
        if (won) return;



        won = true;
        dashing = false;
        controllable = true;

        LevelManager.Instance.Won();
    }

    void DestroyMe()
    {
        destroyed = true;
        Instantiate(shattared, transform.position, transform.rotation);
        gameObject.SetActive(false);

        LevelManager.Instance.Lost();
    }   
    
}
