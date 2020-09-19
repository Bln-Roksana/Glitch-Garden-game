using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    AudioSource[] musicThemes;
    AudioSource winMusicTheme;
    AudioSource loseMusicTheme;

    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseScreen;
    int numberOfAttackers = 0;
    bool levelTimerFinished = false;
    float waitToLoad=8;

    private void Start()
    {
        winLabel.SetActive(false);
        loseScreen.SetActive(false);
        musicThemes = GetComponents<AudioSource>();
        winMusicTheme = musicThemes[0];
        loseMusicTheme = musicThemes[1];

    }

    public void AttackerSpawned()
    {
        numberOfAttackers++;
    }

    public void AttackerKilled()
    {
        numberOfAttackers--;
        if(numberOfAttackers<=0 && levelTimerFinished)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        winMusicTheme.Play();
        //GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(waitToLoad);
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }

    public void HandleLooseCondition()
    {
        loseScreen.SetActive(true);
        loseMusicTheme.Play();
        Time.timeScale = 0;
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawnerArray = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawnerArray)
        {
            spawner.StopSpawning(); //calling this method in each AttackerSpawner script
        }
    }


    /*bool timeReachedZero = false;
    bool noAttackersLeft=false;
    int attackersLeft;

    GameTimer timerScript;

    // Start is called before the first frame update
    void Start()
    {
        timerScript = FindObjectOfType<GameTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerScript.timeFinished)
        {
            timeReachedZero = true;
            attackersLeft = FindObjectsOfType<Attacker>().Length;
            Debug.Log("attackersLeft: " + attackersLeft);

            if (attackersLeft == 0)
            {
                noAttackersLeft = true;
            }
        }

        if (timeReachedZero && noAttackersLeft)
        {
            Debug.Log("End Level Now");
        }
    }*/
}
