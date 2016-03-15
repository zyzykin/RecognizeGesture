using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGesture : MonoBehaviour {

	public bool OnDraw = true;
	private LineRenderer line_render;
	private DrawPath path;
	public List<Vector3> points;
	public List<Vector3> drawingPoints;

	public GameObject trail;

	public int currentPoint = 0;

	void Awake()
	{
		line_render = GetComponent<LineRenderer>();
		path = GetComponent<DrawPath>();

		path.Init();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (PlaygroundController.PAUSE) {return;}

		if (Input.touchCount > 0)
		{
			var touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				currentPoint = 0;
				CleanGesture();
            }
			else if (touch.phase == TouchPhase.Moved)
			{
				Vector2 screenPosition = touch.position;
				Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 4));

				trail.transform.position = worldPosition;

				path.addPoint(worldPosition);
			}
			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
			}
		}
		else
		{

			if (Input.GetMouseButtonDown(0))
			{
				currentPoint = 0;
				CleanGesture();
			}
			else if (Input.GetMouseButton(0))
			{
				Vector2 screenPosition = Input.mousePosition;
				Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 4));

				trail.transform.position = worldPosition;

				path.addPoint(worldPosition);
			}
			else if (Input.GetMouseButtonUp(0))
			{
			}
		}

		if (OnDraw)
		{
			path.MyUpdate();
		}
	}

	public void CleanGesture()
	{
		path.Init();
		points.Clear();
	}
}
