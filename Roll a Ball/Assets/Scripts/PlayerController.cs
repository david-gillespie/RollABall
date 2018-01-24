using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text statusText;
	public Text timerText;

	private Rigidbody rb;
	private int count;
	private float startTime;
	private bool playing;

	void Start(){
		rb = GetComponent<Rigidbody> ();

		count = 0;

		setCountText ();

		statusText.text = "";
		startTime = Time.time;
		playing = true;

		countText.text = "Count: 0";
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal,0.0f,moveVertical);

		rb.AddForce (movement * speed);
	}

	void Update(){
		if (playing) {
			float t = 30.0f - (Time.time - startTime);
			if (t > 0) {
				string minutes = ((int)(t / 60)).ToString ();
				string seconds = (t % 60).ToString ("f2");
				timerText.text = minutes + ":" + seconds;
			} else {
				timerText.text = 0 + ":" + 0;
				statusText.text = "You Lose!";
				playing = false;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("pickup")){
			other.gameObject.SetActive(false);
			count++;
			setCountText ();
		}else if(other.gameObject.CompareTag("bonus")){
			other.gameObject.SetActive(false);
			count+=5;
			setCountText();
		}
	}

	void setCountText(){
		if (playing) {
			countText.text = "Count: " + count.ToString ();
			if (count >= 16) {
				statusText.text = "You Win!";
				playing = false;
			}
		}
	}
}