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

    public Text TxtScore;
   public  int scoreP1;
  public int scoreP2;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
                break; 
            case 2: //Desactiver Tape-mole  -  Activer Slash
                break; 
            case 3:  //Desactiver Slash - Activer Shake
                break;
                    case 4: // Désactiver shake - Montrer score
                break;     
            case 5: //Changer player - Retour phase 0 / Final score
                if(PlayerOneTurn == false)
                {
                    //ScoreFinal
                }
                else
                {
                    ChangePlayer();
                    Phase = 0;
                    //Desactiver score
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
