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

    public SaveVoice saveVoice;

    private float minimumLoudness;
    private GameManager gameManager;

    private AudioSource scratchAudioSource;

    public AudioClip eventClip;
    public AudioClip strongerClip;
    private float timeOffset;

    void Awake()
    {
        _audio = gameObject.AddComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (useMicrophone)
        {
            if(Microphone.devices.Length > 0)
            {
                try
                {
                    selectedDevice = Microphone.devices[PlayerPrefs.GetInt("Microphone")].ToString();
                }

                catch
                {
                    selectedDevice = Microphone.devices[0].ToString();
                }
                
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
        actions.Add("past", Past);

        scratchAudioSource = GameObject.FindGameObjectWithTag("Scratch").GetComponent<AudioSource>();
        timeOffset = 2f;
        

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray()); 
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;   
        keywordRecognizer.Start();

        sensitivity = 100f;
        loudness = 0f;
        highestLoudness = 0f;
        timer = 0f;
        minimumLoudness = 50f;      
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

        if (Input.GetKeyDown(KeyCode.F3))
        {
            highestLoudness = 100f;
            Stop();
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            highestLoudness = 10f;
            Stop();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        { 
            Past();
        }
        
    }

    void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        //Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    
    void Stop()
    {
        Debug.Log("STOP");
        if (audioScreams.isPlaying && !ManagerMenu.Instance.paused && GameManager.Instance.indexIsland == 2)
        { 
            Debug.Log(highestLoudness);
            
            if (highestLoudness > minimumLoudness)
            {
                audioScreams.Stop();
                GameManager.Instance.PlayFallDownJail();
                
                Microphone.End(selectedDevice);

                saveVoice.audioClip = _audio.clip;
                _audio.Stop();
                _audio.clip = null;
                _audio.clip = Microphone.Start(selectedDevice, true, 10, AudioSettings.outputSampleRate);   //10
                _audio.loop = true;
                while(!(Microphone.GetPosition(selectedDevice) > 0)) {}
                _audio.Play();

                gameManager.OpenSecondPortal();

                if(ScreamStop != null)
                {
                    ScreamStop();
                }

                if (DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Time)
                {
                    scratchAudioSource.gameObject.GetComponent<ScratchManager>().ScratchTime();
                }

                else if (DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Voxophone)
                {
                    scratchAudioSource.gameObject.GetComponent<ScratchManager>().ScratchVoxophone();
                }

                DialogueManager.Instance.audioSource.spatialBlend = 0f;
                DialogueManager.Instance.BeginDialogue(eventClip, timeOffset, AudioKind.AudioKindEnum.Event);
                GameManager.Instance.finishAudios = true;
            }

            else
            {
                if (DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Time)
                {
                    scratchAudioSource.gameObject.GetComponent<ScratchManager>().ScratchTime();
                }

                else if (DialogueManager.Instance.audioKindEnum == AudioKind.AudioKindEnum.Voxophone)
                {
                    scratchAudioSource.gameObject.GetComponent<ScratchManager>().ScratchVoxophone();
                }

                DialogueManager.Instance.audioSource.spatialBlend = 0f;
                DialogueManager.Instance.BeginDialogue(strongerClip, timeOffset, AudioKind.AudioKindEnum.Event);
            }
            
        }
    }

    void Past()
    {
        Debug.Log("PAST");
        if (!GameManager.Instance.statueDisolved && GameManager.Instance.onPositionToDisolve && !ManagerMenu.Instance.paused)
        {
            GameManager.Instance.DissolveStatueSign();
            //Debug.Log("PAST");
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
