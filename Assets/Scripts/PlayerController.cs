using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator wolfAnimator;

    [SerializeField] Collider2D cuerpo;

    [SerializeField] UIScore uiScore;
    [SerializeField] private AudioClip pickUp;
    [SerializeField] private AudioClip hurt;
    [SerializeField] private AudioClip PowerUp;


    [Tooltip("El tiempo que dura el slide")]
    [SerializeField] float slideTime;

    [Tooltip("El tiempo que dura la invinsibilidad")]
    [SerializeField] float powerUpTime;
    [Tooltip("Fuerza del salto")]
    public int Jump;
    [Tooltip("Velocidad Actual")]
    public float Velocity;
    float currentVelocity;
    public float fallSpeed;
    public uint score = 0;

    public bool inAir = false;
    public bool grounded = true;
    public bool jump = false;
    public bool slide = false;
    bool JumpState;
    public bool isDead = false;
    public bool powerUp = false;



    private void Awake()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        playerAnimator.SetBool("inAir", inAir);
        playerAnimator.SetBool("jump", jump);
        playerAnimator.SetBool("slide", slide);
        playerAnimator.SetBool("powerUp", powerUp);
        Debug.Log(grounded);
        Debug.Log(inAir);
        if (Input.GetKeyDown("space") && grounded == true && isDead == false)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Jump));
            JumpState = true;
        }
        if (Input.GetKeyDown("space") && grounded == false && isDead == false)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = fallSpeed;
            JumpState = true;
        }

        if (Input.GetKeyDown("d") && isDead == false)
        {
            isDead = true;
            playerAnimator.SetBool("isDead", isDead);


        }
        if (Input.GetKeyDown("r") && grounded == true && isDead == false)
        {
            slide = true;
            StartCoroutine(TiempoSlide());
        }
        else if (Input.GetKeyDown("d") && isDead == true)
        {
            isDead = false;
            playerAnimator.SetBool("isDead", isDead);

        }
        ;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {


        //if (collider.CompareTag("ground"))
        //{
        //    grounded = true;
        //    inAir = false;
        //    jump = false;
        //    JumpState = false;
        //    this.GetComponent<Rigidbody2D>().gravityScale = 1;
        //
        //}

        if (collider.CompareTag("Coin"))
        {
            score += 10;

            collider.gameObject.SetActive(false);
            SoundManager.Instance.PlaySound(pickUp);


        }


        uiScore.SetScore(score);
        if (!powerUp)
        {
            if (collider.tag == "Obstaculo" && slide == true)
            {

                deathSecuence();
            }
            if (collider.tag == "Obstaculo" | collider.tag == "Tronco" && slide == false)
            {
                Debug.Log("Moriste");
                // soundManager.SelecionarAudio(3,0.5f);
                deathSecuence();

            }
            if (collider.CompareTag("Buff"))
            {
                SoundManager.Instance.PlaySound(pickUp);
                currentVelocity = Velocity;
                Velocity = Velocity + Velocity / 2;
                powerUp = true;
                jump = false;
                JumpState = false;
                Debug.Log(powerUp);
                collider.gameObject.SetActive(false);
                //soundManager.SelecionarAudio(1,0.5f);
                StartCoroutine(TiempoBuff());
            }
        }

    }


    private void deathSecuence()
    {
        isDead = true;
        wolfAnimator.SetBool("playerDeath", isDead);
        playerAnimator.SetBool("isDead", isDead);
        //StartCoroutine(TiempoMuerto());

    }



    IEnumerator TiempoSlide()
    {
        yield return new WaitForSeconds(slideTime);
        slide = false;
    }
    IEnumerator TiempoMuerto()
    {

        yield return new WaitForSeconds(5);
        GameObject.Destroy(this.gameObject);
    }
    IEnumerator TiempoBuff()
    {
        yield return new WaitForSeconds(powerUpTime);
        Velocity = currentVelocity;
        powerUp = false;
        grounded = true;
        inAir = false;
        jump = false;
        JumpState = false;
        //  soundManager.SelecionarAudio(2,0.5f);

    }

    private void OnTriggerExit2D(Collider2D collider)
    {

        // StartCoroutine(TiempoEntrePlataformas());
        //  grounded  =false;
        //  inAir =true;
        if (!JumpState)
        {

        }
        if (JumpState == true)
        {
            jump = true;
            grounded = false;
            inAir = true;
        }

    }
    void mantenerBox(Collider2D cuerpo, Collider2D pies)
    {
        if (jump)
        {
            cuerpo.offset = new Vector2(-0.5f, cuerpo.offset.y);
            pies.offset = new Vector2(-0.5f, pies.offset.y);
        }
    }
}
