using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    private GameSettings gameSettings;
    public AudioSource music;
    public AudioSource pairingFX;
    public AudioSource yarrr;

    public Sprite piratehat;

    [SerializeField]
    private List<AudioClip> loadAudio;

    public GameObject danceFloorSprite;
    public GameObject[] speakers;

    [SerializeField]
    int musicPlaying;

    private void Awake()
    {
        List<int> musicSelected = new List<int>();
        gameSettings = FindObjectOfType<GameSettings>();
        for(int i=0; i <gameSettings.playerSettings.Length; i++)
        {
            if(!gameSettings.playerSettings[i].connected)
            {
                break;
            }
            musicSelected.Add(gameSettings.playerSettings[i].musicSelected);
        }
        int randomSong = Random.Range(0, 12);
        while(true)
        {
            if(musicSelected.Contains(randomSong))
            {
                randomSong = randomSong == 11 ? 0 : randomSong + 1;
            }
            else
            {
                musicSelected.Add(randomSong);
                break;
            }
        }

        foreach(int songID in musicSelected)
        {
            loadAudio.Add(Resources.Load<AudioClip>("Sounds/DEFAULTSONG" + (songID + 1).ToString()));
        }
        music.clip = loadAudio[loadAudio.Count - 1];
        music.Play();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < loadAudio.Count; i++)
            {
                if (i != loadAudio.Count - 1)
                {
                    loadAudio[i] = Resources.Load<AudioClip>("Pirate/PIRATE" + (i+1));
                }
                else
                {
                    loadAudio[i] = Resources.Load<AudioClip>("Pirate/PIRATE5");
                }
            }

            foreach(PlayerController playerController in FindObjectsOfType<PlayerController>())
            {
                GameObject playerPirateHat = Instantiate(new GameObject(), playerController.transform);
                playerPirateHat.AddComponent<SpriteRenderer>().sprite = piratehat;
                playerPirateHat.transform.localPosition = new Vector3(0, 0.38f, -1f);
            }
            foreach (AiBehaviour aiController in FindObjectsOfType<AiBehaviour>())
            {
                GameObject playerPirateHat = Instantiate(new GameObject(), aiController.transform);
                playerPirateHat.AddComponent<SpriteRenderer>().sprite = piratehat;
                playerPirateHat.transform.localPosition = new Vector3(0, 0.38f, -3f);
            }

            yarrr.Play();
            music.clip = loadAudio[musicPlaying == -1 ? loadAudio.Count-1 : musicPlaying];
            music.Play();
        }
    }

    public void ChangeMusic(int playerNumber)
    {
        if (playerNumber != musicPlaying)
        {
            SpriteRenderer danceFloorRenderer = danceFloorSprite.GetComponent<SpriteRenderer>();
            if (playerNumber < 0)
            {
                music.clip = loadAudio[loadAudio.Count - 1];
            }
            else
            {
                pairingFX.Play();
                musicPlaying = gameSettings.playerSettings[playerNumber].musicSelected;
                danceFloorRenderer.color = gameSettings.playerSettings[playerNumber].playerColor;

                for (int i = 0; i < speakers.Length; i++)
                {
                    ParticleSystem speakersRenderer = speakers[i].GetComponent<ParticleSystem>();
                    var main = speakersRenderer.main;
                    main.startColor = gameSettings.playerSettings[playerNumber].playerColor;
                }

                music.clip = loadAudio[playerNumber];
            }
            music.Stop();

            music.Play();
        }
    }

    public int MusicPlaying()
    {
        return musicPlaying;
    }
}
