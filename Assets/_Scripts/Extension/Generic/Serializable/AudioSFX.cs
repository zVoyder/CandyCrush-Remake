
namespace Extension.Generic.Serializable.Audio
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Audio;

    [System.Serializable]
    public class AudioSFX
    {
        public AudioClip clip;
        [Range(0, 1)] public float volume = 1;
        [Range(-3, 3)] public float pitch = 1;
        [Range(0, 1)] public float spatialBlend;
        public AudioMixerGroup mixerGroup;

        /// <summary>
        /// Initializes an AudioSFX object.
        /// </summary>
        /// <param name="clip">AudioClip.</param>
        /// <param name="volume">Volume.</param>
        /// <param name="pitch">Pitch.</param>
        /// <param name="spatialBlend">SpatialBlend 2D - 3D.</param>
        /// <param name="mixerGroup">AudioMixerGroup.</param>
        public AudioSFX(AudioClip clip, float volume, float pitch, float spatialBlend, AudioMixerGroup mixerGroup)
        {
            this.clip = clip;
            this.volume = volume;
            this.pitch = pitch;
            this.spatialBlend = spatialBlend;
            this.mixerGroup = mixerGroup;
        }
    }
}
