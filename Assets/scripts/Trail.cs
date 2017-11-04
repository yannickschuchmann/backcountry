using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

	public float lifetime = 2f;
	public float minimumVertexDistance = 0.1f;
	private Vector3 velocity;
  private LineRenderer line;
  private List<Vector3> points;
  private Queue<float> spawnTimes = new Queue<float>();
	private Vector3 offset = new Vector3(0, 0.05f, 0);

	void Awake () {
		line = transform.Find("Line").GetComponent<LineRenderer>();
    points = new List<Vector3>() { transform.position + offset };
    line.SetPositions(points.ToArray());
	}

	void Start () {
		velocity = new Vector3(0, 0, -20.0f);
	}

	void Update () {
    while (spawnTimes.Count > 0 && spawnTimes.Peek() + lifetime < Time.time) {
      RemovePoint();
    }

    Vector3 diff = -velocity * Time.deltaTime;
    for (int i = 1; i < points.Count; i++) {
      points[i] += diff;
    }

    if (points.Count < 2 || Vector3.Distance(transform.position, points[1]) > minimumVertexDistance) {
      AddPoint(transform.position + offset);
    }

    points[0] = transform.position + offset;

    line.positionCount = points.Count;
    line.SetPositions(points.ToArray());
	}

  void AddPoint(Vector3 position) {
    points.Insert(1, position);
    spawnTimes.Enqueue(Time.time);
  }

  void RemovePoint() {
    spawnTimes.Dequeue();
    points.RemoveAt(points.Count - 1);
  }
}
