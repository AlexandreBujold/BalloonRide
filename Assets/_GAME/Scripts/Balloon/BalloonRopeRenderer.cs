using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonGame.Balloon.Rendering
{
    public class BalloonRopeRenderer : MonoBehaviour
    {
        public LineRenderer m_lineRenderer;
        public Transform startPos;
        public Transform endPos;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (m_lineRenderer != null)
            {
                if (startPos != null && endPos != null)
                {
                    Vector3[] positions = new Vector3[2];
                    positions[0] = startPos.position;
                    positions[1] = endPos.position;
                    m_lineRenderer.SetPositions(positions);
                }
            }
        }
    } 
}
