using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public abstract class AbstractSoundPlayerService
    {
        protected AudioSource _audioSource;

        public abstract void PlayOneShot(AudioClip audioClip);
    }

}
