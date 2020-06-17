using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OI_WaveBase : OverTimeItem
{
    private float counter;
    protected float m_timeBetweenWaves;

    public OI_WaveBase(Player player, float timeToExpire, float timeBetweenWaves) : base(player, timeToExpire)
    {
        m_timeBetweenWaves = timeBetweenWaves;
    }

    protected override void EnterAction()
    {
        base.EnterAction();
        ExecuteInnerAction();
    }
    protected override void ExecuteAction(float delta)
    {
        counter += delta;
        if (counter > m_timeBetweenWaves)
        {
            ExecuteInnerAction();
            counter = 0;
        }
    }

    protected virtual void ExecuteInnerAction()
    {
    }
}
