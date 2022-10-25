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
                if (wallBrick.color.a == 0.5f)
                    Destroy(collision.gameObject);
                else
                    wallBrick.color = new Color(0f, 1f, 0f, 0.5f);

                Destroy(gameObject);
            }
            if (tag == "Player")
            {
                collided = true;
                GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
                explosion.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1f);
                collision.gameObject.SetActive(false);
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerDestroyed();
                Destroy(gameObject);
                SoundManager.Instance.PlayExplosion();
            }
            if (tag == "DownCollider")
            {
                collided = true;
                Destroy(gameObject);
            }
        }
    }
}
