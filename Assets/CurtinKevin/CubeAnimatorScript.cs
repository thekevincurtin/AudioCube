using UnityEngine;
using System.Collections;

public class CubeAnimatorScript : MonoBehaviour {
	
	GameObject myAnimatedGameObject;
	public ReadAudioScript readAudioScript;
	int updateCounter; //Think of a way to use this var to make smoother animations (using an if in update)
	Vector3 currentScale;
	Vector3 nextScale;
	float dominantPitchFrequency;

	Color lowColor;
	Color midColor;
	Color highColor;
	Color currentColor;

	float highSpeed;
	float midSpeed;
	float lowSpeed;
	//float lerp = 5.0f;

	MeshRenderer meshRenderer;
	float rotspeed;

	
	// Use this for initialization
	void Start () {
		myAnimatedGameObject = GameObject.Find("AudioCube");
	//	updateCounter = 0;
		currentScale = new Vector3(0,0,0);
		nextScale = new Vector3(0,0,0);

		GameObject light = new GameObject("My Light");
		light.AddComponent<Light>();
		light.transform.position = new Vector3(0, 1, 7);

		currentColor = Color.yellow;

		gameObject.renderer.material.color = currentColor;

		lowColor = Color.blue;
		midColor = Color.green;
		highColor = Color.yellow;


		meshRenderer = myAnimatedGameObject.GetComponent<MeshRenderer>();
		rotspeed = 200;
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (rotspeed*Time.deltaTime,rotspeed*Time.deltaTime,rotspeed*Time.deltaTime,Space.World);


		//audio.GetOutputData(currentOutputData,64);
		//if(updateCounter==30){
			nextScale = readAudioScript.GetNextVolumeScale(myAnimatedGameObject);
		//	updateCounter = 0;
			//myAnimatedGameObject.transform.localScale = new Vector3(currentVolumeLevel,currentVolumeLevel,currentVolumeLevel);
			//nextScale.Set(currentVolumeLevel, currentVolumeLevel, currentVolumeLevel);
			myAnimatedGameObject.transform.localScale = Vector3.Lerp(currentScale, nextScale, Time.deltaTime);
			currentScale = nextScale;
			//Debug.Log(currentScale.x);
		//}
		//else{
		//	updateCounter++;
		//}

		if(updateCounter==100){
			dominantPitchFrequency = (float) readAudioScript.GetSpectrumData();
			//myColor = new Color(255*(dominantPitchFrequency/50000), 0, 255-255*(dominantPitchFrequency/1400));
			//Material newMaterial = new Material(Shader.Find("Transparent/Diffuse"));
			//float lerp = Mathf.PingPong(Time.time, 1.0f) / 1.0f;
			if(dominantPitchFrequency<70){
				//renderer.material.color = Color.Lerp(currentColor,lowColor,lerp);
				//myAnimatedGameObject.renderer.material.color = lerpColor;
				//newMaterial.color = Color.Lerp(currentColor,lowColor,lerp);
				Material newMaterial = new Material(Shader.Find("Reflective/Diffuse"));
				//newMaterial.color = Color.Lerp(currentColor,lowColor,lerp);
				newMaterial.color = lowColor;
				currentColor = lowColor;
				meshRenderer.material = newMaterial;
				myAnimatedGameObject.renderer.material.color = lowColor;
				Debug.Log ("low");
			}
			else if(dominantPitchFrequency<1000){
				//renderer.material.color  = Color.Lerp(currentColor, midColor, lerp);
				//myAnimatedGameObject.renderer.material.color = lerp;
				//newMaterial.color = Color.Lerp(currentColor,lowColor,lerp);
				Material newMaterial = new Material(Shader.Find("Reflective/Diffuse"));
				//newMaterial.color = Color.Lerp(currentColor,midColor,lerp);
				newMaterial.color = midColor;
				//currentColor = midColor;
				meshRenderer.material = newMaterial;
				myAnimatedGameObject.renderer.material.color = midColor;
				Debug.Log ("mid");
			}
			else{
				//renderer.material.color  = Color.Lerp(currentColor, highColor, lerp);
				//myAnimatedGameObject.renderer.material.color = lerp;
				//newMaterial.color = Color.Lerp(currentColor,lowColor,lerp);
				Material newMaterial = new Material(Shader.Find("Reflective/Diffuse"));
				//newMaterial.color = Color.Lerp(currentColor,highColor,lerp);
				newMaterial.color = highColor;
				//currentColor = highColor;
				meshRenderer.material = newMaterial;
				myAnimatedGameObject.renderer.material.color = highColor;
				Debug.Log ("high");
			}
			updateCounter = 0;
		}
		else{
			updateCounter++;
		}
		
		//myMaterial.color = myColor;
		//myMeshRenderer.material = myMaterial;

		//Debug.Log(dominantPitchFrequency);

	}
	
	public void ResizeCubeWithVolume(float[] currentOutputData, GameObject myObject){
		//my
	}

}
