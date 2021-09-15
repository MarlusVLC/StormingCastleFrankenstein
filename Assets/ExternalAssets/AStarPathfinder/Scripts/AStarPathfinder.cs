/*
 * Copyright 2018 Dane Zeke Liergaard All Rights Reserved.
 *
 * Licensed under the GNU General Public License Version 3 (the "License");
 *
 * You may copy, distribute and modify the software as long as you track changes/dates
 * in source files. Any modifications to or software including (via compiler) GPL-licensed
 * code must also be made available under the GPL along with build & install instructions.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace AStar {
  /// <summary>
  /// Base implementation of an A* pathfinding algorithm.
  /// </summary>
  [Serializable]
  public abstract class AStarPathfinder<T> {
    /// <summary>
    /// Minimum range to be considered valid. Useful for ranged attacks that can't
    /// attack anything within certain distance.
    /// </summary>
    public float MinRange;
    /// <summary>
    /// The maximum distance a valid cell can be.
    /// </summary>
    public float MaxRange;

    protected Dictionary<T, Node> Open = new Dictionary<T, Node>();
    protected Dictionary<T, Node> Closed = new Dictionary<T, Node>();

    /// <summary>
    /// A Dictionary of reachable cells (from most recent call to FindAllInRange) to the paths to those cells.
    /// </summary>
    public Dictionary<T, List<T>> Reachable {
      get {
        return Closed.ToDictionary(kv => kv.Key, kv => kv.Value.Path);
      }
    }
    /// <summary>
    /// A re-used hash set of the neighbors of a cell. The required overridden
    /// function FindNeighbors must add the neighbors to this set.
    /// </summary>
    private HashSet<T> neighbors = new HashSet<T>();
    private T origin;
    private T destination;

    protected class Node {
      public float Score;
      public List<T> Path;

      public Node() {
        Path = new List<T>();
        Score = 0f;
      }

      public Node(List<T> path) {
        Path = path;
        Score = path.Count;
      }

      public Node(List<T> path, float score) {
        Path = path;
        Score = score;
      }
    }

    /// <summary>
    /// Finds all the nodes in range from start node.
    /// Calls AllOptionCallback on each in-range node found.
    /// </summary>
    /// <param name="start">Start node.</param>
    /// <returns>IEnumerable of the closed nodes.</returns>
    public IEnumerable<T> FindAllInRange(T start) {
      Prepare();
      origin = start;
      destination = default(T);
      Open[start] = new Node { Path = new List<T> { start } };
      while (Open.Count > 0) {
        var closest = CloseClosestOpenNode();
        if (closest.Value.Score >= MinRange && (MaxRange == 0f || closest.Value.Score <= MaxRange)) {
          AllOptionCallback(closest.Key);
        }
        if (QuitEarly(closest.Key)) {
          break;
        }
        // Queue neighbor nodes if any, and they don't already have shorter paths.
        MaybeQueueNeighbors(closest);
      }
      // If MinRange > 0f, limit result to closed nodes at or greater than that distance.
      if (MinRange > 0f) {
        return Closed.Keys.Where(t => Closed[t].Score >= MinRange && (MaxRange == 0f || Closed[t].Score <= MaxRange));
      }
      return Closed.Keys;
    }

    /// <summary>
    /// Finds the most expedient path from start T to end T via A* pathfinding.
    /// Includes the starting space but not the destination.
    /// </summary>
    /// <param name="start">Start node.</param>
    /// <param name="end">Destination node.</param>
    public List<T> FindPath(T start, T end) {
      Prepare();
      origin = start;
      destination = end;
      var startNode = new Node { Path = new List<T> { start } };
      Open[start] = startNode;
      KeyValuePair<T, Node> shortestPath = new KeyValuePair<T, Node>();
      while (Open.Count > 0) {
        var lowest = CloseLowestScoreOpenNode();
        // If we've found the shortest path to dest, no need to look further.
        if (lowest.Key.Equals(end) || QuitEarly(lowest.Key)) {
          shortestPath = lowest;
          break;
        }
        MaybeQueueNeighbors(lowest);
      }
      if (shortestPath.Value == null) {
        UnityEngine.Debug.Log("No path between " + start + " and " + end);
        return null;
      }
      foreach (var t in shortestPath.Value.Path) {
        PathCallback(t, shortestPath.Value.Path);
      }
      return shortestPath.Value.Path;
    }

    protected void AddNeighbor(T neighbor) {
      neighbors.Add(neighbor);
    }

    /// <summary>
    /// Override to determine distance between two cells.
    /// Default is the length of the given path between them.
    /// </summary>
    /// <param name="from">Source cell.</param>
    /// <param name="to">Destination cell.</param>
    /// <param name="path">Current path between the cells.</param>
    /// <returns>Distance between the cells.</returns>
    protected virtual float GetDistance(T from, T to, List<T> path) {
      return path != null ? path.Count : 0;
    }

    /// <summary>
    /// Implement this to cause the pathfinder to quit early if a cell meets certain parameters.
    /// </summary>
    /// <param name="t">Source cell.</param>
    /// <returns>True if the pathfinder should quit searching now.</returns>
    protected virtual bool QuitEarly(T cell) {
      return false;
    }

    /// <summary>
    /// The only method that requires implementation. This function must call
    /// `AddNeighbor(cell)` on each cell that is considered a neighbor of `cell`.
    /// </summary>
    protected abstract void FindNeighbors(T cell);

    /// <summary>
    /// Implement this to do any preparation before searching for paths/ranges.
    /// Make sure to call base.Prepare() if you do to clear Open and Closed lists.
    /// </summary>
    protected virtual void Prepare() {
      Open.Clear();
      Closed.Clear();
    }

    /// <summary>
    /// Implement this to check if a cell is a valid path. If not valid,
    /// the cell will not be traversed during pathfinding and will not be
    /// considered a valid destination.
    /// </summary>
    /// <param name="t"></param>
    /// <returns>True if cell is a valid path.</returns>
    protected virtual bool IsValidPath(T cell) {
      return true;
    }

    /// <summary>
    /// Implement this method to perform some action on each node along the path
    /// to the destination during FindPath.
    /// </summary>
    /// <param name="pathNode">The node in the path to destination.</param>
    /// <param name="path">The path from origin to destination.</param>
    protected virtual void PathCallback(T pathNode, List<T> pathToNode) {
    }

    /// <summary>
    /// Implement this to perform some action on each node found in FindAllInRange.
    /// </summary>
    /// <param name="pathNode">The in-range node found.</param>
    protected virtual void AllOptionCallback(T pathNode) {
    }

    private void MaybeQueueNeighbors(KeyValuePair<T, Node> pair) {
      FindNeighbors(pair.Key);
      if (neighbors.Count == 0) {
        return;
      }
      foreach (var neighbor in neighbors) {
        var newPath = new List<T>(pair.Value.Path) { neighbor };
        MaybeQueueNeighbor(pair.Value, newPath, neighbor);
      }
      neighbors.Clear();
    }

    private void MaybeQueueNeighbor(Node node, List<T> newPath, T neighbor) {
      if (neighbor == null) {
        return;
      }

      // If this is not a valid node to traverse, move on.
      if (!IsValidPath(neighbor)) {
        return;
      }

      // Calculate score based on "distance" between node and origin, and
      // between node and destination (if any).
      var newScore = GetDistance(origin, neighbor, newPath);
      if (destination != null) {
        newScore += GetDistance(neighbor, destination, null);
      }

      // If this cell would be out of MaxRange (if any), move on.
      if (MaxRange != 0f && newScore > MaxRange) {
        return;
      }

      // If neighbor doesn't already have a distance, use this value.
      if (!Open.ContainsKey(neighbor) && !Closed.ContainsKey(neighbor)) {
        var newNode = new Node(newPath, newScore);
        Open[neighbor] = newNode;
        return;
      }

      // If there's already a longer path to the neighbor, replace the path and score.
      if (Open.ContainsKey(neighbor) && newScore < Open[neighbor].Score) {
        Open[neighbor].Path = newPath;
        Open[neighbor].Score = newScore;
        return;
      }

      // If the neighbor is closed but this path is shorter, reopen it.
      if (Closed.ContainsKey(neighbor) && newScore < Closed[neighbor].Score) {
        Closed[neighbor].Path = newPath;
        Closed[neighbor].Score = newScore;
        Open[neighbor] = Closed[neighbor];
        Closed.Remove(neighbor);
        return;
      }
    }

    /// <summary>
    /// Returns and closes the open node with the lowest score.
    /// </summary>
    /// <returns>Open node with the lowest score.</returns>
    private KeyValuePair<T, Node> CloseLowestScoreOpenNode() {
      if (Open == null || Open.Count == 0) {
        return new KeyValuePair<T, Node>();
      }
      var lowest = Open.First();
      foreach (var pair in Open) {
        if (pair.Value.Score < lowest.Value.Score) {
          lowest = pair;
        }
      }
      Open.Remove(lowest.Key);
      Closed[lowest.Key] = lowest.Value;
      return lowest;
    }

    /// <summary>
    /// Returns the open node with the shortest path from the start node.
    /// </summary>
    /// <returns>Open node with the shortest path.</returns>
    private KeyValuePair<T, Node> CloseClosestOpenNode() {
      if (Open == null || Open.Count == 0) {
        return new KeyValuePair<T, Node>();
      }
      var closest = Open.First();
      foreach (var pair in Open) {
        if (pair.Value.Path.Count < closest.Value.Path.Count) {
          closest = pair;
        }
      }
      Open.Remove(closest.Key);
      Closed[closest.Key] = closest.Value;
      return closest;
    }
  }
}
