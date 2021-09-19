using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AStar.Demo {
	public class Space : MonoBehaviour {
		public BoardBuilder BoardBuilder;
		public SpriteRenderer SpriteRenderer;
		public Color OpenColor;
		public Color ClosedColor;
		public Color StartColor;
		public Color EndColor;
		public bool Closed;
		public float FadeTime;
		public Vector2 Position;
		public Space[] Neighbors = new Space[4];
		public Space East {
			get { return Neighbors[0]; }
			set { Neighbors[0] = value; }
		}
		public Space North {
			get { return Neighbors[1]; }
			set { Neighbors[1] = value; }
		}
		public Space West {
			get { return Neighbors[2]; }
			set { Neighbors[2] = value; }
		}
		public Space South {
			get { return Neighbors[3]; }
			set { Neighbors[3] = value; }
		}

		private void Start() {
			if (BoardBuilder == null) {
				gameObject.SetActive(false);
				return;
			}
			SpriteRenderer = GetComponent<SpriteRenderer>();
		}

		Coroutine fadeCR;
		public void StartFade(Color color, bool force=false) {
			if (force || fadeCR == null) {
				fadeCR = StartCoroutine(Fade(color));
			}
		}

		public void OpenClose(bool closed, bool force=false) {
			Closed = closed;
			if (Closed) {
				StartFade(ClosedColor, force);
			} else {
				StartFade(OpenColor, force);
			}
		}

    public void OnMouseDown() {
			// Hold down Ctrl to set as start space.
			var ctrl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
			var shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
			var space = Input.GetKey(KeyCode.Space);
			if (ctrl && space) {
				// Ctrl+Space means open/close a ring of diameter 1 spaces around the target.
				BoardBuilder.CloseWithinRange(this, !Closed, BoardBuilder.Range);
			} else if (ctrl) {
				Closed = false;
				BoardBuilder.StartSpace.StartFade(OpenColor);
				BoardBuilder.StartSpace = this;
				StartCoroutine(Fade(StartColor));
			} else if (shift) {
				// Hold down Shift to set as end space.
				Closed = false;
				BoardBuilder.EndSpace.StartFade(OpenColor);
				BoardBuilder.EndSpace = this;
				StartCoroutine(Fade(EndColor));
			} else if (space) {
				// Hold down Space to open/close all nodes within range of this one.
				Closed = !Closed;
				BoardBuilder.CloseWithinRange(this, Closed);
			} else {
				Closed = !Closed;
				StartCoroutine(Fade(Closed ? ClosedColor : OpenColor));
			}
			BoardBuilder.RecalculatePath();
    }

		public IEnumerator Fade(Color endColor) {
			// Stop other running Fades.
			StopCoroutine("Fade");
			var startTime = Time.time;
			var endTime = startTime + FadeTime;
			var startColor = SpriteRenderer.color;
			while (Time.time < endTime) {
				var lerp = Mathf.InverseLerp(startTime, endTime, Time.time);
				SpriteRenderer.color = Color.Lerp(startColor, endColor, lerp);
				yield return null;
			}
			SpriteRenderer.color = endColor;
			fadeCR = null;
		}
  }
}
