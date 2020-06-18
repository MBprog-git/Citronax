using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

 public static GameManager instance;
    public Camera cam;

   public bool PlayerOneTurn =true;

    public int Phase;

    public Text TxtTimer;
    float timer;
    public Text TxtScore;
   public  int scoreP1;
  public int scoreP2;
    public GameObject PanelPhase1;
    public GameObject PanelPhase2;
    public GameObject PanelPhase3;
    public GameObject PanelPhase4;
    public GameObject PanelPhaseFinal;

    void Awake()
    {
        if (instance == null)
            instance = this;
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
        switch (Phase)
        {
            case 1: //Activer Tape-mole
                PanelPhase1.SetActive(true);
                break; 
            case 2: //Desactiver Tape-mole  -  Activer Slash
                PanelPhase1.SetActive(false);
                PanelPhase2.SetActive(true);
                this.gameObject.GetComponent<Swipo>().StartPhase2();
                SetTimer(this.gameObject.GetComponent<Swipo>().Temps);
                break; 
            case 3:  //Desactiver Slash - Activer Shake
                PanelPhase2.SetActive(false);
                PanelPhase3.SetActive(true);
                this.gameObject.GetComponent<Shaker>().StartPhase3();
                SetTimer(this.gameObject.GetComponent<Shaker>().Temps);
                break;
                    case 4: // Désactiver shake - Montrer score
                PanelPhase3.SetActive(false);
                PanelPhase4.SetActive(true);
                break;     
            case 5: //Changer player - Retour phase 0 / Final score
                
                PanelPhase4.SetActive(false);
                if(PlayerOneTurn == false)
                {
                    
                    PanelPhaseFinal.SetActive(true);
                }
                else
                {
                    ChangePlayer();
                    Phase = 0;
                
                    ChangePhase();
                }
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
        TxtTimer.text = timer + " s";
        }
        else
        {
        TxtTimer.text = "0 s";

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
}
