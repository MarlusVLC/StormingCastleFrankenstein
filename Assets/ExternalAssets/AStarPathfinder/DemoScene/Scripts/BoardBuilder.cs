using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AStar.Demo {
	public class BoardBuilder : MonoBehaviour {
		public CameraController CameraController;
		public LineRenderer PathLineRenderer;
		public Text RangeText;
		public int Range = 1;
		public Text MessageText;
		public Space SpacePrefab;
		[Range(2, 20)] public int Width = 5;
		[Range(2, 20)] public int Height = 5;
		public List<Space> Spaces;
		private Dictionary<Vector2, Space> spaceMap = new Dictionary<Vector2, Space>();

		public Space StartSpace;
		public Space EndSpace;
		public bool SeedRandom;
		public int RandomSeed;

		public DemoPathfinder Pathfinder;
		private System.Random random;

		// Use this for initialization
		private void Start () {
			Pathfinder = new DemoPathfinder() {
				PathGradient = PathLineRenderer.colorGradient
			};

			random = SeedRandom ? new System.Random(RandomSeed) : new System.Random();
			for (var x = 0; x < Width; x++) {
				for (var y = 0; y < Height; y++) {
					var space = Instantiate(SpacePrefab, transform);
					space.Position = new Vector2(x, y);
					spaceMap[space.Position] = space;
					space.transform.position = new Vector3(x, y, 0);
					space.name = x + ", " + y;
					space.BoardBuilder = this;
					CameraController.AddTarget(space.gameObject);
					Spaces.Add(space);
					AddNeighbors(space);
				}
			}

			// Select a random start and end space initially.
			StartSpace = Spaces[random.Next(Spaces.Count)];
			StartSpace.StartFade(StartSpace.StartColor);
			do {
				EndSpace = Spaces[random.Next(Spaces.Count)];
			} while (Spaces.Count > 1 && EndSpace == null || EndSpace == StartSpace);
			EndSpace.StartFade(EndSpace.EndColor);

			RecalculatePath();
		}

		private void Update() {
			var scrollDelta = Input.GetAxis("Mouse ScrollWheel");
			if (Input.GetKeyUp(KeyCode.Minus) || scrollDelta < 0f) {
				Range = Math.Max(0, Range - 1);
				RangeText.text = "Pulse Range: " + Range;
			}
			if (Input.GetKeyUp(KeyCode.Plus) || scrollDelta > 0f) {
				Range = Math.Min(Range + 1, 10);
				RangeText.text = "Pulse Range: " + Range;
			}

      if (Input.GetKeyUp(KeyCode.P)) {
        var screenshotName = "pathfinder_" + DateTime.Now.ToString() + ".png";
        screenshotName = screenshotName.Replace("/", "-").Replace(":", "-");
        ScreenCapture.CaptureScreenshot(screenshotName);
        Debug.Log("Screenshot saved to " + screenshotName);
      }
		}

		private void AddNeighbors(Space space) {
			var westPos = space.Position;
			westPos.x--;
			if (spaceMap.ContainsKey(westPos)) {
				space.West = spaceMap[westPos];
				space.West.East = space;
			}
			var southPos = space.Position;
			southPos.y--;
			if (spaceMap.ContainsKey(southPos)) {
				space.South = spaceMap[southPos];
				space.South.North = space;
			}

		}

		List<Space> path;
		public void RecalculatePath() {
			if (path != null) {
				path.ForEach(space => space.OpenClose(space.Closed));
			}
			path = Pathfinder.FindPath(StartSpace, EndSpace);
			if (path == null) {
				MessageText.text = "No path found between " + StartSpace.name + " and " + EndSpace.name;
				PathLineRenderer.positionCount = 0;
				StartSpace.StartFade(PathLineRenderer.colorGradient.Evaluate(0f), true);
				EndSpace.StartFade(PathLineRenderer.colorGradient.Evaluate(1f), true);
				return;
			}
			MessageText.text = "";
			PathLineRenderer.positionCount = path.Count;
			for (var i = 0; i < path.Count; i++) {
				PathLineRenderer.SetPosition(i, path[i].transform.position);
			}
		}

    public void CloseWithinRange(Space space, bool closed, int minRange=0) {
			Pathfinder.AllowClosed = true;
			Pathfinder.Closing = closed;
			Pathfinder.MinRange = minRange;
			Pathfinder.MaxRange = Range;
			// Opening/closing the node is handled in the `AllOptionCallback` in `DemoPathfinder`.
			var inRange = Pathfinder.FindAllInRange(space);
			Pathfinder.MinRange = 0f;
			Pathfinder.MaxRange = 0f;
			Pathfinder.AllowClosed = false;
    }

		public void SetTaxiCab(bool set) {
			Pathfinder.VectorDistance = !set;
			RecalculatePath();
		}
  }
}
