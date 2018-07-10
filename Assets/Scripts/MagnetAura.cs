using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAura : MonoBehaviour {
    private GameObject player;
    private GameController gameCont;

	void Start () {
        player = GameObject.Find("Player");
        gameCont = GameObject.Find("Main Camera").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(transform.localScale.x - 0.75f * Time.deltaTime, transform.localScale.y - 0.75f * Time.deltaTime, transform.localScale.z - 0.75f * Time.deltaTime);
        if(transform.localScale.x < 0.1f)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            collision.transform.position = new Vector2(Mathf.Lerp(collision.transform.position.x, player.transform.position.x, 0.05f), Mathf.Lerp(collision.transform.position.y, player.transform.position.y, 0.05f));
            float dist = Vector2.Distance(player.transform.position, collision.transform.position);
            if(dist < 0.75f)
            {
                Destroy(collision.gameObject);
                gameCont.AddCoin();
            }
        }
    }
}
