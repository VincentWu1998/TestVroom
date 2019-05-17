using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficLights : MonoBehaviour {
	private List<Light> reds = new List<Light>();
	private List<Light> yellows = new List<Light>();
	private List<Light> greens = new List<Light>();

	private int curr = 0;

	// Use this for initialization
	void Start () {
		//reds.Add (this.gameObject.transform.GetChild (2).transform.getChild (0));

		Light[] lights = GetComponentsInChildren<Light>();
		foreach (Light light in lights) {
			Debug.LogError ("NAME: " + light.name);
			if (light.name.IndexOf ("rLight") != -1) {
				reds.Add (light);
				Debug.LogError ("RED: " + reds.Count);
			} else if (light.name.IndexOf ("gLight") != -1) {
				greens.Add (light);
				Debug.LogError ("GREEN: " + greens.Count);
			} else {
				yellows.Add (light);
				Debug.LogError ("YELLOW: " + yellows.Count);
			}
		}
		changeLights ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void changeLights() {
		while (true) {
			curr = (curr + 1) % 3;
			//yield WaitForSeconds(5);
			Debug.LogError("INTERVAL");
		}
	}
}
