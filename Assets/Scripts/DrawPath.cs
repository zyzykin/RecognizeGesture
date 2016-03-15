using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawPath : MonoBehaviour {

	private LineRenderer lineRenderer;
	public int maxPoint = 10;
	private float pointsDistance = 0.3f;
	private PlayerGesture Gesture;
	public BezierPath bezierPath;
	private List<Vector3> gizmos;

	// Use this for initialization
	public void Init() 
	{
		lineRenderer = GetComponent<LineRenderer>();
		Gesture = GetComponent<PlayerGesture>();
	}

	public void MyUpdate () 
	{
		Render();
	}

	public void addPoint(Vector3 point)
	{
//		print (myPlane.points.Count);
		point.z = transform.position.z;

		//if (Gesture.points.Count <= maxPoint) {
			if (Gesture.points.Count <= 1){
				Gesture.points.Add(point);
				return;
			}

			int index = Gesture.points.Count >= 2 ? Gesture.points.Count-2 : Gesture.points.Count-1;


			if(Vector3.Distance(Gesture.points[index], point) >=  pointsDistance){
				Gesture.points.Insert(Gesture.points.Count-1, point);
			}

			Gesture.points[Gesture.points.Count-1] = point;
		//}
	}

	public bool PointsColectorIsFull()
	{
		return Gesture.points.Count >= maxPoint;
	}

	private void Render()
	{
		RenderBezier ();
		BezierInterpolate ();
	}

	private void RenderBezier()
	{
		bezierPath = new BezierPath ();
		
		bezierPath.SetControlPoints (Gesture.points);
		Gesture.drawingPoints = bezierPath.GetDrawingPoints2 ();
		
		gizmos = Gesture.drawingPoints;
		
		SetLinePoints (Gesture.drawingPoints);
	}

	private void BezierInterpolate()
	{
		bezierPath = new BezierPath();
		bezierPath.Interpolate(Gesture.points, 0.25f);

		Gesture.drawingPoints = bezierPath.GetDrawingPoints2();
		
		gizmos = bezierPath.GetControlPoints();  
		SetLinePoints(Gesture.drawingPoints);
	}
	
	private void SetLinePoints(List<Vector3> drawingPoints)
	{
		if (lineRenderer != null) {
			//lineRenderer.sortingOrder = 10;
            lineRenderer.SetVertexCount (drawingPoints.Count);

			for (int i=0; i< Gesture.currentPoint; i++) {
				if (Gesture.currentPoint <= drawingPoints.Count) {
					drawingPoints [i] = transform.position;
				}
			}

			for (int i=0; i<drawingPoints.Count; i++) {
				lineRenderer.SetPosition (i, drawingPoints [i]);
			}
		}
	}

	public void OnDrawGizmos()
	{
		if (gizmos == null)
		{
			return;
		}        
		
		for (int i = 0; i < gizmos.Count; i++)
		{
			Gizmos.DrawWireSphere(gizmos[i], 0.25f);            
		}
	}

}
