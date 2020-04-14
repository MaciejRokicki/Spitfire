using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private AppManager appManager;
    private GameObject transitionEffect;

    private GameObject borderRight;
    private GameObject borderTop;
    private GameObject borderLeft;
    private GameObject borderBot;

    private float spawnLeftX;
    private float spawnTopY;
    private float spawnRightX;
    private float spawnBotY;

    public Animator ScorePanelAnimator;
    public TextMeshProUGUI CurrentScoreText;

    public RectTransform AddedScorePrefab;

    public Transform LevelObjects;
    
    internal GameObject player;
    public GameObject PlayerPrefab;

    public GameObject EnemyPrefab;
    public int enemyCount = 0;

    public GameObject BonusScorePrefab;

    internal int score = 0;

    private void Awake()
    {
        appManager = GameObject.Find("AppManager").GetComponent<AppManager>();
        transitionEffect = GameObject.Find("TransitionEffect");

        borderRight = GameObject.Find("BorderRight");
        borderTop = GameObject.Find("BorderTop");
        borderLeft = GameObject.Find("BorderLeft");
        borderBot = GameObject.Find("BorderBot");

        Vector2 borderRightPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f));
        borderRightPosition.x += borderRight.GetComponent<SpriteRenderer>().size.x / 4;
        borderRight.transform.position = borderRightPosition;

        Vector2 borderTopPosition = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height));
        borderTopPosition.x = 0.0f;
        borderTopPosition.y += borderTop.GetComponent<SpriteRenderer>().size.y / 3;
        borderTop.transform.position = borderTopPosition;

        Vector2 borderLeftPosition = Camera.main.ScreenToWorldPoint(Vector2.zero);
        borderLeftPosition.x -= borderLeft.GetComponent<SpriteRenderer>().size.x / 4;
        borderLeft.transform.position = borderLeftPosition;

        Vector2 borderBotPosition = Camera.main.ScreenToWorldPoint(Vector2.zero);
        borderBotPosition.x = 0.0f;
        borderBotPosition.y -= borderBot.GetComponent<SpriteRenderer>().size.y / 3;
        borderBot.transform.position = borderBotPosition;

        spawnLeftX = borderLeft.transform.position.x + borderLeft.GetComponent<SpriteRenderer>().size.x / 2 + 1.0f;
        spawnTopY = borderTop.transform.position.y - borderTop.GetComponent<SpriteRenderer>().size.y / 2 - 1.0f;
        spawnRightX = borderRight.transform.position.x - borderRight.GetComponent<SpriteRenderer>().size.x / 2 - 1.0f;
        spawnBotY = borderBot.transform.position.y + borderBot.GetComponent<SpriteRenderer>().size.y / 2 + 1.0f;

    }

    private void Start()
    {
        transitionEffect.GetComponent<Animator>().SetTrigger("Hide");
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
        if (LevelObjects.childCount > 0)
        {
            ClearLevel();
            yield return new WaitForSeconds(2.0f);
        }

        SpawnPlayer();

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
        Vector3 spawnPosition = RandomSpawnVector();

        Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity).transform.SetParent(LevelObjects);
        enemyCount++;
    }

    public void SpawnBonusScore()
    {
        int rand = Random.Range(1, 11);

        if(rand <= 2)
        {
            Vector3 spawnPosition = RandomSpawnVector();

            Instantiate(BonusScorePrefab, spawnPosition, Quaternion.identity).transform.SetParent(LevelObjects);
        }
    }

    private Vector3 RandomSpawnVector()
    {
        int randomBorder = Random.Range(0, 4);

        float x = 0.0f;
        float y = 0.0f;

        switch (randomBorder)
        {
            case 0:
                x = spawnLeftX;
                y = Random.Range(-borderLeft.transform.position.y / 2, borderLeft.transform.position.y / 2);
                break;

            case 1:
                x = Random.Range(-borderTop.transform.position.x / 2, borderTop.transform.position.x / 2);
                y = spawnTopY;
                break;

            case 2:
                x = spawnRightX;
                y = Random.Range(-borderRight.transform.position.y / 2, borderRight.transform.position.y / 2);
                break;

            case 3:
                x = Random.Range(-borderBot.transform.position.x / 2, borderBot.transform.position.x / 2);
                y = spawnBotY;
                break;
        }

        return new Vector3(x, y, 0.0f);
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
            if(go.tag == "BonusScore")
            {
                go.GetComponent<BonusScore>().Die();
            }
            else
            {
                Destroy(go);
            }
        }

        enemyCount = 0;
        HighScore();
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
        RectTransform uiObj = Instantiate(AddedScorePrefab, pos, Quaternion.identity, GameObject.Find("Canvas").transform);

        uiObj.GetComponent<TextMeshProUGUI>().SetText(points.ToString());
    }

    private void HighScore()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            if(score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}