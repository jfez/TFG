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

    public delegate void Action();
    public static event Action ScreamStop;

    private bool stop;

    public PruebaGuardarVoz prueba;

    private float minimumLoudness;



    void Awake()
    {
        _audio = gameObject.AddComponent<AudioSource>();

        if (useMicrophone)
        {
            if(Microphone.devices.Length > 0)
            {
                selectedDevice = Microphone.devices[Microphone.devices.Length - 1].ToString();
                Debug.Log(Microphone.devices.Length + ", " + selectedDevice);
                _audio.outputAudioMixerGroup = _mixerGroupMicrophone;
                _audio.clip = Microphone.Start(selectedDevice, true, 10, AudioSettings.outputSampleRate);
                _audio.loop = true;
                _audio.playOnAwake = false;
                while(!(Microphone.GetPosition(selectedDevice) > 0)) {}
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
    
    void Start(){
        actions.Add("stop", Stop);
        
        stop = false;

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray()); 
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;   
        keywordRecognizer.Start();

        sensitivity = 100f;
        loudness = 0f;
        highestLoudness = 0f;
        timer = 0f;
        minimumLoudness = 45f;      
    }

    void Update ()
    {
        timer += Time.deltaTime;
    
        loudness = GetAveragedVolume() * sensitivity;
        //Debug.Log(loudness);
        
        if (loudness > highestLoudness)
        {
            highestLoudness = loudness;
        }

        if (timer > 3f)
        {
            timer = 0f;
            highestLoudness = loudness;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Microphone.End(selectedDevice);
            prueba.audioClip = _audio.clip;
            Debug.Log("RESET");
            _audio.Stop();
            _audio.clip = null;
            //Destroy(_audio);
            //_audio = gameObject.AddComponent<AudioSource>();
            //_audio.outputAudioMixerGroup = _mixerGroupMicrophone;
            _audio.clip = Microphone.Start(selectedDevice, true, 10, AudioSettings.outputSampleRate);   //10
            _audio.loop = true;
            while(!(Microphone.GetPosition(selectedDevice) > 0)) {}
            _audio.Play();
        }
    }

    void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        //Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    
    void Stop()
    {
        stop = true;
        //actions.Remove("stop");
        Debug.Log(highestLoudness);

        if (audioScreams.isPlaying)
        { 
            if (highestLoudness > minimumLoudness)
            {
                audioScreams.Stop();
                
                Microphone.End(selectedDevice);


                prueba.audioClip = _audio.clip;
                _audio.Stop();
                _audio.clip = null;
                //Destroy(_audio);
                //_audio = gameObject.AddComponent<AudioSource>();
                //_audio.outputAudioMixerGroup = _mixerGroupMicrophone;
                _audio.clip = Microphone.Start(selectedDevice, true, 10, AudioSettings.outputSampleRate);   //10
                _audio.loop = true;
                while(!(Microphone.GetPosition(selectedDevice) > 0)) {}
                _audio.Play();
                Debug.Log("PLAY");

                if(ScreamStop != null)
                {
                    ScreamStop();
                }
                
                //Destroy(gameObject);
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

    public float GetLoudness ()
    {
        return loudness;
    }

}
