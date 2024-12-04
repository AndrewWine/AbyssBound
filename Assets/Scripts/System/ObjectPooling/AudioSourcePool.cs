using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool
{
    private Queue<AudioSource> pool = new Queue<AudioSource>();
    private AudioSource prefab;
    private int initialSize;

    public AudioSourcePool(AudioSource prefab, int initialSize)
    {
        this.prefab = prefab;
        this.initialSize = initialSize;

        // Populate the pool with initial AudioSource objects
        for (int i = 0; i < initialSize; i++)
        {
            AudioSource obj = UnityEngine.Object.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public AudioSource Get()
    {
        if (pool.Count > 0)
        {
            AudioSource audioSource = pool.Dequeue();
            audioSource.gameObject.SetActive(true);  // Activate the AudioSource
            return audioSource;
        }
        else
        {
            // If the pool is empty, create a new AudioSource
            AudioSource newAudioSource = UnityEngine.Object.Instantiate(prefab);
            newAudioSource.gameObject.SetActive(true);
            return newAudioSource;
        }
    }

    public void Return(AudioSource audioSource)
    {
        audioSource.gameObject.SetActive(false);  // Deactivate the AudioSource
        pool.Enqueue(audioSource);
    }

    public void Clear()
    {
        while (pool.Count > 0)
        {
            UnityEngine.Object.Destroy(pool.Dequeue().gameObject);
        }
    }
}
