using UnityEngine;

namespace Ski {
  public class ObstacleGenerator {
    public Obstacle[] obstacles;
    private Vector3[] spawnPoints;

    public ObstacleGenerator(Obstacle[] _obstacles, Vector3[] _spawnPoints) {
      obstacles = _obstacles;
      spawnPoints = _spawnPoints;
    }
    public Obstacle nextObstacle() {
      return obstacles[Random.Range(0, obstacles.Length)];
    }
    public Vector3 nextPosition() {
      float randomX = Random.Range(spawnPoints[0].x, spawnPoints[1].x);
      return new Vector3(randomX, spawnPoints[0].y, spawnPoints[0].z);
    }
  }
}
