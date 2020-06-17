using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTimeItem : ItemParent
{
    protected float m_timeToExpire;
    private float counter;
    protected bool enterActionDone = false;

    public OverTimeItem() { }
    public OverTimeItem (Player player, float timeToExpire)
    {
        m_player = player;
        m_timeToExpire = timeToExpire;

        AuxTimer.AuxUpdate += Update;

        EnterAction();
    }
    protected virtual void EnterAction()
    {
        counter = 0;
    }

    protected virtual void ExecuteAction(float delta)
    {

    }

    protected virtual void ExitAction()
    {
        enterActionDone = true;
        ((ItemManager)ItemManager.Instance).removeItemFromList(this);
    }

    protected void Update(float delta)
    {
        if (!enterActionDone)
        {
            counter += delta;

            if (counter < m_timeToExpire)
            {
                ExecuteAction(delta);
            }
            else
            {
                ExitAction();
            }
        }
    }
}
