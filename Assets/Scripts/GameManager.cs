﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float distance = -3.2f;
    public float cameraDistance = 10.0f;
    public float[] range = new float[] { 0.4f, 0.6f };
    public GameObject yuki_1;
    public GameObject yuki_2;
    public GameObject yuki_3;
    public GameObject yuki_4;
    public Vector3[] cameraViewportPos;
    // public Vector3[] cameraViewportWorldPos;
    public Dictionary<string, Vector3> cameraViewportWorldPos;
    private List<GameObject> yukis;
    private bool isGameEnd;
    public float score;
    public Text scoreText;
    public Text gameoverScoreText;
    public GameObject gameoverPanel;

    // Use this for initialization
    void Start()
    {
        score = 0;
        yukis = new List<GameObject>();
        if (null != yuki_1) yukis.Add(yuki_1);
        if (null != yuki_2) yukis.Add(yuki_2);
        if (null != yuki_3) yukis.Add(yuki_3);
        if (null != yuki_4) yukis.Add(yuki_4);
        cameraViewportPos = new Vector3[4] 
        {
            new Vector3(1.0f,1.0f,cameraDistance),
            new Vector3(0f,1.0f,cameraDistance),
            new Vector3(0f,0f,cameraDistance),
            new Vector3(1.0f,0f,cameraDistance)
        };
        cameraViewportWorldPos = new Dictionary<string, Vector3>()
        {
            { "tr", Camera.main.ViewportToWorldPoint(new Vector3(1.0f,1.0f,cameraDistance)) },
            { "tl", Camera.main.ViewportToWorldPoint(new Vector3(0f,1.0f,cameraDistance)) },
            { "bl", Camera.main.ViewportToWorldPoint(new Vector3(0f,0f,cameraDistance)) },
            { "br", Camera.main.ViewportToWorldPoint(new Vector3(1.0f,0f,cameraDistance)) }
        };
        InstantiateYuki();
    }

    public void End()
    {
        isGameEnd = true;
        gameoverScoreText.text = score.ToString("0");
        gameoverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void InstantiateYuki()
    {
        float ry = Random.Range(range[0], range[1]);
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(1.2f,ry, 10f));
        Vector3 posReverse = new Vector3(pos.x, pos.y + distance, pos.z);
        GameObject yukiAbove = Instantiate(RandomYuki(), pos, Quaternion.identity);
        GameObject yukiBelow = Instantiate(RandomYuki(), posReverse, Quaternion.identity);

        yukiAbove.tag = "yukiAbove";
        yukiBelow.tag = "yukiBelow";
        yukiBelow.transform.Rotate(new Vector3(0, 0, 180.0f));
    }

    // Get a random yuki in yuki's list
    GameObject RandomYuki()
    {
        int index = Mathf.RoundToInt(Random.Range(0, 3));
        return yukis[index];
    }

    public void SetScore(float score)
    {
        this.score = score;
        scoreText.text = score.ToString("0");
    }

    public float GetScore()
    {
        return score;
    }
    public void SetIsGameEnd(bool isGameEnd)
    {
        this.isGameEnd = isGameEnd;
    }

    public bool GetIsGameEnd()
    {
        return isGameEnd;
    }

}
