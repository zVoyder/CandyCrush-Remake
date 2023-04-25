namespace Extension.Audio
{
    using Extension.Generic.Serializable.Audio;
    using UnityEngine;

    public static class AudioExtension
    {
        /// <summary>
        /// Method to allow the passing of 
        /// AudioSFX Object into the mix so that copy the properties.
        /// </summary>
        /// <param name="audioSetting">AudioSFX settings</param>
        /// <param name="pos">position you want to spawn the audio</param>
        /// <returns></returns>
        public static void PlayClipAtPoint(this AudioSFX audioSetting, Vector3 pos)
        {
            GameObject tempGO = new GameObject("ClipAtPoint " + pos); // create the temp audio object
            tempGO.transform.position = pos; // set its position
            AudioSource tempASource = tempGO.AddComponent<AudioSource>(); // add an audio source to it
            tempASource.clip = audioSetting.clip;
            tempASource.volume = audioSetting.volume;
            tempASource.pitch = audioSetting.pitch;
            tempASource.outputAudioMixerGroup = audioSetting.mixerGroup;
            tempASource.spatialBlend = audioSetting.spatialBlend;

            tempASource.Play(); // play the sound
            Object.Destroy(tempGO, tempASource.clip.length); // destroy the gameobject at clip's end
        }
    }
}
