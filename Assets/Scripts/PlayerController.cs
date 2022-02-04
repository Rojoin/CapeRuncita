using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Animator animator;
    public int Jump;
    public bool inAir = false;
    public bool grounded = true;
    public bool jump = false;
    bool JumpState;
    public int dash;
    public int dashForce;
    bool isDead  = false;
    public int Velocity;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("inAir",inAir);
        animator.SetBool("jump",jump);
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
        else if(Input.GetKeyDown("d")&& isDead == true)
        {
            isDead = false;
            animator.SetBool("isDead", isDead);
            
        }
//Codigo de Dash
        // if(Input.GetKeyDown("a")&& dash > 0)
        // {
        //     this.GetComponent<Rigidbody2D>().velocity = transform.right * dashForce;
        // }
        
        if(isDead == false)
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(Velocity, this.GetComponent<Rigidbody2D>().velocity.y);
        
    }

    
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        grounded = true;
        inAir =false;
        jump =false;
        JumpState = false;
        if(collider.tag == "Obstaculo")
        {
            isDead=true;
            animator.SetBool("isDead", isDead);
            StartCoroutine(TiempoMuerto());
         
        }
        if(collider.CompareTag("Buff"))
        {
            dash++;
        }
    }
    IEnumerator TiempoMuerto()
    {
        
        yield return new WaitForSeconds(1);
        GameObject.Destroy(this.gameObject);
    }
    
    private void OnTriggerExit2D(Collider2D collider) {
        grounded  =false;
        inAir =true;
        if(JumpState == true)
        {
            jump =true;
        }
        
    }
}
