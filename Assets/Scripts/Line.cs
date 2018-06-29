using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour {
    public LineRenderer lineRend;
    public EdgeCollider2D edgeCol;

    private List<Vector2> points;

    private bool outVision;
    private float counter;

    private void Start()
    {
        outVision = false;
        counter = 0;
    }
    private void Update()
    {
        if(outVision)
        {
            counter += Time.deltaTime;
            if(counter >= 2)
            {
                Destroy(gameObject);
            }
        }
    }
    public void UpdateLine(Vector2 mousePos)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(mousePos);
            return;
        }

        if (Vector2.Distance(points.Last(), mousePos) > 0.1f)
        {
            SetPoint(mousePos);
        }
    }

    private void SetPoint(Vector2 point)
    {
        points.Add(point);

        lineRend.positionCount = points.Count;
        lineRend.SetPosition(points.Count - 1, point);

        if (points.Count > 1)
        {
            edgeCol.points = points.ToArray();
            edgeCol.transform.position = new Vector3(0, 0, 0);
        }
    }

    void OnBecameInvisible()
    {
        outVision = true;
    }
}
