using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform LevelObjects;
    public GameObject PlayerPrefab;
    internal GameObject player;

    public GameObject EnemyPrefab;
    public int enemyCount = 0;

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
        ClearLevel();
        SpawnPlayer();

        for(int i = 3; i >= 1; i--)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1.0f);
        }

        PlayerMovement();
        StartCoroutine(EnemySpawner());
    }

    private IEnumerator EnemySpawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);

            if(player == null)
                yield break;

            if(enemyCount + 1 <= 5)
                SpawnEnemy();
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
            Destroy(LevelObjects.GetChild(i).gameObject);
        }
    }
}