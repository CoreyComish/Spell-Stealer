using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemiesRemaining : MonoBehaviour
{
    private int eRem;
    private GameProgression gp;
    Text text;
    public string sceneName;

    void Awake()
    {
        text = GetComponent<Text>();
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        eRem = enemies.Length;
        text.text = "Enemies Remaining: " + eRem;
        if (eRem == 0)
        {
            print(sceneName);
            gp.LevelComplete(sceneName);
        }
    }
}
