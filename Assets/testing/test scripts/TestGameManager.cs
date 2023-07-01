using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TestGameManager : MonoBehaviour
{
    private int testFlagNum;
    private int testEnemyNum = 1;
    private int testScore = 1;
    public bool testIsGameOver;
    public GameObject gameOver;
    public TextMeshProUGUI testFlagCount;
    public TextMeshProUGUI testEnemyCount;
    public TextMeshProUGUI testScoreText;

    // Start is called before the first frame update
    void Start()
    {
        testIsGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (testIsGameOver == true)
        {
            gameOver.SetActive(true);
        }
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("Test Game");
    }

    public void TestUpdateFlagCount(int falgsToAdd)
    {
        testFlagNum += falgsToAdd;
        testFlagCount.text = "Flags: " + testFlagNum;
        TestUpdateScore(1);
    }

    public void TestUpdateEnemyCount(int enemysToAdd)
    {
        testEnemyNum += enemysToAdd;
        testEnemyCount.text = "Enemys: " + testEnemyNum;
        TestUpdateScore(1);
    }

    public void TestUpdateScore(int scoreToAdd)
    {
        testScore += scoreToAdd;
        testScoreText.text = "Score: " + testScore;
    }
}
// add cop per # of flags
// add cop count + flag count
//add a score (cop count * flag count)