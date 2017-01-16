using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Seed
    public int seed;
    public bool seeded = false;

    public float score = 0;

    private Shifter shifter;
    private GameObject checkpoint;

    public Settings settings;

    // Use this for initialization
    void Start ()
    {
        settings = GameObject.Find("GameManager").GetComponent<Settings>();
        checkpoint = GameObject.Find("Checkpoint");
        shifter = GameObject.Find("Shifter").GetComponent<Shifter>();

        if (!this.seeded)
        {
            this.seed = UnityEngine.Random.Range(10000, 99999);
        }

        // Initialize shifter color
        this.shifter.updateColor();
        this.shifter.updateColorScheme();
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.score += Time.deltaTime;
	}

    // Score displaying
    private void OnGUI()
    {
        GUI.color = Color.white;
        GUILayout.Label(" Score: " + Math.Floor(score).ToString());
    }

    public void nextShifterColor()
    {
        switch (this.shifter.color)
        {
            case 0:
                this.shifter.color = 1;
                break;
            case 1:
                this.shifter.color = 2;
                break;
            case 2:
                this.shifter.color = 3;
                break;
            case 3:
                this.shifter.color = 0;
                break;
            default:
                Debug.LogError("Invalid Color State!");
                break;
        }
        this.shifter.updateColor();
    }

    public void shifterCollision()
    {
        print("BOOM!");
    }

    public void shifterCheckpoint(float pointsAwarded = 1f)
    {
        this.score += pointsAwarded;
        checkpoint.GetComponent<Checkpoint>().IncreaseDifficulty();
    }

    // Random int from int x to y (exclusive)
    public int random(int x, int y)
    {
        this.seed += settings.primes[this.seed % 7];
        return (this.seed % (y - x)) + x;
    }
}
