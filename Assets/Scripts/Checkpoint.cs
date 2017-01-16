using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public int numBlocks;
    public float speed = 3f;
    public GameObject blocks;
    // used in float randoms from int seed
    private float scaler = 1000f;
    static int minWidthScale = 1000;
    static int goalWidthScale = 4900;
    static float leftBound = -3.15f;

    private int maxRepeat = 2000;
    private int minRepeat = 1500;
    private float timer = 0f;
    private float balanceFactor = 4f;

    private GameManager gameManager;
    private Settings settings;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            CreateBlocks();
            timer = gameManager.random(minRepeat, maxRepeat) / scaler;
        }
    }

    // Increase difficulty
    public void IncreaseDifficulty ()
    {
        int choice = gameManager.random(0, 4);
        switch(choice)
        {
            case 0:
                if ((speed / minRepeat) < balanceFactor)
                {
                    minRepeat -= 25;
                }
                break;
            case 1:
                if (maxRepeat > minRepeat)
                {
                    maxRepeat -= 25;
                }
                break;
            case 2:
                speed += 0.05f;
                break;
            default:
                break;
        }
    }

    // Block creation
    void CreateBlocks ()
    {
        numBlocks = gameManager.random(2, 4);
        float curPos = leftBound;
        int curWidthScale = 0;
        int maxWidthScale = goalWidthScale - ((numBlocks - 1) * minWidthScale);
        bool hasPassable = false;
        int numBlocksCreated = 0;
        for (int i = 0; i < numBlocks; i++)
        {
            // Generate a random scale
            int widthScale;

            //if we are generating the last block, we just scale it to the goal
            if (i == numBlocks - 1)
            {
                widthScale = goalWidthScale - curWidthScale;
            }
            else
            {
                widthScale = gameManager.random(minWidthScale, maxWidthScale);
            }
            GameObject newBlock = Instantiate(blocks);
            newBlock.transform.localScale = new Vector3((widthScale / scaler), newBlock.transform.localScale.y, newBlock.transform.localScale.z);
            float width = newBlock.GetComponent<BoxCollider2D>().size.x * (widthScale / scaler);
            newBlock.transform.position = new Vector3(curPos + width / 2, newBlock.transform.position.y, newBlock.transform.position.z);
            // if last block is created without any passable blocks, create a normal passable block
            if ((i == numBlocks - 1) && (!hasPassable))
            {
                newBlock.GetComponent<Box>().color = gameManager.random(0, 4);
            }
            else
            {
                newBlock.GetComponent<Box>().color = gameManager.random(0, newBlock.GetComponent<Box>().numColors);
            }

            if (newBlock.GetComponent<Box>().color < 4)
            {
                hasPassable = true;
            }

            newBlock.GetComponent<Box>().speed = this.speed;

            curPos += (width);
            curWidthScale += widthScale;
            numBlocksCreated++;
            maxWidthScale = goalWidthScale - (curWidthScale + (numBlocks - numBlocksCreated - 1) * minWidthScale);
        }
    }
}
