using UnityEngine;

namespace Services
{
    public class SimpleSoundPlayerService : AbstractSoundPlayerService
    {
        public SimpleSoundPlayerService(AudioSource audioSource) => _audioSource = audioSource;

        public override void PlayOneShot(AudioClip audioClip) => _audioSource.PlayOneShot(audioClip);
    }

}
