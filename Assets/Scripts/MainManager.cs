using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public Text scoreText;
    private string m_name;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        UpdateUIText();
    }
    public void Awake()
    {

        if (DataManager.Instance.currentName.Length <= 1)
        {
            m_name = "Anonymous";
        }
        else
        {
            m_name = DataManager.Instance.currentName;
        }
        //Debug.Log(m_name);
        ScoreText.text = $"Score : {m_name} : {m_Points}";
    }
    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        // Debug.Log(DataManager.Instance.currentName);
        // Debug.Log(DataManager.Instance.currentName.Length.ToString());
        //m_name = (DataManager.Instance.currentName.Length <= 1) ? DataManager.Instance.currentName : "Anonymous";
        ScoreText.text = $"Score : {m_name} : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        DataManager.Instance.UpdateHighScore(m_Points);
        UpdateUIText();
        GameOverText.SetActive(true);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void UpdateUIText()
    {
        string name = DataManager.Instance.highScoreName.Length<=1 ? DataManager.Instance.highScoreName : "Anonymous";
        scoreText.text = "Best Score: " + DataManager.Instance.highScoreName + " : " + DataManager.Instance.highScore.ToString();
    }
}
