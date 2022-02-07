using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] Animator animator2;
    public int Jump;
    public bool inAir = false;
    public bool grounded = true;
    public bool jump = false;
    public bool slide = false;
    bool JumpState;
    public int dash;
    public int dashForce;
    bool isDead  = false;
    bool powerUp = false;
    public int Velocity;
    int currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("inAir",inAir);
        animator.SetBool("jump",jump);
        animator.SetBool("slide",slide);
        animator.SetBool("powerUp", powerUp);
        Debug.Log(grounded);
        Debug.Log(inAir);
        if(Input.GetKeyDown("space") && grounded == true && isDead ==false)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Jump));
           JumpState =true;
        }
        if(Input.GetKeyDown("d") && isDead == false)
        {
            isDead = true;
            animator.SetBool("isDead",isDead);
            

        }
        if(Input.GetKeyDown("r") && grounded == true&& isDead ==false)
        {
            slide = true;
            StartCoroutine(TiempoSlide());
        }
        else if(Input.GetKeyDown("d")&& isDead == true)
        {
            isDead = false;
            animator.SetBool("isDead", isDead);
            
            
        }

        if(isDead == false)
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(Velocity, this.GetComponent<Rigidbody2D>().velocity.y);
        
    }

    
    private void OnTriggerEnter2D(Collider2D collider) 
    {

        if(collider.CompareTag("ground"))
        {
        grounded = true;
        inAir =false;
        jump =false;
        JumpState = false;

        }
        if(!powerUp)
        {
        if(collider.tag == "Obstaculo" && slide == true)
        {
         deathSecuence();
        }
        if(collider.tag == "Obstaculo"  | collider.tag == "Tronco" && slide == false )
        {
            deathSecuence();
         
        }
        if(collider.CompareTag("Buff"))
        {
            currentVelocity = Velocity;
            Velocity = Velocity + Velocity/2;
            powerUp= true;
            Debug.Log(powerUp);
            DestroyObject(collider.gameObject);
            StartCoroutine(TiempoBuff());
        }
        }
        
    }
    private void OnDestroy() {
        animator2.SetBool("playerDeath", isDead);
    }
    private void deathSecuence()
    {
        isDead=true;
            animator.SetBool("isDead", isDead);
            
            StartCoroutine(TiempoMuerto());
         
    }
    IEnumerator TiempoSlide()
    {
        yield return new WaitForSeconds(1.5f);
        slide = false;
    }
    IEnumerator TiempoMuerto()
    {
        
        yield return new WaitForSeconds(1);
        GameObject.Destroy(this.gameObject);
    }
    IEnumerator TiempoBuff()
    {
        yield return new WaitForSeconds(5);
        Velocity = currentVelocity;
        powerUp = false;
        grounded = true;
        inAir =false;
        jump =false;
        JumpState = false;

    }
    
    private void OnTriggerExit2D(Collider2D collider) {
       
        // StartCoroutine(TiempoEntrePlataformas());
        //  grounded  =false;
        //  inAir =true;
        if(!JumpState)
        {

        }
        if(JumpState == true)
        {
            jump =true;
            grounded  =false;
         inAir =true;
        }
        
    }
}
