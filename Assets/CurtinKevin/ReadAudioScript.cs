using UnityEngine;
using System.Collections;

public class ReadAudioScript : MonoBehaviour {

	//Volume
	float[] currentOutputData; //volume level
	float volumeLevel;
	float rawVolume;
	Vector3 nextVolumeScale;

	//Pitch
	float[] currentAudioSpectrum; //frequency spectrum
//	double pitchValue; //Hz
	float fSample;
	int FREQSAMPLES = 1024;
	float threshold = .02f;

	// Use this for initialization
	void Start () {
		currentOutputData = new float[8196];
		volumeLevel = 0;
		rawVolume = 0;
		nextVolumeScale = new Vector3(0,0,0);

		currentAudioSpectrum = new float[FREQSAMPLES];
		fSample = AudioSettings.outputSampleRate;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public Vector3 GetNextVolumeScale(GameObject myGameObject){
		audio.GetOutputData(currentOutputData,0);

		for(int i=0;i<currentOutputData.Length;i++){
			rawVolume = currentOutputData[i];
			volumeLevel += Mathf.Abs(rawVolume);
		}
		
		volumeLevel = (volumeLevel/250);
		//volumeLevel += (Mathf.Sqrt(rawVolume*rawVolume));
		nextVolumeScale.Set(volumeLevel, volumeLevel, volumeLevel);
		return nextVolumeScale;
	}

	public double GetSpectrumData(){
		audio.GetSpectrumData(currentAudioSpectrum, 0, FFTWindow.BlackmanHarris);
		float maxV = 0;
		int maxN = 0;
		for(int i=0;i<FREQSAMPLES;i++){
			if(currentAudioSpectrum[i] > maxV && currentAudioSpectrum[i] > threshold){
				maxV = currentAudioSpectrum[i];
				maxN = i; //index of Max Frequency
			}
		}
		double freqN = maxN;
		if(maxN > 0 && maxN > FREQSAMPLES-1){
			float dL = currentAudioSpectrum[maxN-1]/currentAudioSpectrum[maxN];
			float dR = currentAudioSpectrum[maxN+1]/currentAudioSpectrum[maxN];
			freqN += 0.5*(dR*dR - dL*dL);
		}
		double pitchValue = freqN*(fSample/2)/FREQSAMPLES;
		return pitchValue;
	}
}
