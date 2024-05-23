using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public MinigameStoneController stone;
    private ObjectPool objectPool;

    public float spawnInterval = 2.0f;
    public Transform[] spawnPoints;

    public RawImage stoneLifeImage;
    private RawImage[] stoneLifeImages;
    public GameObject lifeTransform;
    private int oldLife;

    private bool isGameOver;
    public GameObject endPanel;
    private float timer;

    void Start()
    {
        objectPool = this.GetComponent<ObjectPool>();
        GameManager.Instance._minigame = this.GetComponent<MinigameManager>();
        stone = this.GetComponent<MinigameStoneController>();
        
        setData();
    }

    void Update()
    {
        if(!isGameOver)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnObstacle();
                timer = 0;
            }
        }else{
            endPanel.SetActive(true);
        }
    }

    private void SpawnObstacle()
    {
        GameObject obstacle = objectPool.GetPooledObject();
        if (obstacle != null)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            obstacle.transform.position = spawnPoint.position;
            // obstacle.transform.rotation = spawnPoint.rotation;
            obstacle.SetActive(true);
        }
    }

    private void setData()
    {
        if(GameManager.Stone.growingStone.GetScientificName() == "LimeStone")
            stone.life = 3;
        else if(GameManager.Stone.growingStone.GetScientificName() == "Granite")
            stone.life = 5;
        
        stoneLifeImages = new RawImage[stone.life];
        for(int i = 0; i < stone.life; i++) {
            stoneLifeImages[i] = Instantiate(stoneLifeImage);
            stoneLifeImages[i].transform.SetParent(lifeTransform.transform);
            stoneLifeImages[i].rectTransform.localPosition = new Vector3(-500 + (50 * i), -870, 0);
        }

        oldLife = stone.life;
    }

    public void restartBtn()
    {

    }
}