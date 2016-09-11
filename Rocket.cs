using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RocketNet;
using System;

[RequireComponent(typeof(AudioSource))]


public class Rocket : MonoBehaviour {

	AudioSource music;
	int samplerate;

	Device rocket;

    public bool isPlayer = false;

    static Dictionary<string, Track> tracks = new Dictionary<string, Track>();
	static float currentRow  = 0;
	static AudioSource globalAudioSource;
	float BPM = 135*4;
	bool isPlaying = true;


    public enum Tracks
    {
        SCENE,
        FX_1,
        FX_2
    };

	// Use this for initialization

	void Start () {
		Application.runInBackground = true;

		this.music = this.GetComponent<AudioSource>();

		globalAudioSource = this.music;
		this.samplerate = this.music.clip.frequency;

        
		this.rocket = new Device("sync", isPlayer);

        if (!isPlayer)
        {
            this.rocket.Connect();
        }
		this.rocket.IsPlaying = this.IsPlaying; 
		this.rocket.SetRow = this.SetRow;
		this.rocket.Pause = Pause;

        Array values = Enum.GetValues(typeof(Tracks));

        foreach (Tracks val in values)
        {
            this.AddTrack(val.ToString());
            
        }

    }		


	public void Pause(bool pause)
	{
		if(pause)
		{
			isPlaying = false;
			this.music.Pause();
		}
		else
		{
			isPlaying = true;
			this.music.Play();
		}
	}
	public bool IsPlaying()
	{
		return isPlaying;
	}


	public void SetRow(int row)
	{
		currentRow = row;
		this.music.timeSamples = (int)(row/BPM*60*samplerate);
	}

	public void AddTrack(string name)
	{
		tracks.Add(name, this.rocket.GetTrack(name));
	}

	public static float GetValue(string name)
	{
		return tracks[name].GetValue(currentRow);
	}

    public static float GetValue(Tracks name)
    {
        return tracks[name.ToString()].GetValue(currentRow);
    }


    public static float GetTime()
	{
		return globalAudioSource.time;
	}

	public static int GetRow()
	{
		return (int)currentRow;
	}


	// Update is called once per frame
	void Update ()
	{
		int row = (int)(music.timeSamples*BPM/60.0/samplerate);
		this.rocket.Update(row);
		currentRow = (float)(music.timeSamples*BPM/60.0/samplerate);
	}
}
