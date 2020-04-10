using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TransitionEffect transitionEffect;

    public Animator ScorePanelAnimator;
    public TextMeshProUGUI CurrentScoreText;

    public RectTransform AddedScorePrefab;

    public Transform LevelObjects;
    public GameObject PlayerPrefab;
    internal GameObject player;
    public GameObject EnemyPrefab;
    public int enemyCount = 0;

    internal int score = 0;

    private void Awake()
    {
        GameObject.Find("TransitionPanel").GetComponent<Image>().material.SetFloat("_Scale", 0.4f);
    }

    private void Start()
    {
        GameObject.Find("TransitionPanel").GetComponent<Image>().material.SetFloat("_Fade", 1.0f);
        NewGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void NewGame()
    {
        StartCoroutine(StartGameCourtine());
    }

    private IEnumerator StartGameCourtine()
    {
        StartCoroutine(transitionEffect.ShowEffect());

        if (LevelObjects.childCount > 0)
        {
            ClearLevel();
            yield return new WaitForSeconds(2.0f);
        }

        SpawnPlayer();

        StartCoroutine(transitionEffect.HideEffect());
        yield return new WaitForSeconds(2.0f);

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

        if(!ScorePanelAnimator.GetBool("showScoreAnimation"))
        {
            ScorePanelAnimator.ResetTrigger("showScoreAnimation");
            ScorePanelAnimator.SetTrigger("showScoreAnimation");
        }
    }

    public void SpawnAddedScore(Vector3 pos, int points)
    {
        RectTransform uiObj = Instantiate(AddedScorePrefab, Camera.main.WorldToScreenPoint(pos), Quaternion.identity, GameObject.Find("Canvas").transform);

        uiObj.GetComponent<TextMeshProUGUI>().SetText(points.ToString());
    }
}