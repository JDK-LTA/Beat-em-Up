using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class II_HealthUp : ImmediateItem
{
    public II_HealthUp(Player player, float basicModifier) : base(player, basicModifier) { }

    protected override void ApplyItem()
    {
        m_player.HpCurrent += m_basicModifier;

        if (m_player.HpCurrent > m_player.HpMax)
        {
            m_player.HpCurrent = m_player.HpMax;
        }
    }
}
