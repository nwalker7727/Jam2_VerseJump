using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.UI;

public class MenuAudioManager : MonoBehaviour
{
    private EventInstance musicEventInstances;
    private EventDescription musicDescription;
    private float fadeOut;

    public static MenuAudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Start()
    {
        fadeOut = 4;
        InitializeMusic(MenuEvents.instance.menuMusic);
    }

    private void FixedUpdate()
    {
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstances = CreateInstance(musicEventReference);
        musicEventInstances.getDescription(out musicDescription);
        musicEventInstances.start();
    }

    public void SetMusic(float paramNum)
    {
        musicEventInstances.setParameterByName("Menu", paramNum);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void thanos()
    {
        musicEventInstances.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
