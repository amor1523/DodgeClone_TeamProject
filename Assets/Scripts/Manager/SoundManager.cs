using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum SoundType
{
    Bgm,
    Effect,
    MaxCount,  // �ƹ��͵� �ƴ�, ���� �˱� ����
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

    // �����, ȿ���� ��ø�� �����ϱ� ����
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

            _audioSources[(int)SoundType.Bgm].loop = true; // bgm ������ ���� �ݺ� ���
        }
    }

    public void Clear()
    {
        // ����� ���� ��� ��ž, ���� ����
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // ȿ���� Dictionary ����
        _audioClips.Clear();
    }

    // �����, ȿ���� ���
    public void Play(AudioClip clip, SoundType type = SoundType.Effect, float volume = 1.0f) // = ����Ʈ��
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
            audioSource.PlayOneShot(clip); // �� ���� ���
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
            path = $"Assets/Sounds/{path}"; // Sound ���� �ȿ� ����� �� �ֵ���

        AudioClip audioClip = null;

        if (type == SoundType.Bgm) // BGM ������� Ŭ�� ���̱�
        {
            audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
        }
        else // Effect ȿ���� Ŭ�� ���̱�
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
