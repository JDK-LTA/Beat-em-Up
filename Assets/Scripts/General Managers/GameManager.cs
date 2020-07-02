using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public delegate void OnItem(Player player);
    public event OnItem OnItemTaken;

    public delegate void GameEvents(int waveIndex);
    public event GameEvents OnStartWave;
    public event GameEvents OnCooldownWave;
    public event GameEvents OnEndWave;

    private int waveIndex = 0;

    private GameObject nexus;
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Text waveText;

    private bool gameEnded = false;

    [HideInInspector] public List<Player> players = new List<Player>();

    public GameObject Nexus { get => nexus; set => nexus = value; }
    public Text WaveText { get => waveText; set => waveText = value; }

    void Start()
    {
        FindPlayers();
        OnCooldownWave?.Invoke(waveIndex);

        nexus = FindObjectOfType<Nexus>().gameObject;

        InputManager.Instance.OnEscape += ToggleMenu;

    }

    public void FindPlayers()
    {
        Player[] foundPlayers = FindObjectsOfType<Player>();
        for (int i = 0; i < foundPlayers.Length; i++)
        {
            players.Add(foundPlayers[i]);
        }
    }
    private void OnItemTake(Player player)
    {
        OnItemTaken(player);
    }
    public void ToggleMenu()
    {
        if (!gameEnded)
        {
            Time.timeScale = !canvasMenu.activeInHierarchy ? 0 : 1;
            canvasMenu.SetActive(!canvasMenu.activeInHierarchy);
        }
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void EndGame(bool win)
    {
        gameEnded = true;
        Time.timeScale = 0;

        GameObject toBeSet = win ? winPanel : losePanel;
        toBeSet.SetActive(true);
    }

    public void UpdateWaveText()
    {
        waveText.text = "WAVE " + (WaveManager2.Instance.CurrentWave + 1) + "/" + WaveManager2.Instance.Waves.Count;
    }

}