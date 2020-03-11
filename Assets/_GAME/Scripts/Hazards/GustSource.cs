using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GustSource : MonoBehaviour
{
    protected enum Direction
    {
        NULL,
        RIGHT,
        LEFT,
        UP,
        DOWN
    }

    [Header("Wing Gust Direction")]
    [SerializeField] protected Direction m_facing;

    protected delegate void GustDel();

    protected GustDel gustDelegate;

    protected virtual void OnEnable()
    {
        InitializeDirection(m_facing);
        gustDelegate = GustConsistent;
    }

    protected virtual void Update()
    {
        gustDelegate();
    }

    protected void GustConsistent()
    {

    }

    protected void InitializeDirection(Direction dir)
    {
        if (m_facing == Direction.NULL)
        {
            m_facing = Direction.RIGHT;
        }
    }
}
