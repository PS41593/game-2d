using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float movespeed;
    [SerializeField]private float jumpspeed;
    Rigidbody2D rb;
    Animator animator;
    public Transform _canjump;
    public LayerMask nen;
    private bool canjump;
    private bool doublejump;
    private bool _flip;
    [SerializeField] private float down;
    Vector2 Vector;
    public GameObject bulletPrefab;
    public Transform guntransform;
    void Start()
    {
        Vector = new Vector2(0,-Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ten();
    }
    private void Move()
    {
        canjump = Physics2D.OverlapCircle(_canjump.position, 0.2f, nen);
        var Move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(Move*movespeed, rb.velocity.y);            
        if (Input.GetKeyDown(KeyCode.W)&& canjump)
        {                                                      
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
            animator.SetBool("2Jump",true);
            animator.SetBool("ak", false);
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
            animator.SetBool("isRun", true); animator.SetBool("ak", false);
        }
         if(Move == 0 && rb.velocity.y == 0)
        {
            animator.SetBool("isRun", false);
            
            animator.SetBool("2Jump", false);
        }
         if(rb.velocity.y == 0)
        {
            animator.SetBool("2Jump", false);
            animator.SetBool("falling",false);
            
        }        
        if (rb.velocity.y <0 ) 
        { 
            animator.SetBool("falling", true);
            
            rb.velocity -= Vector * down *Time.deltaTime ;
        }
        if (Input.GetKeyUp(KeyCode.E)&& canjump)
        {
            animator.SetBool("ak", true);          
        }
        else animator.SetBool("ak", false);
        transform.localScale = _flip ? new Vector2(5.076945f, 4.419212f) : new Vector2(-5.076945f, 4.419212f);
    }

    private void ten()
    {
        // nhan phim f ban dan
        if (Input.GetKeyUp(KeyCode.E)&& canjump)
        {
           
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

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var name = other.gameObject.name;
        var tag = other.gameObject.tag;
        if (tag == "Bot"||tag =="Trap")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);          
        }
    }
}
