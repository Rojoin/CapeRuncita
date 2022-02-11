using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] Animator animator2;
    [SerializeField] SceneController scenes;

    [Tooltip("El tiempo que dura el slide")]
    [SerializeField] float tiempoSlide;

    [Tooltip("El tiempo que dura la invinsibilidad")]
    [SerializeField] float tiempoPowerUp;
    [Tooltip("Fuerza del salto")]
    public int Jump;
    [Tooltip("Velocidad Actual")]
    public float Velocity;
    float currentVelocity;
    public bool inAir = false;
    public bool grounded = true;
    public bool jump = false;
    public bool slide = false;
    bool JumpState;
    public bool isDead  = false;
    bool powerUp = false;
    bool checkSpeedUp = false;
    public bool inHouse = false;
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
        if(Input.GetKeyDown("space") && grounded == true && isDead ==false && inHouse==false)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Jump));
           JumpState =true;
        }
        if(Input.GetKeyDown("space")&& inHouse)
        {
            exitHouse();
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

        if(collider.CompareTag("House"))
        {
            enterHouse();
        }
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
    public void modifyVelocity(float newVelocity)
    {
        if(powerUp)
        {
           checkSpeedUp=true;
        }
        if(!powerUp)
        {
        Velocity += newVelocity;
        currentVelocity = Velocity;
        checkSpeedUp = false;
        }
    }
    private void OnDestroy() {
        
    }
    private void deathSecuence()
    {
        isDead=true;
        animator2.SetBool("playerDeath", isDead);
        animator.SetBool("isDead", isDead);
        //StartCoroutine(TiempoMuerto());
         
    }
    
    public void enterHouse()
    {
        inHouse = true;
        currentVelocity = Velocity;
        Velocity = 0;
        scenes.timerActive= false;
        scenes.nextUpdate = scenes.nextUpdate+10;

    
        
    }
    public void exitHouse()
    {
        Velocity = currentVelocity +2;
        scenes.timerActive= true;
            inHouse = false;
        
    }
    IEnumerator TiempoSlide()
    {
        yield return new WaitForSeconds(tiempoSlide);
        slide = false;
    }
    IEnumerator TiempoMuerto()
    {
        
        yield return new WaitForSeconds(5);
        GameObject.Destroy(this.gameObject);
    }
    IEnumerator TiempoBuff()
    {
        yield return new WaitForSeconds(tiempoPowerUp);
        Velocity = currentVelocity;
        powerUp = false;
        grounded = true;
        inAir =false;
        jump =false;
        JumpState = false;
        if(checkSpeedUp)
        {
            modifyVelocity(scenes.velocidadNueva);
        }

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
