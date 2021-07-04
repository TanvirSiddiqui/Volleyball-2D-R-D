using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetCollision : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 offSet = new Vector2(0, 2f);
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Vector2 bounceDir = other.gameObject.transform.position - gameObject.transform.position;
            Vector2 shootForce;
            Debug.Log(bounceDir);
            bounceDir.Normalize();
            shootForce = bounceDir * 100f;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(shootForce);
            Debug.Log("Net Collision: " + shootForce);
        }
    }
}
