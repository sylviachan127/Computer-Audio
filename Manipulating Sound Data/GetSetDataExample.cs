using UnityEngine;
using System.Collections;
//attach this Component/Script to a new GameObject in the Hierarchy when finished
[RequireComponent(typeof(AudioSource))] //optional: this automatically adds an AudioSource to your GameObject if it doesn't have one
public class GetSetDataExample : MonoBehaviour {

	public float noiseThreshold = 0.5f; //changing this in the editor will alter this value
										//if you change this in the Editor, it will ALWAYS overwrite this initial assignment
										//think of this as the default value when the script is initially added to your GameObject

	// Use this for initialization
	void Start () {
		AudioSource audioSource = GetComponent<AudioSource>(); //grabbing a reference to our AudioSource

		int lengthSamples = audioSource.clip.samples; //number of samples in this clip
		int channels = audioSource.clip.channels; //number of channels, mono = 1, stereo = 2
		int samplingFrequency = audioSource.clip.frequency; //Sampling Frequency, # of samples per second

		//stream (the last argument) must be set to false or else SetData will not work
		AudioClip newClip = AudioClip.Create ("ModifiedClip", lengthSamples, channels, samplingFrequency, false);

		float[] sampleData = new float[audioSource.clip.samples * audioSource.clip.channels];

		int thirtyFive = Mathf.Abs (-35); //absolute value example

		audioSource.clip.GetData(sampleData, 0); //getting the sample data and storing it in the float array we declared

		for (int i=0; i < sampleData.Length; i++)
		{
			//do your operations here
			sampleData[i] = sampleData[i] * 0.5F;  //e.g. reduces the amplitude of the clip by 1/2
			if (i % 1000 == 0)
				Debug.Log (i + ": " + sampleData[i]);
		}

		newClip.SetData (sampleData, 0); //set the altered data to the new clip
		audioSource.clip = newClip; //assign the newly created clip to our AudioSource
		audioSource.Play (); //play the new clip after assigning it to the AudioSource

	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (noiseThreshold);
	
	}
}
