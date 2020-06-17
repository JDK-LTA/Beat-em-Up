using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OI_Invincibility : OverTimeItem
{
    public OI_Invincibility(Player player, float timeToExpire): base(player, timeToExpire) { }

    protected override void EnterAction()
    {
        base.EnterAction();

        m_player.IsInvincible = true;
    }

    protected override void ExitAction()
    {
        m_player.IsInvincible = false;
        base.ExitAction();
    }
}
