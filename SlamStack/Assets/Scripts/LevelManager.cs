using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; 

    [SerializeField] private GameObject platform ;
    [SerializeField] private float distanceBetweenPlatforms = 0.35f;
    [SerializeField] private Transform centrePillar;
    [SerializeField] private Transform finishLine;
    private int levelMinLength = 40;
    private int levelMaxLength = 60;
    [SerializeField] private Material normalMat;
    [SerializeField] private Material damageMat;
    [SerializeField] private AudioClip winAC; 
   
    
    

    [Header("UI")]
    [SerializeField] private GameObject lostUI;
    [SerializeField] private GameObject wontUI;

   
    public Material planeMate;
    public Material baseMate;
    public MeshRenderer cylinder;
    public MeshRenderer playerMesh;

    private AudioSource myAs; 
    
   
   
    private void Awake()
    {
      
        Instance = this;
        myAs = GetComponent<AudioSource>();
        myAs.clip = winAC;
       
    }

    void Start()
    {
       
        Initialize();
    }   

    private void Update()
    {
        if (Input.GetMouseButtonDown(0 ))
        {
            planeMate.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
           
            baseMate.color = planeMate.color + Color.gray;
            playerMesh.material.color = planeMate.color;
            cylinder.material.color = planeMate.color;

        }
    }

    void Initialize()
    {
        float levelLenght = Random.Range(levelMinLength, levelMaxLength);

        int numberOfPlatforms = Mathf.CeilToInt (levelLenght / distanceBetweenPlatforms);

        levelLenght = numberOfPlatforms * distanceBetweenPlatforms;

        centrePillar.localScale = new Vector3(1, levelLenght + 1, 1);

        finishLine.position = new Vector3(0, -levelLenght, 0);

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            GameObject p = Instantiate(platform, new Vector3(0, -distanceBetweenPlatforms * i, 0), Quaternion.identity);
            Platforms platformScript = p.GetComponent<Platforms>();

            List<int> allTilesIndencies = new List<int>(5) { 0, 1, 2, 3, 4, 5};
            List<int> damageTileIndencies = new List<int>();

            int damageTilesCount = Random.Range(0, 4);

            for (int j = 0; j < damageTilesCount; j++)
            {
                int randomIndex = Random.Range(0, allTilesIndencies.Count);
                damageTileIndencies.Add(allTilesIndencies[randomIndex]);

                allTilesIndencies.RemoveAt(randomIndex);

            }
            platformScript.Initialize(damageTileIndencies, normalMat, damageMat);
        }
    }

    public void Lost()
    {
        lostUI.SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        GameManager.Instance.ResetScore();
    }
    public void Won()
    {
        wontUI.SetActive(true);
        myAs.Play();

    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
    }
}
