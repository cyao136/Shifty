using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    // current color, (0 = green, 1 = red, 2 = blue, 3 = yellow)
    public int color = 0;
    public int numColors = 5;
    public float speed = 0f;

    private GameManager gameManager;


    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        this.GetComponent<SpriteRenderer>().color = gameManager.settings.colors[color];
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (this.transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
    }

    // Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Shifter>() != null)
        {
            if (collision.GetComponent<Shifter>().color != this.color)
            {
                gameManager.shifterCollision();
            }
            else
            {
                gameManager.shifterCheckpoint(10 / this.gameObject.transform.localScale.x);
            }
        }
    }
}
