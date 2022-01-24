using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class PlayerScript : MonoBehaviour
{
    public float timeLeft = 10.0f;
    public Text TimeText;
    public Text ScoreText;
    public Text WinText;
    private int Score;
    bool restart = false;
    bool musicPlay = false;
    public AudioClip WinMusic;
    public AudioClip LoseMusic;
    public AudioClip Gameplay;
    AudioSource audioSource;

    public ParticleSystem ShotPrefab;
    void Start()
    {
        Score = 0;
        SetCountText();
        WinText.text = "";
        audioSource = GetComponent<AudioSource>();
        PlaySound(Gameplay);

    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ParticleSystem ShotEffect = Instantiate(ShotPrefab, new Vector2(0.24f, -2.06f), Quaternion.identity);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider !=null) 
                {
                    Destroy(hit.collider.gameObject);
                    Score+=1;
                    SetCountText();
                }
        }
        if (timeLeft>=0.0f)
        {
            timeLeft -=Time.deltaTime;   
        }
        WinLose();
        TimeText.text = "Time left: " + (timeLeft).ToString("0");

        if (Input.GetKey(KeyCode.R))
        {
            if (restart==true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                restart = false;
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

     void SetCountText()
     {
        ScoreText.text = "Zombies Killed: " + Score.ToString();
     }

     void WinLose()
     {    
        if ((timeLeft <= 0.1f) && (Score!=10))
        {
            WinText.text = "Zombies got too close to you. You lost. Press R to restart or Esc to exit.";
            restart = true;
            if (musicPlay==false)
            {
                audioSource.Stop();
                PlaySound(LoseMusic);
                musicPlay=true;
            }
        }

        if (Score == 10)
        {
            WinText.text = "You survived this night. Great Job! Press R to restart or Esc to exit.";
            restart = true;
            if (musicPlay==false)
            {
                audioSource.Stop();
                PlaySound(WinMusic);
                musicPlay = true;
            }
        }
     }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}