using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] List<ShooterController> shootersList;
    [SerializeField] List<ChaserController> chasersList;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GameObject player;
    [SerializeField] GameObject finalScene;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text respawnText;
    [SerializeField] TMP_Text finalText;

    public float timer;
    public float totalScore;
    public float respawnTimer;
    float timerAux;

    public bool isOver;

    public Values timers;

    void Start()
    {
        timer = timers.timerValue;
        respawnTimer = timers.respawnValue;

        Time.timeScale = 1;

        totalScore = 0;
        timerAux = respawnTimer;

        for (int i = 0; i < 2; i++)
        {
            shootersList[i].gameObject.SetActive(true);
            chasersList[i].gameObject.SetActive(true);

            shootersList[i].transform.position = spawnPoints[Random.Range(0, 3)].position;
            chasersList[i].transform.position = spawnPoints[Random.Range(0, 3)].position;
        }
    }

    void Update()
    {
        if (!isOver)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                respawnTimer -= Time.deltaTime;
                UpdateTimer(timer);
                UpdateRespawnTimer(respawnTimer);
            }
            else
            {
                timer = 0;
                isOver = true;
            }

            if (player.GetComponent<HealthController>().currentHealth <= 0)
            {
                isOver = true;
            }
        }
        else
        {
            Time.timeScale = 0;
            finalScene.SetActive(true);
            finalText.text = totalScore.ToString();
        }

        if (respawnTimer <= 0)
        {
            SpawnEnemies();
            respawnTimer = timerAux;
        }
    }

    public void Score(int score)
    {
        totalScore += score;
        scoreText.text = totalScore.ToString();
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        int seconds = (int)currentTime;

        timerText.text = seconds.ToString();
    }

    void UpdateRespawnTimer(float currentTime)
    {
        currentTime += 1;
        int seconds = (int)currentTime;

        respawnText.text = seconds.ToString();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < shootersList.Count; i++)
        {
            if (!shootersList[i].gameObject.activeInHierarchy)
            {
                shootersList[i].gameObject.SetActive(true);
                shootersList[i].transform.position = spawnPoints[Random.Range(0, 3)].position;
            }

            if (!chasersList[i].gameObject.activeInHierarchy)
            {
                chasersList[i].gameObject.SetActive(true);
                chasersList[i].transform.position = spawnPoints[Random.Range(0, 3)].position;
            }
        }
    }
}
