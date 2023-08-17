using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonPersistent<AudioManager>
{
  [SerializeField] private AudioClip[] musicList;

  public AudioSource src;

  void Start()
  {
    src = GetComponent<AudioSource>();

    src.loop = true;
  }

  public void SetAndPlay(int index)
  {
    src.Stop();
    src.clip = musicList[index];
    src.Play();
  }
}
