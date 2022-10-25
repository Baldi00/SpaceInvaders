using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collided)
        {
            string tag = collision.gameObject.tag;
            if (tag == "WallBrick")
            {
                collided = true;
                SpriteRenderer wallBrick = collision.gameObject.GetComponent<SpriteRenderer>();
                if (wallBrick.color.r == 0.5f)
                    Destroy(collision.gameObject);
                else
                    wallBrick.color = new Color(0f, 1f, 0f, 0.5f);

                Destroy(gameObject);
            }
            if (tag == "Player")
            {
                collided = true;
                Destroy(collision.gameObject);
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerDestroyed();
                Destroy(gameObject);
            }
        }
    }
}
