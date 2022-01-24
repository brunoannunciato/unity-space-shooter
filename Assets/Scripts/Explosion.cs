using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioClip;
    private AudioSource _audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.Log("AudioSource is null");
        }

        Destroy(this.gameObject, 2.6f);
        _audioSource.Play();
    }
}
