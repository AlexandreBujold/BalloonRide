using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonGame.GustHazard
{
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
            switch(m_facing)
            {
                case Direction.LEFT:
                    Debug.DrawLine(transform.position, transform.position - new Vector3(3, 0, 0), Color.red);
                    break;
                case Direction.UP:
                    Debug.DrawLine(transform.position, transform.position + new Vector3(0, 3, 0), Color.red);
                    break;
                case Direction.DOWN:
                    Debug.DrawLine(transform.position, transform.position - new Vector3(0, 3, 0), Color.red);
                    break;
                case Direction.RIGHT:
                    Debug.DrawLine(transform.position, transform.position + new Vector3(3, 0, 0), Color.red);
                    break;
                default:
                    Debug.DrawLine(transform.position, transform.position + new Vector3(3, 0, 0), Color.red);
                    break;
            }
        }

        protected void InitializeDirection(Direction dir)
        {
            if (m_facing == Direction.NULL)
            {
                m_facing = Direction.RIGHT;
            }
        }
    }
}
