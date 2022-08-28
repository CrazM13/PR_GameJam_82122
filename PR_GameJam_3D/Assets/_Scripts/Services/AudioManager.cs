using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	[SerializeField] private new AudioSource audio;

	[SerializeField] private AudioClip[] idleLines;
	[SerializeField] private AudioClip[] pickUpLines;
	[SerializeField] private AudioClip[] dropOffLines;

	void Start() {
		audio = GetComponent<AudioSource>();
	}

	public void PlayClip(AudioClip clip) {
		audio.PlayOneShot(clip);
	}

	private void PlayRandom(AudioClip[] clips) {
		if (clips.Length <= 0) return;

		int index = Random.Range(0, clips.Length);

		PlayClip(clips[index]);
	}

	public void PlayIdle() {
		PlayRandom(idleLines);
	}

	public void PlayPickUp() {
		PlayRandom(pickUpLines);
	}

	public void PlayDropOff() {
		PlayRandom(dropOffLines);
	}

}
