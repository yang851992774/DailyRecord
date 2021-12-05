//------------------------------------------------------------
// File : Line.cs
// Email: mailto:zhiqiang.yang@cootek.cn
// Desc : 
//------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GBGame
{
    public class Line : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public EdgeCollider2D edgeCollider;
        public Rigidbody2D rigidBody;

        [HideInInspector] public List<Vector2> points = new List<Vector2>();
        [HideInInspector] public int pointCount = 0;

        float pointsMinDistance = 0.1f;
        float circleColliderRadius;


        public void AddPoint(Vector2 newPoint)
        {
            if (pointCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
                return;


            points.Add(newPoint);
            pointCount++;

            // 添加圆形碰撞体
            var circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
            circleCollider.offset = newPoint;
            circleCollider.radius = circleColliderRadius;


            lineRenderer.positionCount = pointCount;
            lineRenderer.SetPosition(pointCount - 1, newPoint);

            if (pointCount > 1)
            {
                edgeCollider.points = points.ToArray();
            }

        }

        public Vector2 GetLastPoint()
        {
            return lineRenderer.GetPosition(pointCount - 1);
        }

        public void UsePhysics(bool usePhysics)
        {
            rigidBody.isKinematic = !usePhysics;
        }


        public void SetLineColor(Gradient colorGradient)
        {
            lineRenderer.colorGradient = colorGradient;
        }

        public void SetPointsMinDistance(float distance)
        {
            pointsMinDistance = distance;
        }

        public void SetLineWidth(float width)
        {
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;

            circleColliderRadius = width / 2;

            edgeCollider.edgeRadius = circleColliderRadius;
        }
    }
}


