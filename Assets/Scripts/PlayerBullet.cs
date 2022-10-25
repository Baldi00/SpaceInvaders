using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
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
            if (tag == "Enemy")
            {
                collided = true;
                int points = collision.gameObject.GetComponent<Enemy>().points;
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerKilledEnemy(points);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}