using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum SoundType
{
    MIRROR,
    BUS,
    PENCIL,
    TYPING,
    GRABOBJECT,
    DROPOBJECT,
    CLOCK,
    RIGHTWORD,
    WRONGWORD,
    RIGHTPOEM,
    WRONGPOEM,
    TIKTOK
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] _soundList;

    private static SoundManager Instance;
    private AudioSource _audioSource;
    // Start is called before the first frame update


    void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;

        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        GetComponents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref _soundList, names.Length );
        for (int i = 0; i < _soundList.Length; i++)
        {
            _soundList[i]._name = names[i];
        }
    }
#endif

    private void GetComponents()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType soundType, float volume = 1)
    {
        AudioClip[] clips = Instance._soundList[(int)soundType].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        Instance._audioSource.PlayOneShot(randomClip, volume);
    }

    public static void PlayLoop(SoundType soundType)
    {
        AudioClip[] clips = Instance._soundList[(int)soundType].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        Instance._audioSource.clip = randomClip;
        Instance._audioSource.loop = true;
        Instance._audioSource.Play();
    }

    public static void StopLoop()
    {
        if(Instance._audioSource.isPlaying == true)
        {
            Instance._audioSource.Stop();
        }
    }
}


[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds {  get => _sounds; }

    [HideInInspector] public string _name;
    [SerializeField] private AudioClip[] _sounds;
}
