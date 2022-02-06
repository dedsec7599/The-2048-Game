using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  //This is the heart of the game. This handles all the functions from taking input from user to making calls for updating highscore & score UI and values, New Game button, win and lose conditions. 
    public static GameController instance;
    public static int ticker;

    [SerializeField] GameObject fillPrefab;
    [SerializeField] CellShift[] allCells;

    public static Action<string> slide;
    public int myScore;
    public int highScore;
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] TextMeshProUGUI highScoreDisplay;

    int isGameOver;
    [SerializeField] GameObject gameOverPanel;

    //This variable can be updated to make win condition at 4096 value. UI can be manually made to 8x8 block.
    [SerializeField] int WinScore=2048;
    [SerializeField] GameObject winPanel;
    bool hasWon;

    public Color[] fillColors;

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreDisplay.text = highScore.ToString();
    }

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //This func is always the first function to be executed when game starts
    void Start()
    {
        StartSpawnFill();
        StartSpawnFill();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFill();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("up");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("down");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("left");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("right");
        }
    }


    //this function is responsible for filling the board with blocks
    public void SpawnFill()
    {
        bool isFull = true;
        for(int i = 0; i < allCells.Length; i++)
        {
            if (allCells[i].fill == null)
            {
                isFull = false;
            }

            if (isFull == true)
            {
                return;
            }
        }

        int whichSpawn = Random.Range(0, allCells.Length);
        if (allCells[whichSpawn].transform.childCount != 0)
        {
            //Debug.Log(allCells[whichSpawn].name + "Is already filled");
            SpawnFill();
            return;
        }

        float chance = Random.Range(0f, 1f);
        //Debug.Log(chance);

        if(chance<=0.8f)
        {
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
            FillBlocks tempFillComp = tempFill.GetComponent<FillBlocks>();
            allCells[whichSpawn].GetComponent<CellShift>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
        }
        else
        {
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
            FillBlocks tempFillComp = tempFill.GetComponent<FillBlocks>();
            allCells[whichSpawn].GetComponent<CellShift>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(4);
        }

    }

    //This function is called in start function above to randomly create 2 blocks with value 2 at start.
    public void StartSpawnFill()
    {
        int whichSpawn = Random.Range(0, allCells.Length);
        if (allCells[whichSpawn].transform.childCount != 0)
        {
            //Debug.Log(allCells[whichSpawn].name + "Is already filled");
            SpawnFill();
            return;
        }

        float chance = Random.Range(0f, 1f);
        //Debug.Log(chance);

        GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
        FillBlocks tempFillComp = tempFill.GetComponent<FillBlocks>();
        allCells[whichSpawn].GetComponent<CellShift>().fill = tempFillComp;
        tempFillComp.FillValueUpdate(2);

    }


    public void ScoreUpdate(int scoreIn)
    {
        myScore += scoreIn;
        UpdateHighScore();
        scoreDisplay.text = myScore.ToString();
    }

    public void UpdateHighScore()
    {
        if (myScore > highScore)
        {
            highScore = myScore;
            highScoreDisplay.text = highScore.ToString();
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void GameOverCheck()
    {
        isGameOver++;
        if (isGameOver >= 16)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void CheckWin(int maxBlockVal)
    {
        if (hasWon) return;
        if (maxBlockVal == WinScore)
        {
            winPanel.SetActive(true);
            hasWon = true;
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
