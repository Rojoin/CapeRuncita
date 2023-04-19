using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator wolfAnimator;
    [SerializeField] private Transform raycastOrigin;

    private BoxCollider2D collider;
    private Rigidbody2D rb;
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
    public float fallSpeed;
    public uint score = 0;

    public bool inAir = false;
    public bool grounded = true;
    public bool jump = false;
    public bool slide = false;
    bool JumpState;
    public bool isDead = false;
    public bool powerUp = false;
    [SerializeField] private float rayDistance;


    private void Awake()
    {

    }
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = !IsIntheAir();
        SetAnimatorVariables();

    }


    public void JumpMovement()
    {
        if (grounded)
        {
            Debug.Log("Salto");
            rb.AddForce(new Vector2(0, Jump), ForceMode2D.Impulse);
            JumpState = true;
        }
        else
        {
            rb.velocity = new Vector2(0, -fallSpeed);
        }
    }

    public void SlideMovement()
    {
        if (grounded)
        {
            Debug.Log("Sliding");
            slide = true;
            StartCoroutine(TiempoSlide());
        }
    }

    private void SetAnimatorVariables()
    {
        playerAnimator.SetBool("inAir", inAir);
        playerAnimator.SetBool("jump", jump);
        playerAnimator.SetBool("slide", slide);
        playerAnimator.SetBool("powerUp", powerUp);
    }

    bool IsIntheAir()
    {
        bool doesHit = Physics2D.Raycast(raycastOrigin.position, Vector3.down, rayDistance);
        if (doesHit)
        {
            return !Physics2D.Raycast(raycastOrigin.position, Vector3.down, rayDistance).collider.CompareTag("ground");
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
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
    //    Velocity = currentVelocity;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastOrigin.position, Vector3.down * rayDistance);
    }
}
