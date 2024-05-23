using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum SoundType
{
    Bgm,
    Effect,
    MaxCount,  // 아무것도 아님, 끝을 알기 위함
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Sound
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    GameObject soundManager = new GameObject("SoundManager");
                    _instance = soundManager.AddComponent<SoundManager>();
                    DontDestroyOnLoad(soundManager);
                }
            }
            return _instance;
        }
    }

    // 배경음, 효과음 중첩이 가능하기 위함
    private AudioSource[] _audioSources = new AudioSource[(int)SoundType.MaxCount];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Play("bgm.mp3", SoundType.Bgm, 0.1f);
    }

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(SoundType)); // "Bgm", "Effect"
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)SoundType.Bgm].loop = true; // bgm 재생기는 무한 반복 재생
        }
    }

    public void Clear()
    {
        // 재생기 전부 재생 스탑, 음반 빼기
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 효과음 Dictionary 비우기
        _audioClips.Clear();
    }

    // 배경음, 효과음 재생
    public void Play(AudioClip clip, SoundType type = SoundType.Effect, float volume = 1.0f) // = 디폴트값
    {
        if (clip == null)
            return;
        if (type == SoundType.Effect)
        {
            AudioSource audioSource = _audioSources[(int)SoundType.Effect];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.volume = volume;
            audioSource.PlayOneShot(clip); // 한 번만 재생
        }
        else // Sound.Bgm
        {
            AudioSource audioSource = _audioSources[(int)SoundType.Bgm];
            audioSource.volume = volume;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void Play(string path, SoundType type = SoundType.Effect, float volume = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, volume);
    }

    AudioClip GetOrAddAudioClip(string path, SoundType type = SoundType.Effect)
    {
        if (path.Contains("Assets/Sounds") == false)
            path = $"Assets/Sounds/{path}"; // Sound 폴더 안에 저장될 수 있도록

        AudioClip audioClip = null;

        if (type == SoundType.Bgm) // BGM 배경음악 클립 붙이기
        {
            audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
        }
        else // Effect 효과음 클립 붙이기
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }
}
