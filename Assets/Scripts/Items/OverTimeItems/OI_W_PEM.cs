using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OI_W_PEM : OI_WaveBase
{
    private GameObject m_wavePEM;
    public OI_W_PEM(Player player, float timeToExpire, float timeBetweenWaves, GameObject wavePEM) : base(player, timeToExpire, timeBetweenWaves)
    {
        m_wavePEM = Object.Instantiate(wavePEM, m_player.transform.position, new Quaternion(0, 0, 0, 0), m_player.transform);
        m_wavePEM.SetActive(false);
    }

    protected override void ExecuteInnerAction()
    {
        m_wavePEM.SetActive(!m_wavePEM.activeInHierarchy);
    }
    protected override void ExitAction()
    {
        Object.Destroy(m_wavePEM);
        base.ExitAction();
    }
}
