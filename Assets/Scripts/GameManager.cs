using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Animator NewGameCounterBackgroundAnimator;
    public TextMeshProUGUI CounterNewGameStart;
    public TextMeshProUGUI CurrentScoreText;
    public TextMeshProUGUI AddedScoreText;

    public Transform LevelObjects;
    public GameObject PlayerPrefab;
    internal GameObject player;
    public GameObject EnemyPrefab;
    public int enemyCount = 0;

    internal int score = 0;

    private void Awake()
    {

    }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        StartCoroutine(StartGameCourtine());
    }

    private IEnumerator StartGameCourtine()
    {
        if (LevelObjects.childCount > 0)
        {
            ClearLevel();
            yield return new WaitForSeconds(2.0f);
        }

        NewGameCounterBackgroundAnimator.SetTrigger("newGame");

        SpawnPlayer();

        for(int i = 3; i >= 1; i--)
        {
            CounterNewGameStart.SetText(i.ToString());

            if(i == 1)
                NewGameCounterBackgroundAnimator.SetTrigger("hide");

            yield return new WaitForSeconds(1.0f);
        }

        PlayerMovement();
        StartCoroutine(EnemySpawner());
    }

    private IEnumerator EnemySpawner()
    {
        yield return new WaitForSeconds(1.0f);

        while(true)
        {
            if(player == null)
                yield break;

            if (enemyCount + 1 <= 5)
                SpawnEnemy();

            yield return new WaitForSeconds(2.0f);
        }
    }

    private void SpawnPlayer()
    {
        player = Instantiate(PlayerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        player.transform.SetParent(LevelObjects);
        player.GetComponent<PlayerController>().enabled = false;
    }

    private void PlayerMovement()
    {
        player.GetComponent<PlayerController>().enabled = true;
    }

    private void SpawnEnemy()
    {
        int randomBorder = Random.Range(0, 4);

        Vector3 spawnPosition;
        float x = 0.0f;
        float y = 0.0f;

        switch(randomBorder)
        {
            case 0:
                x = -25.0f;
                y = Random.Range(-13.5f, 13.5f);
                break;

            case 1:
                x = Random.Range(-25.0f, 25.0f);
                y = 13.5f;
                break;

            case 2:
                x = 25.0f;
                y = Random.Range(-13.5f, 13.5f);
                break;

            case 3:
                x = Random.Range(-25.0f, 25.0f);
                y = -13.5f;
                break;
        }

        spawnPosition = new Vector3(x, y, 0.0f);

        Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity).transform.SetParent(LevelObjects);
        enemyCount++;
    }

    private void ClearLevel()
    {
        for(int i = 0; i < LevelObjects.childCount; i++)
        {
            GameObject go = LevelObjects.GetChild(i).gameObject;

            if(go.tag == "Enemy")
            {
                go.GetComponent<Enemy>().Die();
            }
            else
            {
                Destroy(go);
            }
        }

        enemyCount = 0;
        score = 0;
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
        CurrentScoreText.SetText(this.score.ToString());
        AddedScoreText.SetText(score.ToString());
        GameObject.Find("ScorePanel").GetComponent<Animator>().SetTrigger("showScoreAnimation");
    }
}