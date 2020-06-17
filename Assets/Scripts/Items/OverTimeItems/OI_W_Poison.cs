using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OI_W_Poison : OI_WaveBase
{
    private float m_DamagePerWave;
    public OI_W_Poison(Player player, float timeToExpire, float timeBetweenWaves, float DamagePerWave) : base(player, timeToExpire, timeBetweenWaves)
    {
        m_DamagePerWave = DamagePerWave;
    }

    protected override void ExecuteInnerAction()
    {
        m_player.HpCurrent -= m_DamagePerWave;
    }
}
