﻿using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Observer Register")]
    public PlayerBlackBoard blackBoard;
    public AreaSound areaSound;
    public DeathBringerArea bossFightSound;
    public AnimationTriggersDamageOfPlayer triggersDamageOfPlayer;

    [SerializeField] private float sfxMinimunDistance;
    [SerializeField] private AudioSource sfxPrefab;  // Prefab for SFX
    [SerializeField] private AudioSource[] sfx;

    [SerializeField] private AudioSource[] bgm;

    public bool playBgm;
    private int bgmIndex;

    private AudioSourcePool sfxPool;

    private void Awake()
    {
        // Create an ObjectPool for SFX with an initial size of 10
        sfxPool = new AudioSourcePool(sfxPrefab, 20);
        
    }

    private void Update()
    {
        if (!playBgm)
            StopAllBGM();
        else
        {
            if (!bgm[bgmIndex].isPlaying)
            {
                PlayBGM(bgmIndex);
            }
        }
    }

    private void OnEnable()
    {
        //player
        blackBoard.primaryAttack.PlaySfxAtk += PlaySFX;
        blackBoard.primaryAttack2.PlaySfxAtk += PlaySFX;
        blackBoard.primaryAttack3.PlaySfxAtk += PlaySFX;
        blackBoard.playerDeathState.playerDeathSFX += PlaySFX;
        triggersDamageOfPlayer.PlayerSFXAtk += PlaySFX;
        CharacterStats.playerbeinghit += PlaySFX;


        //area
        areaSound.soundAction += PlaySFX;
        areaSound.stopSoundAction += StopSFXWithTime;

        //enemy
        AnimationFinishTriggerEnemy.AttackSFX += PlaySFX;
        DeathState.deathSFX += PlaySFX;
        HitState.BeingHit += PlaySFX;
        
        //Death Bringer
        DeathStateDeathBringer.deathBringerDeathSFX += PlaySFX;
        ExitTeleport.exitTeleportSFX += PlaySFX;
        DeathBringerSpellController.spellSFX += PlaySFX;

        //battle sound
        bossFightSound.bosssoundAction += PlayBGM;
        bossFightSound.stopbossSoundAction += StopBattleFieldSound;
    }

    private void OnDisable()
    {
        //player
        blackBoard.primaryAttack.PlaySfxAtk -= PlaySFX;
        blackBoard.primaryAttack2.PlaySfxAtk -= PlaySFX;
        blackBoard.primaryAttack3.PlaySfxAtk -= PlaySFX;
        blackBoard.playerDeathState.playerDeathSFX -= PlaySFX;
        CharacterStats.playerbeinghit -= PlaySFX;
        triggersDamageOfPlayer.PlayerSFXAtk -= PlaySFX;

        //area
        areaSound.soundAction -= PlaySFX;
        areaSound.stopSoundAction -= StopSFXWithTime;

        //enemy
        AnimationFinishTriggerEnemy.AttackSFX -= PlaySFX;
        DeathState.deathSFX -= PlaySFX;
        HitState.BeingHit -= PlaySFX;

        //Death Bringer
        DeathStateDeathBringer.deathBringerDeathSFX -= PlaySFX;
        ExitTeleport.exitTeleportSFX -= PlaySFX;
        DeathBringerSpellController.spellSFX -= PlaySFX;
        bossFightSound.bosssoundAction -= PlayBGM;
        bossFightSound.stopbossSoundAction -= StopBattleFieldSound;

    }

    public void PlaySFX(int _sfxIndex, Transform _source)
    {
        // Kiểm tra nếu nguồn âm thanh gần người chơi và chỉ phát khi cần thiết
        if (_source != null && Vector2.Distance(GameObject.Find("Player").transform.position, _source.position) > sfxMinimunDistance)
        {
            return;  // Nếu quá xa, không phát âm thanh
        }

        if (_sfxIndex < sfx.Length)
        {
            // Lấy AudioSource từ pool và phát âm thanh
            AudioSource audioSource = sfxPool.Get();
            audioSource.pitch = UnityEngine.Random.Range(0.85f, 1.1f);
            audioSource.clip = sfx[_sfxIndex].clip;
            audioSource.Play();

            // Trả lại AudioSource vào pool sau khi phát xong
            StartCoroutine(ReturnToPoolAfterPlay(audioSource));
        }
    }


    private IEnumerator ReturnToPoolAfterPlay(AudioSource audioSource)
    {
        // Wait for the audio to finish playing
        yield return new WaitForSeconds(audioSource.clip.length);
        sfxPool.Return(audioSource);
    }

    public void StopSFX(int _index) => sfx[_index].Stop();


    public void StopSFXWithTime(int _index)
    {
        StartCoroutine(DecreaseVolume(sfx[_index]));
    }

    private IEnumerator DecreaseVolume(AudioSource _audio)
    {
        float defaultVolume = _audio.volume;
        while (_audio.volume > 0.1f)
        {
            _audio.volume -= _audio.volume * 0.2f;
            yield return new WaitForSeconds(0.25f);
            if (_audio.volume <= 0.1f)
            {
                _audio.Stop();
                _audio.volume = defaultVolume;
                break;
            }
        }
    }

    public void PlayRandomBGM()
    {
        bgmIndex = UnityEngine.Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void PlayBGM(int _bgmIndex)
    {
        bgmIndex = _bgmIndex;
        StopAllBGM();
        bgm[bgmIndex].Play();
    }
    public void StopBattleFieldSound()
    {
        StopAllBGM();
        StartCoroutine(WaitAndPlayBGM(5, bgmIndex)); // Gọi sau 5 giây
    }

    private IEnumerator WaitAndPlayBGM(float waitTime, int _bgmIndex)
    {
        yield return new WaitForSeconds(waitTime);
        PlayRandomBGM();
    }
    public void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}
