using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

 public static GameManager instance;

   public bool PlayerOneTurn =true;

    public int Phase;
    public float TempsInstru;
    
    float timer;
   public  int scoreP1;
  public int scoreP2;
    [Space]
    [Header("Banchement")]
    public Camera cam;
    public Text TxtTimer;
    public Text TxtScore;
    public GameObject PanelPhase1;
    public GameObject PanelPhase2;
    public GameObject PanelPhase3;
    public GameObject PanelPhaseFinal;
    public GameObject panelInstruc;
    public Text TxtTitre;
    public Text TxtInstruc;

    bool once = true;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        SetInstruc();
    }
    void Update()
    {
        UpdateTimer();   

    }
    public void doExitGame()
    {
        Application.Quit();
    }
    public void MyLoadScene(string nameScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nameScene);
     
    }

    public void ChangePhase()
    {
        Phase++;
        panelInstruc.SetActive(false);

        switch (Phase)
        {
            case 1: //Activer Tape-mole
                PanelPhase1.SetActive(true);
                break; 
            case 2: //Desactiver Tape-mole  -  Activer Slash
           
                PanelPhase2.SetActive(true);
                this.gameObject.GetComponent<Swipo>().StartPhase2();
                SetTimer(this.gameObject.GetComponent<Swipo>().Temps);
                break; 
            case 3:  //Desactiver Slash - Activer Shake
          
                PanelPhase3.SetActive(true);
                this.gameObject.GetComponent<Shaker>().StartPhase3();
                SetTimer(this.gameObject.GetComponent<Shaker>().Temps);
                break;
                    case 4: // Désactiver shake - Montrer score
                PanelPhase3.SetActive(false);
                PanelPhaseFinal.SetActive(true);
                break;     
  
                
            
               
        }
    }
    public void ChangePlayer()
    {
        PlayerOneTurn = !PlayerOneTurn;
        UpdateScore(0);
    }

    public void SetTimer(float Temps)
    {
        timer = Temps;
        TxtTimer.text = timer + " s";
    }

    public void UpdateTimer()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
        TxtTimer.text = (int)timer + " s";
        }
        else
        {
        TxtTimer.text = "0 s";
            if (!once)
            {
                ChangePhase();
                once = true;
            }
        }
    }

    public void UpdateScore(int AddScore)
    {
        if (PlayerOneTurn)
        {
            scoreP1 += AddScore;
            TxtScore.text = "Score: " + scoreP1;
        }
        else
        {
            scoreP2 += AddScore;
            TxtScore.text = "Score: " + scoreP2;
        }
    }

    public void SetInstruc()
    {
        switch (Phase)
        {
            case 0:
                //Set instru taptap
                TxtTitre.text = " TAPTAP!";
                TxtInstruc.text = "Tap et non fap";
                panelInstruc.SetActive(true);
         
                break;    
            case 1:
                PanelPhase1.SetActive(false);
                //Set instru SwipySwipe
                panelInstruc.SetActive(true);
            
                break;      
            case 2:
                PanelPhase2.SetActive(false);
                //Set instru Shakinator
                panelInstruc.SetActive(true);
                break;
        }
        SetTimer(TempsInstru);
        once = false;
    }
}
