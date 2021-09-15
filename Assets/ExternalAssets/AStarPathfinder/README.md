# A* Pathfinder

This simple-to-use, yet robust pathfinder is a must-have for navigating any graphed elements.

Just implement 1 method to find the neighbors of a given node, and you're done! Or make 
use of a variety of helper methods to add functionality, such as

* Performing an action on all path nodes (or all nodes within a range) 
  (`PathCallback(T)`/`AllOptionCallback(T)`)
* Quitting early given some condition of a node (`QuitEarly(T)`)
* Modify weight of a path between nodes based on criteria of your design
  (`GetDistance(T, T, List<T> path)`)

It's not limited to 2D grids, either - it can navigate 3D elements, or even non-visual 
linked graphs.

Check out the demo scene for an example of the various features, or see below for 
instructions on how to incorporate this pathfinder into your game. 

# Implementation

Extend the `AStarPathfinder` class with a template type that represents the nodes of your graph.

```csharp
public class SpacePathfinder : AStarPathfinder<Space> {
  protected override IEnumerable<Space> FindNeighbors(Space space) {
    // Call `AddNeighbor(space)` to each Space that is adjacent and accessible to `space`.
  }
}
```

## Optional Fields/Methods

There are several optional fields which you can specify and methods you can implement
to hone the behavior of your AStarPathfinder to your liking.

### Fields

* `MaxRange`: This is the maximum distance the pathfinder should look before giving up.
* `MinRange`: Minimum distance for a node to be considered a valid destination. Useful for
  things like ranged attacks that must target at least X tiles away.

### Methods

* `GetDistance(T, T, List<T>)`: By default the score of a node is the length of its path
  from the origin node. Implement this to change the calculation that determines the
  score of a given node (and therefor its priority in determining the best path).
* `QuitEarly(T)`: Use this to quit execution of `FindPath` or `FindAllInRange` early
  based on the current node. In the case of `FindPath`, the path to the given node
  will be returned.
* `Prepare()`: This is invoked before running `FindPath` or `FindAllInRange`. Override
  to add special processes before either is called (don't forget to call `base.Prepare()`
  if you do).
* `IsValidPath(T)`: Implement this method to perform some checks on the node T before
  adding it as a valid path/destination. An alternative to checking for valid neighbors
  during `FindNeighbors`.
* `PathCallback(T)`: After `FindPath` finds the shortest path between two nodes but
  before that path is returned, `PathCallback` is called on each node in the path.
* `AllOptionCallback(T)`: Called on every valid option found during `FindAllInRange`.

## Usage

Using your `AStarPathfinder` class is easy once you've implemented the appropriate 
methods.

* `FindPath(T, T)`: The most common usage of A* Pathfinding, call this to find the 
  shortest path (if any) between two items.
* `FindAllInRange(T, int)`: If instead of a specific path, you actually want to find all
  the cells within range, this is the method to call.
  * The second parameter `maxRange` can be used to override the normal MaxRange for
    the duration of this call.
