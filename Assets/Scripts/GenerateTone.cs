using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GenerateTone : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GenerateSound(float frequency, float duration)
    {
        int sampleRate = AudioSettings.outputSampleRate;
        int sampleLength = Mathf.CeilToInt(duration * sampleRate);
        float[] samples = new float[sampleLength];

        for (int i = 0; i < sampleLength; i++)
        {
            samples[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate);
        }

        AudioClip audioClip = AudioClip.Create("Tone", sampleLength, 1, sampleRate, false);
        audioClip.SetData(samples, 0);

        audioSource.clip = audioClip;
        audioSource.Play();
    }
}