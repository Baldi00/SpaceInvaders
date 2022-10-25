using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int durationFrames = 40;

    private void Update()
    {
        durationFrames--;
        if(durationFrames < 0)
        {
            Destroy(gameObject);
        }
    }
}
