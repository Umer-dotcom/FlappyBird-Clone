using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Scroller : MonoBehaviour
{
    [SerializeField] float scrollingSpeed;
    [SerializeField] Rigidbody2D rgb;
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rgb.velocity = Vector2.left * scrollingSpeed * Time.deltaTime;  //Using velocity to move it
    }

    public void setScrollingSpeed(float speed)
    {
        scrollingSpeed = speed;
    }
}
