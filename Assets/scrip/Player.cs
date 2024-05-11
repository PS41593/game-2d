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
    public Transform _canjump;
    public LayerMask nen;
    private bool canjump;
    private bool doublejump;
    private bool _flip;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        canjump = Physics2D.OverlapCircle(_canjump.position, 0.2f, nen);
        var Move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(Move*movespeed, rb.velocity.y); 
        
       
        if (!Input.GetKeyDown(KeyCode.W)&& canjump)
        {
            doublejump = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (canjump || doublejump) 
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
                doublejump = !doublejump;
            }
        }
        if(Move > 0)          
        {
           _flip = true;
        }
        if (Move < 0)
        {
            _flip = false;
        }
        transform.localScale = _flip ? new Vector2(5.076945f, 4.419212f) : new Vector2(-5.076945f, 4.419212f);
    }
}
