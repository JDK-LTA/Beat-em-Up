using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class II_SpeedUp : ImmediateItem
{
    public II_SpeedUp(Player player, float basicModifier): base(player, basicModifier)
    {
    }

    protected override void ApplyItem()
    {
        m_player.YSpeed += (m_basicModifier * (m_player.XSpeed / m_player.YSpeed));
        m_player.XSpeed += m_basicModifier;
        if (m_player.XSpeed > m_player.MaxXSpeed)
        {
            m_player.YSpeed = (m_player.XSpeed / m_player.YSpeed) * m_player.MaxXSpeed;
            m_player.XSpeed = m_player.MaxXSpeed;
        }
    }
}
