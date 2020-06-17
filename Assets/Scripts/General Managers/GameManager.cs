using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public delegate void OnItem(Player player);
    public event OnItem OnItemTaken;

    public delegate void GameEvents(int waveIndex);
    public event GameEvents OnStartWave;
    public event GameEvents OnCooldownWave;
    public event GameEvents OnEndWave;

    private int waveIndex = 0;

    public GameObject Nexus;

    [HideInInspector] public List<Player> players = new List<Player>();

    void Start()
    {
        FindPlayers();
        OnCooldownWave?.Invoke(waveIndex);
    }

    public void FindPlayers()
    {
        Player[] foundPlayers = FindObjectsOfType<Player>();
        for (int i = 0; i < foundPlayers.Length; i++)
        {
            players.Add(foundPlayers[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnItemTake(Player player)
    {
        OnItemTaken(player);
    }


    //private float cdAux = 5;
    //private void StartCooldown()
    //{
    //    ((AuxTimer)AuxTimer.Instance).AuxUpdate += Cooldown;
    //}
    //private void Cooldown(float delta)
    //{
    //    cdAux -= delta;

    //    if (cdAux < 0)
    //    {
    //        OnStartWave(waveIndex);
    //    }
    //}
}