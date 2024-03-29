using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource music;
    public bool startPlaying;
    public BeatScroller beatScroller;
    public static GameManager instance;
    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;
    
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    
    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;
    
    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;
    
    void Start()
    {
        instance = this;
        
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        
        totalNotes = FindObjectsOfType<NoteObject>().Length; 
    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                beatScroller.hasStarted = true;
                
                music.Play();
            }
        }
        else
        {
            if(!music.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);
                
                normalsText.text = normalHits.ToString();
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = missedHits.ToString();
                
                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;
                
                percentHitText.text = percentHit.ToString("F1") + "%";
                
                string rankVal = "F";
                rankText.color = Color.red;
                
                if (percentHit > 40)
                {
                    rankVal = "D";
                    rankText.color = Color.red;
                    if (percentHit > 55)
                    {
                        rankVal = "C";
                        rankText.color = new Color(1.0f, 0.64f, 0.0f);;
                        if (percentHit > 70)
                        {
                            rankVal = "B";
                            rankText.color = Color.yellow;
                            if (percentHit > 85)
                            {
                                rankVal = "A";
                                rankText.color = Color.cyan;
                                if (percentHit > 95)
                                {
                                    rankVal = "S";
                                    rankText.color = Color.green;
                                }
                            }
                        }
                    }
                }
                
                rankText.text = rankVal;
                
                finalScoreText.text = currentScore.ToString();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
        
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        
        multiText.text = "Multiplier: x" + currentMultiplier;
        
        // currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }
    
    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
    }
    
    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        
        goodHits++;
    }
    
    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        
        perfectHits++;
    }
    
    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
        
        missedHits++;
    }
}
