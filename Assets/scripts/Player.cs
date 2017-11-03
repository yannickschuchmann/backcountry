using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float lifetime = 2f;
	public float minimumVertexDistance = 0.1f;
	public Vector3 velocity = new Vector3(0, 0, -20);
  private float movement = 0.0f;

  private LineRenderer line;
  private List<Vector3> points;
  private Queue<float> spawnTimes = new Queue<float>();

	private Vector3 offset = new Vector3(0, 0.05f, 0);

  void Awake() {
    line = transform.Find("Line").GetComponent<LineRenderer>();
    points = new List<Vector3>() { transform.position + offset };
    line.SetPositions(points.ToArray());
  }

  void Update() {
    var horizontal = Input.GetAxis("Horizontal");
    if (horizontal != 0) {
      movement += horizontal * 0.07f;
    } else {
      movement -= movement * 0.05f;
    }
    transform.position += new Vector3(movement, 0, 0);

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
