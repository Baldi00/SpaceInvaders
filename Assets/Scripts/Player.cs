using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode leftButton;
    public KeyCode rightButton;
    public KeyCode fireButton;
    public float speed = 10;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetKey(leftButton) && transform.position.x > -4.5f)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left);
        }
        if (Input.GetKey(rightButton) && transform.position.x < 4.5f)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.right);
        }
        if (Input.GetKeyDown(fireButton) && GameObject.Find("PlayerBullet(Clone)") == null)
        {
            Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y + 0.15f), Quaternion.identity);
            SoundManager.Instance.PlayBullet();
        }
    }
}
