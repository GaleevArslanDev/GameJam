using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Music")]
        [SerializeField] private AudioClip gameMusicLoop;

        [Header("Footsteps")]
        [SerializeField] private AudioClip[] footstepTile;
        [SerializeField] private AudioClip footstepWaterPuddle;

        [Header("Slip")]
        [SerializeField] private AudioClip slipPuddleFall;
        [SerializeField] private AudioClip getUpStart;
        [SerializeField] private AudioClip getUpEnd;

        [Header("Shopping")]
        [SerializeField] private AudioClip pickupProduct;

        [Header("UI")]
        [SerializeField] private AudioClip timerLast10Sec;
        [SerializeField] private AudioClip uiPanelStatUpdate;

        [Header("Minigame")]
        [SerializeField] private AudioClip minigameSuccessHit;
        [SerializeField] private AudioClip minigameFailMiss;
        [SerializeField] private AudioClip minigameRoundWin;
        [SerializeField] private AudioClip minigameRoundLose;
        [SerializeField] private AudioClip minigameThrowItem;
        [SerializeField] private AudioClip minigameRhythmTileLight;
        [SerializeField] private AudioClip minigameRhythmJump;

        [Header("End Game")]
        [SerializeField] private AudioClip victoryWinJingle;
        [SerializeField] private AudioClip defeatGameOver;
        [SerializeField] private AudioClip statsPanelAppear;

        [Header("Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            PlayGameMusic();
        }

        private void PlayOneShot(AudioClip clip, float volume = 1f)
        {
            if (clip == null)
                return;

            sfxSource.PlayOneShot(clip, volume);
        }

        public void PlayGameMusic()
        {
            if (gameMusicLoop == null)
                return;

            musicSource.clip = gameMusicLoop;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlayFootstepTile()
        {
            if (footstepTile == null || footstepTile.Length == 0)
                return;

            PlayOneShot(
                footstepTile[
                    Random.Range(0, footstepTile.Length)
                ],
                0.7f
            );
        }

        public void PlayFootstepWater()
        {
            PlayOneShot(footstepWaterPuddle);
        }

        public void PlaySlipFall()
        {
            PlayOneShot(slipPuddleFall);
        }

        public void PlayGetUpStart()
        {
            PlayOneShot(getUpStart);
        }

        public void PlayGetUpEnd()
        {
            PlayOneShot(getUpEnd);
        }

        public void PlayPickup()
        {
            PlayOneShot(pickupProduct);
        }

        public void PlayTimerWarning()
        {
            PlayOneShot(timerLast10Sec);
        }

        public void PlayStatUpdate()
        {
            PlayOneShot(uiPanelStatUpdate, 0.6f);
        }

        public void PlayHit()
        {
            PlayOneShot(minigameSuccessHit);
        }

        public void PlayMiss()
        {
            PlayOneShot(minigameFailMiss);
        }

        public void PlayRoundWin()
        {
            PlayOneShot(minigameRoundWin);
        }

        public void PlayRoundLose()
        {
            PlayOneShot(minigameRoundLose);
        }

        public void PlayThrow()
        {
            PlayOneShot(minigameThrowItem);
        }

        public void PlayRhythmLight()
        {
            PlayOneShot(minigameRhythmTileLight);
        }

        public void PlayRhythmJump()
        {
            PlayOneShot(minigameRhythmJump);
        }

        public void PlayVictory()
        {
            PlayOneShot(victoryWinJingle);
        }

        public void PlayDefeat()
        {
            PlayOneShot(defeatGameOver);
        }

        public void PlayStatsPanel()
        {
            PlayOneShot(statsPanelAppear);
        }
    }
}