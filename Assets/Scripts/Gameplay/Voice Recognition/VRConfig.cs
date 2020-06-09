using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VRConfig : MonoBehaviour
{
    private AudioSource _audio;
    private string selectedDevice;
    public AudioMixerGroup _mixerGroupMicrophone, _mixerGroupMaster;
    private bool useMicrophone;
    private float loudness;
    private float sensitivity;
    public Slider sliderSensitivity;
    
    // Start is called before the first frame update
    void Start()
    {
        _audio = gameObject.AddComponent<AudioSource>();
        
        useMicrophone = true;
        if (!PlayerPrefs.HasKey("Sensitivity"))
        {
            PlayerPrefs.SetFloat("Sensitivity", 100f);
            
        }
        
        SetSensitivity(PlayerPrefs.GetFloat("Sensitivity"));
        loudness = 0f;
         
        
        if(Microphone.devices.Length > 0)
        {
            selectedDevice = Microphone.devices[0].ToString();
            PlayerPrefs.SetInt("Microphone", 0);
            //Debug.Log(Microphone.devices.Length + ", " + selectedDevice);
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

        if (!useMicrophone)
        {
            _audio.outputAudioMixerGroup = _mixerGroupMaster;
            _audio.clip = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        loudness = GetAveragedVolume() * sensitivity;
        //Debug.Log (loudness);
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

    public void RestartMicrophone(int indexMicro)
    {
        PlayerPrefs.SetInt("Microphone", indexMicro);
        selectedDevice = Microphone.devices[indexMicro].ToString();
        Debug.Log(Microphone.devices.Length + ", " + selectedDevice);
        _audio.outputAudioMixerGroup = _mixerGroupMicrophone;
        _audio.clip = Microphone.Start(selectedDevice, true, 10, AudioSettings.outputSampleRate);
        _audio.loop = true;
        _audio.playOnAwake = false;
        while(!(Microphone.GetPosition(selectedDevice) > 0)) {}
        _audio.Play();
    }

    public void SetSensitivity(float newSensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", newSensitivity);
        sliderSensitivity.value = newSensitivity;
        sensitivity = newSensitivity;
    }
}
