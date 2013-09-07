using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scoreboard : MonoBehaviour {
	public GameObject scoreboardLinePrefab;
	private Dictionary<GameObject, GladiatorNetwork> scoreboardEntries = new Dictionary<GameObject, GladiatorNetwork> ();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			if (Input.GetKey (KeyCode.Tab)) {
				for (int i = 0; i < this.transform.childCount; ++i)
					this.transform.GetChild (i).gameObject.SetActive (true);
			
//			Debug.Log(this.getPlayers().Count);
			foreach (var gn in this.getPlayers()) {
				if (!scoreboardEntries.ContainsValue (gn)) {
					GameObject obj = (GameObject)Instantiate (scoreboardLinePrefab);
					obj.transform.parent = this.transform;
					obj.transform.localScale = new Vector3 (1f, 1f, 1f);
					obj.transform.localPosition = new Vector3 (0f, 0f, 0f);

					scoreboardEntries.Add (obj, gn);
					
					this.gameObject.GetComponent<UIGrid>().repositionNow = true;
//					repositionNow = true;
				}
			}

			foreach (var entry in scoreboardEntries) {
				entry.Key.transform.FindChild ("Name").GetComponent<UILabel> ().text = entry.Value.name;
				entry.Key.transform.FindChild ("Kill").GetComponent<UILabel> ().text = entry.Value.Kill.ToString ();
				entry.Key.transform.FindChild ("Death").GetComponent<UILabel> ().text = entry.Value.Death.ToString ();
			}

		} else {
			for (int i = 0; i < this.transform.childCount; ++i)
				this.transform.GetChild (i).gameObject.SetActive (false);
		}

	}
}
