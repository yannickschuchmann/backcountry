using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public static Player instance = null;
	public float speed = 20.0f;

  private float movement = 0.0f;

	public static Player getInstance() {
		return instance;
	}

  void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
			DontDestroyOnLoad(gameObject);
		}
  }

  void Update() {
    var horizontal = Input.GetAxis("Horizontal");
    if (horizontal != 0) {
      movement += horizontal * 0.07f;
    } else {
      movement -= movement * 0.05f;
    }
    transform.position += new Vector3(movement, 0, 0);
  }
}
