using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSController : MonoBehaviour
{
	//if use localHost

	public string savingFuSMsLogHistoryURL = "http://localhost/fairy_tail_fighting/savingFuSMsLogHistory.php?";

	//example if use public host
	/*
    string questionURL = "http://10.9.2.215/~hisbullah/quiz_system/getQuestion.php?";
    */
	//MD5 MD5Test;

	void Start()
	{
		//MD5Test = GetComponent<MD5>();
	}

	public IEnumerator savingFuSMsLogHistory(int idx, int idx_fighting, string previous_state, float range_characters, int total_lightattack, int total_heavyattack, 
		int total_rangedattack, int total_upattack, int total_middleattack, int total_downattack, float percentage_idle, float percentage_walk, float percentage_walkbackward, 
		float percentage_lightattack, float percentage_heavyattack, float percentage_rangedattack, float percentage_jump, float percentage_crouch, string choosen_state)
	{
		string savingFuSMsLogHistory_url = savingFuSMsLogHistoryURL + "idx=" + idx
		                                   + "&idx_fighting=" + idx_fighting
		                                   + "&previous_state=" + WWW.EscapeURL (previous_state.ToString ())
		                                   + "&range_characters=" + range_characters
		                                   + "&total_lightattack=" + total_lightattack
		                                   + "&total_heavyattack=" + total_heavyattack
		                                   + "&total_rangedattack=" + total_rangedattack
		                                   + "&total_upattack=" + total_upattack
		                                   + "&total_middleattack=" + total_middleattack
		                                   + "&total_downattack=" + total_downattack
		                                   + "&percentage_idle=" + percentage_idle
		                                   + "&percentage_walk=" + percentage_walk
		                                   + "&percentage_walkbackward=" + percentage_walkbackward
		                                   + "&percentage_lightattack=" + percentage_lightattack
		                                   + "&percentage_heavyattack=" + percentage_heavyattack
		                                   + "&percentage_rangedattack=" + percentage_rangedattack
		                                   + "&percentage_jump=" + percentage_jump
		                                   + "&percentage_crouch=" + percentage_crouch
		                                   + "&choosen_state=" + WWW.EscapeURL (choosen_state.ToString ());

		Debug.Log ("Checking..");
		WWW savingFuSMsLogHistory_get = new WWW (savingFuSMsLogHistory_url);
		yield return savingFuSMsLogHistory_get;

		if (savingFuSMsLogHistory_get.error != null) {
			print ("There was an error validating the choosen answer: " + savingFuSMsLogHistory_get.error);
		} else {
			Debug.Log (savingFuSMsLogHistory_get.text);
		}
	}
}