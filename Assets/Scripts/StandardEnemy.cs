using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : Enemy
{
    public float maxPosition = 0.8f;
    public float movementSideStep = 0.08f;
    public float movementDownStep = 0.1f;
    public float movementOffset;
    private float position = 0f;
    private int direction = -1;
    private float remainingTime;

    private void Start()
    {
        remainingTime = 5.5f - speed + movementOffset;
    }

    private void Update()
    {
        if (GameManager.IsGameRunning)
        {
            remainingTime -= 7.5f * Time.deltaTime;
        }

        if(remainingTime <= 0)
        {
            if(transform.position.y <= -2.6f)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerLost();
            }

            if (direction == -1 && position > -maxPosition)
            {
                transform.Translate(movementSideStep * Vector2.left);
                position -= movementSideStep;
            }
            else if (direction == 1 && position < maxPosition)
            {
                transform.Translate(movementSideStep * Vector2.right);
                position += movementSideStep;
            }
            else if ((direction == -1 && position <= -maxPosition) || (direction == 1 && position >= maxPosition))
            {
                transform.Translate(movementDownStep * Vector2.down);
                direction = -direction;
            }

            remainingTime = 5.5f - speed;
        }
    }
}
