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
                if (wallBrick.color.a == 0.5f)
                    Destroy(collision.gameObject);
                else
                    wallBrick.color = new Color(0f, 1f, 0f, 0.5f);
                Destroy(gameObject);
            }
            if (tag == "UpCollider")
            {
                collided = true;
                GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
                explosion.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
                Destroy(gameObject);
            }
            if (tag == "Enemy" || tag == "Ufo")
            {
                collided = true;
                int points = collision.gameObject.GetComponent<Enemy>().points;
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerKilledEnemy(points, tag);

                GameObject explosion = Instantiate(explosionPrefab, collision.gameObject.transform.position, Quaternion.identity);
                if (tag == "Ufo")
                {
                    explosion.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                    SoundManager.Instance.StopPlaying();
                }
                Destroy(collision.gameObject);
                Destroy(gameObject);
                SoundManager.Instance.PlayExplosion();
            }
            if (tag == "Bullet")
            {
                collided = true;
                Instantiate(explosionPrefab, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Destroy(gameObject);
                SoundManager.Instance.PlayExplosion();
            }
        }
    }
}
