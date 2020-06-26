using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OI_SpeedChange : OverTimeItem
{
    private float lastCurrentXSpeed = 0;
    private float lastCurrentYSpeed = 0;
    private float deltaXSpeed = 0;
    private float deltaYSpeed = 0;

    public OI_SpeedChange(Player player, float timeToExpire, float amountToChange): base(player, timeToExpire, amountToChange) { }

    protected override void EnterAction()
    {
        base.EnterAction();

        lastCurrentYSpeed = m_player.YSpeed;
        lastCurrentXSpeed = m_player.XSpeed;

        m_player.YSpeed += (m_amountToChange * (m_player.XSpeed / m_player.YSpeed));
        m_player.XSpeed += m_amountToChange;
        if (m_player.XSpeed > m_player.MaxXSpeed)
        {
            m_player.YSpeed = (m_player.XSpeed / m_player.YSpeed) * m_player.MaxXSpeed;
            m_player.XSpeed = m_player.MaxXSpeed;
        }
        else if (m_player.XSpeed < 0)
        {
            m_player.YSpeed = 0;
            m_player.XSpeed = 0;
        }

        deltaYSpeed = m_player.YSpeed - lastCurrentYSpeed;
        deltaXSpeed = m_player.XSpeed - lastCurrentXSpeed;

        if (m_amountToChange >= 0)
        {
            m_player.SpriteRenderer.color = Color.magenta;
        }
        else
        {
            m_player.SpriteRenderer.color = Color.yellow;
        }
    }
    protected override void ExitAction()
    {
        m_player.YSpeed -= deltaYSpeed;
        m_player.XSpeed -= deltaXSpeed;

        m_player.SpriteRenderer.color = Color.white;

        base.ExitAction();
    }
}
