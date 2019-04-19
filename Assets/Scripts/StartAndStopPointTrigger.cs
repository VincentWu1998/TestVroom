using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAndStopPointTrigger : MonoBehaviour {

	public GameObject RoundStartTrig;

	void onTriggerEnter() {
		RoundStartTrig.SetActive (true);
	}
}
