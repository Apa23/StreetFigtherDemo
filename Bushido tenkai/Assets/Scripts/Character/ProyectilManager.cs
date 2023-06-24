using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilManager : MonoBehaviour
{
    // Move direction
    private bool _right = true;

    private void Update()
    {
        // Perform movement in the especific direction
        if (_right)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(Vector3.right * 3 * Time.deltaTime);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(Vector3.left * 3 * Time.deltaTime);
        }
    }

    public void setDirection(bool right) // Set the direction
    {
        _right = right;
    }

    private void OnCollisionEnter2D(Collision2D other) //Detect collision
    {
        if (other.collider.CompareTag("Player")) // When the player collides with the player deal damage
        {
            other.gameObject.GetComponent<PlayerManager>().TakeHit(20);
            Destroy(gameObject);
        }
    }
}
