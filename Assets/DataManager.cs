using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string highScoreName;
    public string currentName;
    public int highScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Load();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public class SaveData
    {
        public string name = "";
        public int highScore = 0;
    }
    public void Save()
    {
        SaveData data= new SaveData();
        data.name = highScoreName;
        data.highScore = highScore;

        string jsonData=JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", jsonData);
    }
    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data= JsonUtility.FromJson<SaveData>(json);
            highScoreName = data.name;
            highScore = data.highScore;
        }
    }
    public void UpdateHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            highScoreName = currentName;
        }
    }

    public void SetName(string nameTxt)
    {
        Debug.Log(nameTxt);
        Debug.Log(nameTxt.Length.ToString());
        if (nameTxt.Length<=1)
        {
            currentName = "Anonymous";
            return;
        }
        currentName = nameTxt;
    } 

}
