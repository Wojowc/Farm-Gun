using UnityEngine;

[CreateAssetMenu(menuName = "AudioData/SFXLib", fileName = "SFXLib")]
public class SFXLib : ScriptableObject
{
    [SerializeField] private AudioClip _testSound;
    public AudioClip TestSound => _testSound;
}
