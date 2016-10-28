using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Linq;

[RequireComponent (typeof (Text))]
public class Message : MonoBehaviour {

	Text textEditor;
	string message;
	IEnumerator coroutine;
	bool hasExecutedOnce = false;

	// Use this for initialization
	void Start () {
		textEditor = GetComponent<Text> ();
		message = textEditor.text;
		HideMessage ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowMessage () {
		if (hasExecutedOnce) return;
		coroutine = typeMessage(0.05f);
		StartCoroutine (coroutine);
	}

	public void HideMessage () {
		textEditor.text = "";
		if (coroutine != null) {
			StopCoroutine (coroutine);
		}
	}

	IEnumerator typeMessage (float intervalInSeconds) {
		char previousCharacter = '\n';
		message = message + ' ';
		foreach (char character in message.ToCharArray ()) {
			
			textEditor.text += previousCharacter;

			if (previousCharacter == '\n') {
				string[] writtenLines = textEditor.text.Split ('\n');
				if (writtenLines.Length > 2) {
					textEditor.text = string.Join("\n", writtenLines.Skip(1).ToArray());
				}
			}

			float delay = character == '\n' ? 1.0f : intervalInSeconds;

			previousCharacter = character;
				
			yield return new WaitForSeconds(delay);
		}

		hasExecutedOnce = true;
	}
}
