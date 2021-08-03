using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{
    [SerializeField] float flapSpeed = 200f;
    [SerializeField] Rigidbody2D rgb;
    [SerializeField] GameObject manager;
    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        manager = GameObject.Find("GameManager");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rgb.velocity = Vector2.zero;
            rgb.AddForce(Vector2.up * flapSpeed, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathZone"))
        {
            //Play sound
            //Destroy player
            manager.GetComponent<GameManager>().gameOver();
        }

        if (other.gameObject.CompareTag("ScoreZone"))
        {
            //Increment the score
            manager.GetComponent<GameManager>().Scoring();
            //Play sound
        }

    }

}
