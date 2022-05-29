using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

public audioManager audiomanager;
    [SerializeField] Animator animator;
    [SerializeField] Animator animator2;

    [SerializeField]Collider2D cuerpo;
    [SerializeField]Collider2D pies;
    SoundManager soundManager;
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
    public float fallSpeed;
    public delegate void RequestingPlayerDies();
    public static event RequestingPlayerDies OnRequestingPlayerDies;
    public delegate void RequestingPlayerPickUp();
    public static event RequestingPlayerDies OnRequestingPlayerPickUp;
     public delegate void RequestingPlayerDeath();
    public static event RequestingPlayerDeath OnRequestingPlayerDeath;

    public bool inAir = false;
    public bool grounded = true;
    public bool jump = false;
    public bool slide = false;
    bool JumpState;
    public bool isDead  = false;
    public bool powerUp = false;
    bool checkSpeedUp = false;
   
    
    private void Awake() {
        soundManager = FindObjectOfType<SoundManager>();
    }
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
        if(Input.GetKeyDown("space") && grounded == true && isDead ==false )
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Jump));
           JumpState =true;
        }
        if(Input.GetKeyDown("space") && grounded == false && isDead ==false )
        {
            this.GetComponent<Rigidbody2D>().gravityScale = fallSpeed;
           JumpState =true;
        }
        // if(Input.GetKeyDown("space")&& inHouse)
        // {
        //     exitHouse();
        //     inHouse = false;
        //     grounded = true;
        // inAir =false;
        // jump =false;
        // JumpState = false;
        // }
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
        mantenerBox(cuerpo,pies);
    }

    
    private void OnTriggerEnter2D(Collider2D collider) 
    {

        // if(collider.CompareTag("House"))
        // {
        //   powerUp =false;
        //   animator.SetBool("powerUp", powerUp);
        //     enterHouse();
        //     animator.SetBool("inHouse",inHouse);
        //   grounded = true;
        //   inAir =false;
        //   jump =false;
        //   JumpState = false;
        //   this.GetComponent<Rigidbody2D>().gravityScale= 1;
        //   StopCoroutine("TiempoBuff");
        //   if(powerUp)
        //             {
        //             powerUp =false;
        //             StopCoroutine("TiempoBuff");
        //             }

        // }
        if(collider.CompareTag("ground"))
        {
        grounded = true;
        inAir =false;
        jump =false;
        JumpState = false;
        this.GetComponent<Rigidbody2D>().gravityScale = 1;

        }
       
        if (collider.CompareTag("Queso"))
        {
            scenes.puntosActuales = scenes.puntosActuales + 10;
            DestroyObject(collider.gameObject);
            OnRequestingPlayerPickUp?.Invoke();
            
        }
        if (collider.CompareTag("Salame"))
        {
            scenes.puntosActuales = scenes.puntosActuales + 15;
            DestroyObject(collider.gameObject);
            OnRequestingPlayerPickUp?.Invoke();
        }
        if (collider.CompareTag("Pan"))
        {
            scenes.puntosActuales = scenes.puntosActuales + 5;
            DestroyObject(collider.gameObject);
            OnRequestingPlayerPickUp?.Invoke();
            
        }
        if(!powerUp)
                    {
                    if(collider.tag == "Obstaculo" && slide == true)
                        {
                             
                        deathSecuence();
                        }
                    if(collider.tag == "Obstaculo"  | collider.tag == "Tronco" && slide == false )
                        {   Debug.Log("Moriste");
                            // soundManager.SelecionarAudio(3,0.5f);
                        deathSecuence();
                        
                        }
                    if(collider.CompareTag("Buff"))
                        {
                             OnRequestingPlayerPickUp?.Invoke();
                            currentVelocity = Velocity;
                            Velocity = Velocity + Velocity/2;
                            powerUp= true;
                            jump =false;
                        JumpState = false;
                            Debug.Log(powerUp);
                            DestroyObject(collider.gameObject);
                             //soundManager.SelecionarAudio(1,0.5f);
                            StartCoroutine(TiempoBuff());
                        }
            }
        
    }
    
    private void OnDestroy() {
        audiomanager.isPlayerDeath=true;
    }
    private void deathSecuence()
    {
        isDead=true;
        animator2.SetBool("playerDeath", isDead);
        animator.SetBool("isDead", isDead);
        audiomanager.isPlayerDeath=true;
        //StartCoroutine(TiempoMuerto());
         
    }
    
 
    IEnumerator llegaraCasa()
    {
        yield return new WaitForSeconds(1);
        Velocity = 0;
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
     //  soundManager.SelecionarAudio(2,0.5f);

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
    void mantenerBox(Collider2D cuerpo, Collider2D pies)
    {
        if(jump)
        {
            cuerpo.offset = new Vector2(-0.5f, cuerpo.offset.y);
            pies.offset = new Vector2(-0.5f, pies.offset.y);
        }
    }
}
