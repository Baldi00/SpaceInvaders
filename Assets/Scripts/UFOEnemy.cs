using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOEnemy : Enemy
{
    private void Update()
    {
        if (GameManager.IsGameRunning)
        {
            if (transform.position.x <= -4)
            {
                Destroy(gameObject);
                SoundManager.Instance.StopPlaying();
            }
            transform.Translate(speed * Time.deltaTime * Vector2.left);
        }
    }
}
