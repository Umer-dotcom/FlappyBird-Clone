using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{
    [SerializeField] float flapSpeed = 200f;
    [SerializeField] Rigidbody2D rgb;
    [SerializeField] GameObject manager;
    [SerializeField] bool isDead = false;
    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        manager = GameObject.Find("GameManager");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            AudioManager.PlaySound("wing"); //Play sound
            rgb.velocity = Vector2.zero;
            rgb.AddForce(Vector2.up * flapSpeed, ForceMode2D.Force);
        }

        if(GetComponent<Transform>().position.y >= 7f && isDead == false)
        {
            isDead = true;
            //Play sound
            AudioManager.PlaySound("hit");  //Play sound
            //Destroy player
            manager.GetComponent<GameManager>().gameOver();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathZone"))
        {
            isDead = true;
            //Play sound
            AudioManager.PlaySound("hit");  //Play sound
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
