using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 100;
    [SerializeField]private float movespeed;
    [SerializeField]private float jumpspeed;
    [SerializeField]private float climSpeed = 10f;
    private bool isclimp;
    Rigidbody2D rb;
    Animator animator;
    public Transform _canjump;
    public LayerMask nen;
    private bool canjump;
    
    private bool _flip;
    [SerializeField] private float down;
    Vector2 Vector;
    
    public GameObject bulletPrefab;
    public Transform guntransform;
    CapsuleCollider2D capsuleCollider;
    
    private float cooldown =1f;
    private float fire = 0f;

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _CoinSound;

    [SerializeField] private Slider SlHP;
    
    
    void Start()
    {
        Vector = new Vector2(0,-Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
       //gravti = rb.gravityScale;
       health = maxHealth;
        SlHP.value = health;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        bow();

    }
    public void bow()
    {
        if (Input.GetKeyUp(KeyCode.Space) && Time.time > fire)
        {
            ten();
            fire = Time.time + cooldown;
        }
    }
    public void cooldowns() 
    {
        if(cooldown >0) 
        {
            ten(); 
            cooldown = fire ;
        }
    }
    private void Move()
    {
        canjump = Physics2D.OverlapCircle(_canjump.position, 0.2f, nen);
        var Move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(Move*movespeed, rb.velocity.y);            
        if (Input.GetKeyDown(KeyCode.W)&& canjump)
        {                                                      
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
            animator.SetTrigger("jump");
        }
        if (Move > 0 )
        {
            _flip = true;          
        }
        else if (Move < 0)
        {
            _flip = false;           
        }
        if (rb.velocity.y == 0&& Move >0 ||rb.velocity.y==0 && Move <0) 
        {
            animator.SetBool("isRun", true); 
            
        }
         if(Move == 0 && rb.velocity.y == 0)
        {
            animator.SetBool("isRun", false);
            animator.SetBool("falling", false);
        }
         if(rb.velocity.y == 0)
        {          
            animator.SetBool("falling",false);           
        }        
        if (rb.velocity.y <0 ) 
        { 
            animator.SetBool("falling", true);           
            rb.velocity -= Vector * down *Time.deltaTime;
        }      
        transform.localScale = _flip ? new Vector2(5.076945f, 4.419212f) : new Vector2(-5.076945f, 4.419212f);
    }

    private void ten()
    {
        
        
        
            animator.SetTrigger("ak");
            bulletPrefab.transform.localScale = _flip ? new Vector2(0.15f, 0.2095f) : new Vector2(-0.15f, 0.2095f);
            // tao ra vien dan tai vi tri sung
            var onBullet = Instantiate(bulletPrefab, guntransform.position, Quaternion.identity);
            // Cho vien dan bay theo huong nhan vat                     
            var velocity = new Vector2(10f, 0);
            if (_flip == false)
            {
                velocity = new Vector2(-10f, 0);              
            }                      
            onBullet.GetComponent<Rigidbody2D>()
                .velocity = velocity;
            // Destroy Dan
            Destroy(onBullet, 2f);                 
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      
        var tag = other.gameObject.tag;
        if (tag == "Bot"||tag =="Trap")
        {
            TakeDamage(10);
            SlHP.value = health;
            if (health == 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(0);
            }
        }
        
        if (other.gameObject.tag == "Ladder")
        {
            
            isclimp = true;
            rb.gravityScale = 0f;         
        }

        if (other.gameObject.tag == "Healing")
        {
            health += 10;
            Destroy(other.gameObject);
            if (health > 100) 
            {
                health = maxHealth;
            }
            SlHP.value = health;
        }
        if (other.gameObject.tag == "Coin")
        {
            _audioSource.PlayOneShot(_CoinSound);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            rb.gravityScale = 1f; 
            isclimp =false;
        }
    }
    private void FixedUpdate()
    {
        if (isclimp)
        {
            float climninput = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, climninput * climSpeed);
        }
    }

    
    public void TakeDamage(int damage)
    {
        health -= damage; 
        
    }
}
