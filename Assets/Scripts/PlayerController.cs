using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{



    private BoxCollider2D collider;
    private Rigidbody2D rb;
    [SerializeField] UIScore uiScore;
    [SerializeField] private AudioClip pickUp;
    [SerializeField] private AudioClip hurt;
    [SerializeField] private AudioClip PowerUp;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator wolfAnimator;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float rayDistance;

    [SerializeField] float slideTime;
    [SerializeField] float powerUpTime;

    public int Jump;
    public float fallSpeed;
    public uint score = 0;
    public bool inAir = false;
    public bool grounded = true;
    public bool isJumping = false;
    public bool slide = false;
    public bool isDead = false;


    void Start()
    {
        Init();
    }


    void Update()
    {
        grounded = !IsIntheAir();
        CheckAirDistance();
        SetAnimatorVariables();
    }
    private void Init()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        isDead = false;
    }


    public void JumpMovement()
    {
        if (grounded)
        {
            Debug.Log("Salto");
            rb.AddForce(new Vector2(0, Jump), ForceMode2D.Impulse);
            isJumping = true;
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

    private void CheckAirDistance()
    {
        if (isJumping && rb.velocity.y < 0.0f)
        {
            isJumping = false;
        }
    }
    private void SetAnimatorVariables()
    {
        playerAnimator.SetBool("inAir", inAir);
        playerAnimator.SetBool("jump", isJumping);
        playerAnimator.SetBool("Slide", slide);
        playerAnimator.SetBool("Ground", grounded);
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

        if (collider.tag == "Obstaculo" || (collider.tag == "Tronco" && !slide))
        {
            DeathSequence();
        }


    }
    private void DeathSequence()
    {
        isDead = true;
        wolfAnimator.SetBool("playerDeath", isDead);
        playerAnimator.SetTrigger("Dead");
    }

    IEnumerator TiempoSlide()
    {
        yield return new WaitForSeconds(slideTime);
        slide = false;
        playerAnimator.SetBool("Slide", slide);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastOrigin.position, Vector3.down * rayDistance);
    }
}
