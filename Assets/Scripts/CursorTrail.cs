using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrail : MonoBehaviour
{
    public LineRenderer trailPrefab = null;
    public Camera trailCamera;
    public float clearSpeed = 1;

    private float _distanceFromCamera = 10.0f;
    private LineRenderer currentTrail;
    private List<Vector3> points = new List<Vector3>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DestroyCurrentTrail();
            CreateCurrentTrail();
            AddPoint();
        }

        if (Input.GetMouseButton(0))
        {
            AddPoint();
        }

        UpdateTrailPoints();

        ClearTrailPoints();
    }

    private void DestroyCurrentTrail()
    {
        if (currentTrail != null)
        {
            Destroy(currentTrail.gameObject);
            currentTrail = null;
            points.Clear();
        }
    }

    private void CreateCurrentTrail()
    {
        currentTrail = Instantiate(trailPrefab);
        currentTrail.transform.SetParent(transform, true);
    }

    private void AddPoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        points.Add(trailCamera.ViewportToWorldPoint(new Vector3(mousePosition.x / Screen.width, mousePosition.y / Screen.height, _distanceFromCamera)));
    }

    private void UpdateTrailPoints()
    {
        if (currentTrail != null && points.Count > 1)
        {
            currentTrail.positionCount = points.Count;
            currentTrail.SetPositions(points.ToArray());
        }
        else
        {
            DestroyCurrentTrail();
        }
    }

    private void ClearTrailPoints()
    {
        float clearDistance = Time.deltaTime * clearSpeed;
        while (points.Count > 1 && clearDistance > 0)
        {
            float distance = (points[1] - points[0]).magnitude;
            if (clearDistance > distance)
            {
                points.RemoveAt(0);
            }
            else
            {
                points[0] = Vector3.Lerp(points[0], points[1], clearDistance / distance);
            }
            clearDistance -= distance;
        }
    }

    void OnDisable()
    {
        DestroyCurrentTrail();
    }

}
