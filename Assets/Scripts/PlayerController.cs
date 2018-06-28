using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            EndGame();
        }
    }
    void OnBecameInvisible()
    {
        EndGame();
    }

    private void EndGame()
    {
        Destroy(gameObject);
        GameObject.Find("Main Camera").GetComponent<GameController>().EndGame();
    }
}
