using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        DataManager.Instance.SetName(nameTxt.text.ToString());
        SceneManager.LoadScene(1);
    }
    public void Quit() 
    {
        // save data on quit
        DataManager.Instance.Save();
        //
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
