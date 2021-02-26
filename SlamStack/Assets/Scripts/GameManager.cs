using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Text scoreTxt;


    private int scorePoints;


    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else

            Destroy(gameObject);
        
    }   

     public void AddScore (int points)
    {
        scorePoints += points;
        scoreTxt.text = scorePoints.ToString();
    }

    public void ResetScore()
    {
        scorePoints = 0;
        scoreTxt.text = "0";    
    }
}

