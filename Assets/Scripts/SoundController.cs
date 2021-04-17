using UnityEngine;

public enum SoundGame
{
	Hit,
	Die,
    Point,
    Menu,
	Wing
}

public class SoundController : MonoBehaviour {

	public static SoundController Instance { get; private set; }

	public AudioSource audioSource;
	public AudioClip soundHit;
	public AudioClip soundDie;
	public AudioClip soundPoint;
	public AudioClip soundMenu;
	public AudioClip soundWing;


	void Start() {
		Instance = this;
	}

	public void PlaySound(SoundGame sound)
	{
		var audio = GetAudioClip(sound);
		if (audio != null)
			audioSource.PlayOneShot(audio);
	}

	private AudioClip GetAudioClip(SoundGame soundGame)
    {
        switch (soundGame)
        {
            case SoundGame.Hit:
				return soundHit;
			case SoundGame.Die:
				return soundDie;
            case SoundGame.Point:
				return soundPoint;
			case SoundGame.Menu:
				return soundMenu;
            case SoundGame.Wing:
				return soundWing;
            default:
				return null;
        }
    }
}