using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking.Match;

public class MatchListPanel : MonoBehaviour {
	[SerializeField] private GameObject joinButtonPrefab;

	private void Awake () {
		AvailableMatchesList.OnAvailableMatchesChanged += AvailableMatchesList_OnAvailableMatchesChanged;
	}

	private void AvailableMatchesList_OnAvailableMatchesChanged (List<MatchInfoSnapshot> matches) {
		ClearExistingButtons ();
		CreateNewJoinGameButtons (matches);
	}

	private void ClearExistingButtons () {
		JoinButton [] buttons = GetComponentsInChildren<JoinButton> ();
		foreach (JoinButton button in buttons) {
			Destroy (button.gameObject);
		}
	}

	private void CreateNewJoinGameButtons (List<MatchInfoSnapshot> matches) {
		foreach (MatchInfoSnapshot match in matches) {
			GameObject button = Instantiate (joinButtonPrefab);
			button.GetComponent<JoinButton> ().Initialize (match, transform);
		}
	}
}
