using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool directionUp;
    public float speed;
    protected Rigidbody2D rigidbody2d;
    protected bool collided = false;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 newPosition;
        if (directionUp)
            newPosition = new Vector2(transform.position.x, transform.position.y + speed * Time.fixedDeltaTime);
        else
            newPosition = new Vector2(transform.position.x, transform.position.y - speed * Time.fixedDeltaTime);

        rigidbody2d.MovePosition(newPosition);
    }
}
