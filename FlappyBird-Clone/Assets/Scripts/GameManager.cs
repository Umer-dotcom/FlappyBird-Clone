using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject birdPrefab;
    [SerializeField] GameObject pipePrefab;
    [SerializeField] GameObject scenerayPrefab;
    [SerializeField] GameObject startPage;
    [SerializeField] GameObject countDownPage;
    [SerializeField] GameObject gameOverPage;
    [SerializeField] Text finalScore_txt;
    [SerializeField] Text highScore_txt;
    [SerializeField] GameObject score_txt;
    [SerializeField] GameObject sceneToBeDestroyed;
    [SerializeField] GameObject sceneNewlyInstantiated;

    [SerializeField] float pipeScrollingSpeed = 50f;
    [SerializeField] float minPipeHeight = -1.6f;
    [SerializeField] float maxPipeHeight = 3.7f;
    [SerializeField] float xPosToSpawnPipe = 3.8f;

    [SerializeField] float currentTimer = 0.0f;
    [SerializeField] float timeToSpawnPipe = 5.0f;

    [SerializeField] float sceneryScrollingSpeed = 40f;
    [SerializeField] float xPosToDestroyScenery = -12.5f;
    [SerializeField] float xPosToCreateScenery = -6.8f;

    [SerializeField] Vector3 newScenePosition;
    [SerializeField] Vector3 oldScenePosition;

    [SerializeField] int score = 0;
    [SerializeField] int highScore = 0;

    private Text scoreText;

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScore_txt.text = "HighScore: " + highScore.ToString();
        scoreText = score_txt.GetComponent<Text>(); //Score text to update
        sceneNewlyInstantiated = Instantiate(scenerayPrefab, Vector3.zero, Quaternion.identity); //Instantiating Started Screen
        sceneToBeDestroyed = sceneNewlyInstantiated;    //At first both will point to same scenery
        sceneNewlyInstantiated.GetComponent<Scroller>().setScrollingSpeed(sceneryScrollingSpeed);   //Setting speed of scrolling
        Instantiate(birdPrefab, Vector3.zero, Quaternion.identity); //Instatiating bird at starting screen
        startPage.SetActive(true);  //Making start page appear
        Time.timeScale = 0; //Pause the time of game
        spawnPipe();
    }

    private void Update()
    {
        newScenePosition = sceneNewlyInstantiated.GetComponent<Transform>().position;
        oldScenePosition = sceneToBeDestroyed.GetComponent<Transform>().position;

        checkCreateScenery();
        checkDestroyScenery();

        //Timer
        currentTimer += Time.deltaTime;
        checkSpawnPipe();

    }

    private void checkCreateScenery()
    {
        if(newScenePosition.x <= xPosToCreateScenery)
        {
            //Create new scenery at x = 12
            createNewScenery();
        }
    }

    private void createNewScenery()
    {
        sceneToBeDestroyed = sceneNewlyInstantiated;
        sceneNewlyInstantiated = Instantiate(scenerayPrefab, new Vector2(12, 0), Quaternion.identity);
        sceneNewlyInstantiated.GetComponent<Scroller>().setScrollingSpeed(sceneryScrollingSpeed);
    }

    private void checkDestroyScenery()
    {
        if(oldScenePosition.x <= xPosToDestroyScenery)
        {
            //Destroy scenery at x = -12.5f
            Destroy(sceneToBeDestroyed);
            sceneToBeDestroyed = sceneNewlyInstantiated;
        }
    }

    private void checkSpawnPipe()
    {
        if (currentTimer >= timeToSpawnPipe)
        {
            spawnPipe();
            currentTimer = 0.0f;
        }
    }

    private void spawnPipe()
    {
        GameObject temp = Instantiate(pipePrefab, new Vector2(xPosToSpawnPipe, Random.Range(minPipeHeight, maxPipeHeight)), Quaternion.identity);
        temp.GetComponent<Scroller>().setScrollingSpeed(pipeScrollingSpeed);
        Destroy(temp, 15);
    }

    public void startGame()
    {
        startPage.SetActive(false);
        countDownPage.SetActive(true);
        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        int count = 3;
        for(int i = 0; i < count; i++)
        {
            yield return new WaitForSecondsRealtime(1);
        }

        countDownPage.SetActive(false);
        score_txt.SetActive(true);
        Time.timeScale = 1;
    }

    public void restartGame()
    {
        gameOverPage.SetActive(false);
        //startGame();
        SceneManager.LoadSceneAsync("Level01");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public void Scoring()
    {
        ++score;
        scoreText.text = score.ToString();
    }

    public void gameOver()
    {
        Time.timeScale = 0;
        score_txt.SetActive(false);
        gameOverPage.SetActive(true);
        finalScore_txt.text = "Score: " + score.ToString();
        updateHighScore();
    }

    private void updateHighScore()
    {
        if(highScore < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

}
