using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AStar.Demo {
  [Serializable]
  public class DemoPathfinder : AStarPathfinder<Space> {
    /// <summary>
    /// Enabled while opening all spaces within range of a space.
    /// </summary>
    public bool AllowClosed;
    /// <summary>
    /// When enabled, calculate distance between nodes as vector distance instead of path length.
    /// </summary>
    public bool VectorDistance;
    /// <summary>
    /// True when closing all nodes within certain range, false when opening.
    /// </summary>
    public bool Closing;

    public Gradient PathGradient;

    protected override void FindNeighbors(Space cell) {
      foreach (var neighbor in cell.Neighbors.Where(neighbor => neighbor != null && (AllowClosed || !neighbor.Closed))) {
        AddNeighbor(neighbor);
      }
    }

    // OPTIONAL METHODS //

    protected override float GetDistance(Space from, Space to, List<Space> path) {
      if (VectorDistance) {
        return (from.transform.position - to.transform.position).sqrMagnitude;
      }
      return base.GetDistance(from, to, path);
    }

    protected override void PathCallback(Space pathNode, List<Space> path) {
      var ind = path.IndexOf(pathNode);
      if (ind < 0) {
        return;
      }
      var lerp = Mathf.InverseLerp(0, path.Count, ind);
      pathNode.StartFade(PathGradient.Evaluate(lerp), true);
    }

    protected override void AllOptionCallback(Space pathNode) {
      pathNode.OpenClose(Closing);
    }
  }
}
