using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AStar.Demo {
  /// <summary>
  /// Controls focus of camera on important targets, buffer size.
  /// </summary>
  public class CameraController : MonoBehaviour {
    public float DampTime = 0.2f;
    public float ScreenEdgeBuffer = 1f;
    public float MinSize = 1f;
    public List<Transform> Targets;
    public Camera Camera;
    private float zoomSpeed;
    private Vector3 moveVelocity;
    private Vector3 desiredPosition;

    private void Start() {
      SetStartPositionAndSize();
    }

    private void Update() {
      FindAveragePosition();
      Move();
      float requiredSize = FindRequiredSize();
      Zoom(requiredSize);
      var distDiff = transform.position - desiredPosition;
      var sizeDiff = Mathf.Abs(Camera.orthographicSize - requiredSize);
      if (distDiff.sqrMagnitude < .01f && sizeDiff < .01f) {
        transform.position = desiredPosition;
        Camera.orthographicSize = requiredSize;
      }
    }

    public void AddTarget(GameObject target) {
      if (Targets.Contains(target.transform)) {
        return;
      }
      Targets.Add(target.transform);
    }

    public void Reset() {
      Targets.Clear();
    }

    private void Move() {
      transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, DampTime);
    }

    public void FindAveragePosition() {
      Vector3 averagePos = new Vector3();
      int numTargets = 0;

      foreach (var target in Targets) {
        if (!target.gameObject.activeSelf) {
          continue;
        }

        averagePos += target.position;
        numTargets++;
      }

      if (numTargets > 0) {
        averagePos /= numTargets;
      }

      averagePos.z = transform.position.z;

      desiredPosition = averagePos;
    }

    private void Zoom(float requiredSize) {
      Camera.orthographicSize = Mathf.SmoothDamp(Camera.orthographicSize, requiredSize, ref zoomSpeed, DampTime);
    }

    private float FindRequiredSize() {
      Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPosition);

      float size = 0f;

      for (int i = 0; i < Targets.Count; i++) {
        if (!Targets[i].gameObject.activeSelf) {
          continue;
        }

        Vector3 targetLocalPos = transform.InverseTransformPoint(Targets[i].position);

        Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

        size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

        size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / Camera.aspect);
      }

      size += ScreenEdgeBuffer;

      size = Mathf.Max(size, MinSize);

      return size;
    }

    public void SetStartPositionAndSize() {
      FindAveragePosition();

      transform.position = desiredPosition;

      Camera.orthographicSize = FindRequiredSize();
    }
  }
}
