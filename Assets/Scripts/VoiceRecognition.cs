using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.Audio;

public class VoiceRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public AudioSource audioScreams;

    private float sensitivity;
    private float loudness;
    private float highestLoudness;
    private float timer;
    private AudioSource _audio;
    public bool useMicrophone;
    private string selectedDevice;

    public AudioMixerGroup _mixerGroupMicrophone, _mixerGroupMaster;


    void Start(){
        actions.Add("stop", Stop);
        

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray()); 
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;   
        keywordRecognizer.Start();

        sensitivity = 100f;
        loudness = 0f;
        highestLoudness = 0f;
        timer = 0f;

        _audio = GetComponent<AudioSource>();

        if (useMicrophone)
        {
            if(Microphone.devices.Length > 0)
            {
                selectedDevice = Microphone.devices[0].ToString();
                _audio.outputAudioMixerGroup = _mixerGroupMicrophone;
                _audio.clip = Microphone.Start(selectedDevice, true, 10, AudioSettings.outputSampleRate);
                _audio.Play();
            }

            else
            {
                useMicrophone = false;
            }
            
        }

        if (!useMicrophone)
        {
            _audio.outputAudioMixerGroup = _mixerGroupMaster;
            _audio.clip = null;
        }

        
        
    }

    void Update ()
    {
        timer += Time.deltaTime;
        
        loudness = GetAveragedVolume() * sensitivity;
        
        if (loudness > highestLoudness)
        {
            highestLoudness = loudness;
        }

        if (timer > 3f)
        {
            timer = 0f;
            highestLoudness = loudness;
        }
    }

    void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        //Debug.Log(speech.text);
        actions[speech.text].Invoke();

    }
    
    void Stop()
    {
        Debug.Log(highestLoudness);

        if (audioScreams.isPlaying)
        { 
            if (highestLoudness > 20)
            {
                audioScreams.Stop();
            }
            
        }
        

    }

    float GetAveragedVolume () 
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);

        }
        return a/256;
    }

    
}
