using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmediateItem : ItemParent
{
    protected float m_basicModifier;

    public ImmediateItem(Player player, float basicModifier)
    {
        m_player = player;
        m_basicModifier = basicModifier;

        ApplyItem();
    }

    protected virtual void ApplyItem() { }
}
