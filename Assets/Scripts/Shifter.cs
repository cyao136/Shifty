using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shifter : MonoBehaviour {

    private GameManager gameManager;

    private GameObject shiftHead;
    private GameObject shiftBase;
    private GameObject baseLeft;
    private GameObject baseTop;
    private GameObject baseRight;
    private GameObject baseBottom;

    private float speed_rotate = 4;
    private float speed_movement = 5;

    // current color, (0 = green, 1 = red, 2 = blue, 3 = yellow)
    public int color = 0;

    private float rotation = 0;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shiftBase = GameObject.Find("Shifter/Base");
        baseLeft = GameObject.Find("Shifter/Base/Left");
        baseTop = GameObject.Find("Shifter/Base/Top");
        baseRight = GameObject.Find("Shifter/Base/Right");
        baseBottom = GameObject.Find("Shifter/Base/Bottom");
        shiftHead = GameObject.Find("Shifter/Head");
        // Color is initialized with GameManager
    }

    // Update is called once per frame
    void Update ()
    {
        // ** Shift Color
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.nextShifterColor();
        }
        shiftBase.transform.rotation = Quaternion.Lerp(shiftBase.transform.rotation, Quaternion.Euler(0, 0, this.rotation), Time.deltaTime * this.speed_rotate);


        // ** Movement

        // Mobile
        this.transform.Translate(Input.acceleration.x, 0, 0);

        // PC
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector2.left * speed_movement * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector2.right * speed_movement * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed_movement = 10;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed_movement = 5;
        }

        // Check bound of movement
        if (this.transform.position.x < -2.8f)
        {
            this.transform.position = new Vector3(-2.8f, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.x > 2.8f)
        {
            this.transform.position = new Vector3(2.8f, this.transform.position.y, this.transform.position.z);
        }
    }

    // Called when scheme is changed
    public void updateColorScheme()
    {
        baseTop.GetComponent<SpriteRenderer>().color = gameManager.settings.colors[0];
        baseRight.GetComponent<SpriteRenderer>().color = gameManager.settings.colors[1];
        baseBottom.GetComponent<SpriteRenderer>().color = gameManager.settings.colors[2];
        baseLeft.GetComponent<SpriteRenderer>().color = gameManager.settings.colors[3];
    }

    public void updateColor()
    {
        switch (color)
        {
            case 0:
                this.rotation = 0;
                break;
            case 1:
                this.rotation = 90;
                break;
            case 2:
                this.rotation = 180;
                break;
            case 3:
                this.rotation = 270;
                break;
            default:
                Debug.LogError("Invalid Color State!");
                break;
        }
        shiftHead.GetComponent<SpriteRenderer>().color = gameManager.settings.colors[color];
    }
}
