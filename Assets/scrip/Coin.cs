using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float coinValue = 100f;
    [SerializeField] AudioClip Coinn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            AudioSource.PlayClipAtPoint(Coinn,Camera.main.transform.position);
            FindObjectOfType<GameController>().AddScore((int)coinValue);
            Destroy(gameObject);
            
        }
    }
}
