using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //private const float _RATE = 44100.0f;
    
    private AudioClip dialogueAudio;

    private string[] fileLines;
    
    //Subtitle variables
    private List<string> subtitleLines = new List<string>();

    private List<string> subtitleTimingStrings = new List<string>();
    public List<float> subtitleTimings = new List<float>();

    public List<string> subtitleText = new List<string>();

    private int nextSubtitle = 0;

    private string displaySubtitle;

    //Trigger variables
    private List<string> triggerLines = new List<string>();

    private List<string> triggerTimingStrings = new List<string>();
    public List<float> triggerTimings = new List<float>();

    private List<string> triggers = new List<string>();
    public List<string> triggerObjectNames = new List<string>();
    public List<string> triggerMethodNames = new List<string>();

    private int nextTrigger = 0;

    //GUI
    private GUIStyle subtitlesStyle = new GUIStyle();
    private float scaleRatio = 1.5f;
    private float heightRatio = 0.0225f;


    
    //Singleton property
    public static DialogueManager Instance {get; private set;}

    [HideInInspector]
    public AudioSource audioSource;

    private float timer;

    public bool subtitlesEnabled;
    public Toggle toggle;

    private float timeOffset;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        //audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("subtitlesEnabled"))
        {
            PlayerPrefs.SetInt("subtitlesEnabled", true?1:0);
            subtitlesEnabled = true;
        }

        else
        {
            subtitlesEnabled = PlayerPrefs.GetInt("subtitlesEnabled")==1?true:false;
        }
        
        toggle.isOn = subtitlesEnabled;
        
    }

    public void BeginDialogue (AudioClip passedClip, float timeOffset)
    {
        timer = 0f;
        this.timeOffset = timeOffset;
        
        dialogueAudio = passedClip;

        subtitleLines = new List<string>();
        subtitleTimingStrings = new List<string>();
        subtitleTimings = new List<float>();
        subtitleText = new List<string>();

        triggerLines = new List<string>();
        triggerTimingStrings = new List<string>();
        triggerTimings = new List<float>();
        triggers = new List<string>();
        triggerObjectNames = new List<string>();
        triggerMethodNames = new List<string>();

        nextSubtitle = 0;
        nextTrigger = 0;



        //Get everything from the text file

        TextAsset temp = Resources.Load("Dialogues/" + dialogueAudio.name) as TextAsset;

        
        fileLines = temp.text.Split('\n');

        //Split subtitle and trigger related lines into different lists

        foreach(string line in fileLines)
        {
            if (line.Contains("<trigger/>"))
            {
                triggerLines.Add(line);
            }

            else
            {
                subtitleLines.Add(line);
            }

            
        }

        
        //Split out our subtitle elements

        for (int cnt = 0; cnt < subtitleLines.Count; cnt++)
        {
            string [] splitTemp = subtitleLines[cnt].Split('|');
            subtitleTimingStrings.Add(splitTemp[0]);
            subtitleTimings.Add(float.Parse(CleanTimeString(subtitleTimingStrings[cnt]), System.Globalization.CultureInfo.InvariantCulture));
            subtitleText.Add(splitTemp[1]);
        }

        //Split out our trigger elements

        for (int cnt = 0; cnt < triggerLines.Count; cnt++)
        {
            string [] splitTemp1 = triggerLines[cnt].Split('|');
            triggerTimingStrings.Add(splitTemp1[0]);
            triggerTimings.Add(float.Parse(CleanTimeString(triggerTimingStrings[cnt]), System.Globalization.CultureInfo.InvariantCulture));
            
            triggers.Add(splitTemp1[1]);

            string[] splitTemp2 = triggers[cnt].Split('-');
            splitTemp2[0] = splitTemp2[0].Replace("<trigger/>", "");
            triggerObjectNames.Add(splitTemp2[0]);
            triggerMethodNames.Add(splitTemp2[1]);
        }

        //Set initial subtitle text
        if (subtitleText[0] != null)
        {
            displaySubtitle = subtitleText[0];
        }

        //Set and play the audioclip
        audioSource.clip = dialogueAudio;
        audioSource.Play();
        
        
    }

    void Update ()
    {
        //The timer is on only if we have a clip ready
        if (audioSource.clip != null)
        {
            timer += Time.deltaTime;
        }
        
        //If the clip is over, we remove it from the audioSource
        if (audioSource.clip != null && timer > audioSource.clip.length + timeOffset)
        {
            audioSource.clip = null;
        }
    }
    
    //Remove all characters that are not part of the timing float
    
    private string CleanTimeString (string timeString)
    {
        Regex digitsOnly = new Regex (@"[^\d+(\.\d+)*s]");
        return digitsOnly.Replace(timeString, "");
    }

    void OnGUI ()
    {
        //Make sure that we are using a proper dialogeAudio file
        if (dialogueAudio != null && audioSource.clip != null && audioSource.clip.name == dialogueAudio.name)
        {
            //Check for the <break/> or negative nextSubtitle number
            if(nextSubtitle > 0 && !subtitleText[nextSubtitle-1].Contains("<break/>"))
            {
                //Create our GUI
                GUI.depth = -1001;  
                subtitlesStyle.fixedWidth = Screen.width / scaleRatio;
                subtitlesStyle.wordWrap = true;
                subtitlesStyle.alignment = TextAnchor.MiddleCenter;
                subtitlesStyle.normal.textColor = Color.white;
                subtitlesStyle.fontSize = Mathf.FloorToInt(Screen.height * heightRatio);

                if (subtitlesEnabled && !ManagerMenu.Instance.paused)
                {
                    Vector2 size = subtitlesStyle.CalcSize(new GUIContent());
                    GUI.contentColor = Color.black;
                    GUI.Label(new Rect(Screen.width/2 - size.x/2 + 1, Screen.height/1.1f - size.y + 1, size.x, size.y), displaySubtitle, subtitlesStyle);
                    GUI.contentColor = Color.white;
                    GUI.Label(new Rect(Screen.width/2 - size.x/2, Screen.height/1.1f - size.y, size.x, size.y), displaySubtitle, subtitlesStyle);
                }

                else
                {
                    if (!subtitlesEnabled)
                    {
                        Vector2 size = subtitlesStyle.CalcSize(new GUIContent());
                        GUI.contentColor = Color.black;
                        GUI.Label(new Rect(Screen.width/2 - size.x/2 + 1, Screen.height/1.1f - size.y + 1, size.x, size.y), "", subtitlesStyle);
                        GUI.contentColor = Color.white;
                        GUI.Label(new Rect(Screen.width/2 - size.x/2, Screen.height/1.1f - size.y, size.x, size.y), "", subtitlesStyle);
                    }

                    else if (ManagerMenu.Instance.paused)
                    {
                        Vector2 size = subtitlesStyle.CalcSize(new GUIContent());
                        GUI.contentColor = new Color (0,0,0,0.3f);
                        GUI.Label(new Rect(Screen.width/2 - size.x/2 + 1, Screen.height/1.1f - size.y + 1, size.x, size.y), displaySubtitle, subtitlesStyle);
                        GUI.contentColor = new Color (1,1,1,0.3f);
                        GUI.Label(new Rect(Screen.width/2 - size.x/2, Screen.height/1.1f - size.y, size.x, size.y), displaySubtitle, subtitlesStyle);
                    }
                    
                    
                }
                
            }

            //Increment nextSubtitle when we hit the associated time point
            if(nextSubtitle < subtitleText.Count)
            {
                if (timer > subtitleTimings[nextSubtitle])
                {
                    displaySubtitle = subtitleText[nextSubtitle];
                    nextSubtitle++;
                }
            }

            //Fire triggers when we hit the associated time point
            if(nextTrigger < triggers.Count)
            { 
                if (timer > triggerTimings[nextTrigger])
                {
                    GameObject.FindGameObjectWithTag(triggerObjectNames[nextTrigger]).SendMessage(triggerMethodNames[nextTrigger]);
                    nextTrigger++;
                }
            }
        }
    }
}
