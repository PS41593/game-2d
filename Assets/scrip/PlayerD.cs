using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerD : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public bool duocPhepNhay;
    private Rigidbody2D rb;
    //flipx
    private bool isfacingRight = true;
    //di chuyen
    private float h_move;
    //animation
    private Animator anim;

    private bool doubleJump;
    //tham chieu bullet
    public GameObject ballPrefab;
    //gun
    public Transform attackTransform;

    /*public AudioSource soundVatpham;

    [SerializeField]
    public TMP_Text _scoreText;
    private static int _score = 0;

    [SerializeField]
    public TMP_Text _timeText;
    private static float _time = 0;

    [SerializeField]
    public TMP_Text _diamondText;
    private static int _diamond = 0;

    [SerializeField]
    public TMP_Text _livesText;
    private static int _lives = 3;

    [SerializeField]
    //private Slider _healthSlider;
    private static int health = 100;

    [SerializeField]
    private GameObject _gameoverPanel;*/

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //gan gtri mac dinh
        /*_livesText.text = _lives.ToString();
        _scoreText.text = _score.ToString();
        _diamondText.text = _diamond.ToString();

        health = 100;*/
        // _healthSlider.maxValue = health;
        //_healthSlider.value = health;

        //gan gtri cho time
        //_timeText.text = $"{_time:0.00}";
    }

    private void Update()
    {
        //dem thoi gian
        /*_time += Time.deltaTime;
        _timeText.text = $"{_time:0.00}";*/
        //attack
        Fire();

        //flip
        flip();

        //anim
        anim.SetFloat("moving", Mathf.Abs(h_move));

        //move
        h_move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(h_move * speed, rb.velocity.y);

        //2jumps
        if (duocPhepNhay && !Input.GetKey(KeyCode.W))
        {
            doubleJump = false;

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (duocPhepNhay || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = !doubleJump;
            }
        }

    }
    private void Fire()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("attack");
            ballPrefab.transform.localScale = isfacingRight ? new Vector2(0.15f, 0.2095f) : new Vector2(-0.15f, 0.2095f);
            //tao ra vien dan tai vitri sung
            var oneBall = Instantiate(ballPrefab, attackTransform.position, Quaternion.identity);
            //cho vien dan bay theo huong player
            var velocity = new Vector2(7f, 0);
            if (isfacingRight == false)
            {
                velocity = new Vector2(-7f, 0);
            }
            oneBall.GetComponent<Rigidbody2D>().velocity = velocity;
            //dan bien mat 
            Destroy(oneBall, 1f);


            
        }
        
    }
    //==========================
    private void OnTriggerEnter2D(Collider2D hitboxkhac)
    {
        if (hitboxkhac.gameObject.tag == "Ground")
        {
            duocPhepNhay = true;
        }
        //coin bien mat
        /*if (hitboxkhac.gameObject.tag == "Potion")
        {
            health += 10;
            //_healthSlider.value = health;
            soundVatpham.Play();
            _score += 10;
            _scoreText.text = _score + "";
            Destroy(hitboxkhac.gameObject);
        }
        if (hitboxkhac.gameObject.tag == "Diamond")
        {
            soundVatpham.Play();
            _diamond += 10;
            _diamondText.text = _diamond + "";
            Destroy(hitboxkhac.gameObject);
        }*/

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.tag == "Enemie" || collision.gameObject.tag == "Boss")
        {
            health -= 20;
            //_healthSlider.value = health;
            if (health <= 0)
            {
                _lives -= 1;
                //hien thi lives
                _livesText.text = _lives.ToString();
                //reload man choi
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                if (_lives <= 0)
                {
                    _livesText.text = _lives.ToString();
                    //hien panel gameover
                    _gameoverPanel.SetActive(true);
                    //stop game
                    Time.timeScale = 0;
                }
            }
        }*/
    }

    private void OnTriggerExit2D(Collider2D hitboxkhac)
    {
        if (hitboxkhac.gameObject.tag == "Ground")
        {
            duocPhepNhay = false;
        }
    }
    private void flip()
    {
        if (isfacingRight && h_move < 0 || !isfacingRight && h_move > 0)
        {
            isfacingRight = !isfacingRight;
            Vector3 kich_thuoc = transform.localScale;
            kich_thuoc.x = kich_thuoc.x * -1;
            transform.localScale = kich_thuoc;
        }
    }


}
