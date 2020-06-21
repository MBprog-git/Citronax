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
    bool Changer;
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
    public Text TxtFinalTitre;
    public Text TxtFinal;
    public Text TxtScoreP1;
    public Text TxtScoreP2;

    FirstPhase FirstPhase;
    Swipo SecondPhase;
    Shaker ThirdPhase;

    bool once = true;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        FirstPhase = GetComponent<FirstPhase>();
        SecondPhase = GetComponent<Swipo>();
        ThirdPhase = GetComponent<Shaker>();

        SetInstruc();
        UpdateScore(0);
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
        
        panelInstruc.SetActive(false);

        switch (Phase)
        {
            case 1: //Activer Tape-mole
                PanelPhase1.SetActive(true);
              FirstPhase.startGame = true;
                StartCoroutine(FirstPhase.TimeFirstPhase());
                SetTimer(FirstPhase.TempsGame);
                break; 
            case 2: //Desactiver Tape-mole  -  Activer Slash
           
                PanelPhase2.SetActive(true);
                SetTimer(SecondPhase.Temps);
              SecondPhase.StartPhase2();
                break; 
            case 3:  //Desactiver Slash - Activer Shake
          
                PanelPhase3.SetActive(true);
               ThirdPhase.StartPhase3();
                SetTimer(ThirdPhase.Temps);
                break;
                    case 4: // Désactiver shake - Montrer score
                PanelPhase3.SetActive(false);
                CalculFinal();
                break;     
  
                
            
               
        }
    }
    public void ChangePlayer()
    {
        PlayerOneTurn = !PlayerOneTurn;
        if (!PlayerOneTurn)
        {
        TxtTitre.text = "TOUR 2";
        TxtInstruc.text = "Passer le téléphone au joueur2";
        panelInstruc.SetActive(true);
         
            SetTimer(TempsInstru);
        Changer = true;
            

        }

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
            if (Changer) {
                panelInstruc.SetActive(false);
                Changer = false;
                switch (Phase)
                {
                    case 1:
                        FirstPhase.startGame = true;
                        StartCoroutine(FirstPhase.TimeFirstPhase());
                        SetTimer(FirstPhase.TempsGame);
                        break;  
                    case 2: 
                        
                       SecondPhase.StartPhase2();
                        SetTimer(SecondPhase.Temps);
                        break; 
                    case 3:
                        ThirdPhase.StartPhase3();
                        SetTimer(ThirdPhase.Temps);
                        break;

                }
            }
        }
    }

    public void UpdateScore(int AddScore)
    {
        if (PlayerOneTurn)
        {
            scoreP1 += AddScore;
            TxtScore.text = "Score J1: " + scoreP1 ;
        }
        else
        {
            scoreP2 += AddScore;
            TxtScore.text = "Score J2: " + scoreP2 ;
        }
    }

    public void SetInstruc()
    {
        Phase++;
        switch (Phase)
        {
            case 1:

                TxtTitre.text = " TAPTAP!";
                TxtInstruc.text = "Tap et non fap"; 
                panelInstruc.SetActive(true);
         
                break;    
            case 2:
                PanelPhase1.SetActive(false);
                TxtTitre.text = " SlashySlash!";
                TxtInstruc.text = "Chéri ça va couper"; 
                panelInstruc.SetActive(true);
            
                break;      
            case 3:
                PanelPhase2.SetActive(false);
                TxtTitre.text = " Shaka!";
                TxtInstruc.text = "Shake your booty";
                panelInstruc.SetActive(true);
                break;
        }
        SetTimer(TempsInstru);
        once = false;
    }

    void CalculFinal()
    {
        if (scoreP1 == scoreP2)
        {
            TxtFinalTitre.text = "Egalité...";

        }
        else if (scoreP1> scoreP2)
        {
            TxtFinalTitre.text = "Victoire du joueur 1!";

        }else
        {
            TxtFinalTitre.text = "Victoire du joueur 2!";

        }
        TxtScoreP1.text = "Joueur 1 : "+scoreP1;
        TxtScoreP2.text = "Joueur 2 : "+scoreP2;
        TxtScore.gameObject.SetActive(false);
        TxtTimer.gameObject.SetActive(false);
        PanelPhaseFinal.SetActive(true);
    }
}
