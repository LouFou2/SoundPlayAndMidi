using UnityEngine;

public class SoundSequencer : MonoBehaviour
{
    public ClockTimer clockTimer;
    public bool[] playOnBeats; // the array size should be similar to 'clockTimer.beatsInBar' (set in inspector)
    private bool hasPlayed = false;
    private int previousBeat = -1; // Initialize to an invalid value

    private AudioSource audioSource;
    [SerializeField] private SoundSequencer _soundSequencer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clockTimer = GameObject.Find("ClockTimer").GetComponent<ClockTimer>();
    }

    private void Update()
    {
        if (clockTimer != null)
        {
            int currentBeat = clockTimer.beatCount;

            // Check if the beat count has changed
            if (currentBeat != previousBeat)
            {
                // Reset hasPlayed for the new beat
                hasPlayed = false;
                previousBeat = currentBeat;
            }

            // Check if the array element corresponding to the current beat should play
            if (playOnBeats[currentBeat])
            {
                //if (audioSource.isPlaying ) { audioSource.Stop(); }
                if (!hasPlayed) 
                { 
                    audioSource.Play();
                    hasPlayed = true;
                } 
            }
        }
    }
}
