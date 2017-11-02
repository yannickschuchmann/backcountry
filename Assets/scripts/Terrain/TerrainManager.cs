using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ski {
  public class TerrainManager : MonoBehaviour {
    public Obstacle[] obstacles;
    private ObstacleGenerator obstacleGenerator;

    private GameObject ground;
    private Vector3 groundSize;
    private Vector3 clearPoint;
    private Transform dynamicObstacles;

    public float speed = 13.0f;

    public float minObstacleDistance = 5.0f;
    private float deltaObstacleDistance = 0.0f;

    void Start() {
      dynamicObstacles = transform.Find("DynamicTerrain");
      ground = transform.Find("Ground").gameObject;
      groundSize = ground.GetComponent<Renderer>().bounds.size;
      clearPoint = ground.transform.position + new Vector3(0, 0, groundSize.z / 2);

      initObstacleGenerator();
    }

    void Update() {
      foreach (Transform child in dynamicObstacles.transform) {
        if (child.position.z >= clearPoint.z) {
          Destroy(child.gameObject);
        } else {
          child.position += new Vector3(0, 0, Time.deltaTime * speed);
        }
      }

      if (deltaObstacleDistance <= 0) {
        if (Random.value <= 0.3) {
          spawnObstacle();
        }
        deltaObstacleDistance = minObstacleDistance;
      } else {
        deltaObstacleDistance -= Time.deltaTime * speed;
      }
    }

    void initObstacleGenerator() {
      Vector3 spawnA = ground.transform.position, spawnB = ground.transform.position;
      float width = groundSize.x;
      float depth = groundSize.z;
      spawnA.x -= width / 2;
      spawnA.z -= depth / 2;

      spawnB.x += width / 2;
      spawnB.z -= depth / 2;

      obstacleGenerator = new ObstacleGenerator(
        obstacles,
        new Vector3[] { spawnA, spawnB }
      );
    }

    void spawnObstacle() {
      GameObject newObstacle = Instantiate(obstacleGenerator.nextObstacle().prefab, obstacleGenerator.nextPosition(), Quaternion.identity);
      newObstacle.transform.SetParent(dynamicObstacles);
    }
  }
}