//------------------------------------------------------------
// File : LineDrawer.cs
// Email: mailto:zhiqiang.yang@cootek.cn
// Desc : 
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GBGame
{
    public class LineDrawer : MonoBehaviour
    {
        public GameObject linePrefab;

        public LayerMask canDrawOverLayer;
        int canDrawLayerIndex;

        [Space(30)]
        public Gradient lineColor;
        public float linePointsMinDistance;
        public float lineWidth;

        Line currentLine;
        Camera cam;


        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
            canDrawLayerIndex = LayerMask.NameToLayer("CantDrawOver");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                BeginDraw();
            if (null != currentLine)
                Draw();
            if (Input.GetMouseButtonUp(0))
                EndDraw();
        }


        // 画线逻辑

        void BeginDraw()
        {
            currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

            currentLine.UsePhysics(false);
            currentLine.SetLineColor(lineColor);
            currentLine.SetPointsMinDistance(linePointsMinDistance);
            currentLine.SetLineWidth(lineWidth);


        }

        void Draw()
        {
            var pos = cam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.CircleCast(pos, lineWidth / 3f, Vector2.zero, 1f, canDrawOverLayer);
            if (hit)
            {
                EndDraw();
            }
            else
            {
                currentLine.AddPoint(pos);
            }

        }

        void EndDraw()
        {
            if (currentLine == null)
            {
                return;
            }

            if (currentLine.pointCount < 2)
            {
                Destroy(currentLine.gameObject);
                currentLine = null;
            }
            else
            {
                currentLine.gameObject.layer = canDrawLayerIndex;
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
    }
}


