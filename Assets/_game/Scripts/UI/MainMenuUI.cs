using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private  Button _playButton;

    public Button PlayButton => _playButton;
    
    private void OnValidate()
    {
        if(_playButton == null)
            throw  new System.NullReferenceException($"{nameof(MainMenuUI)} doesn`t have {nameof(_playButton)} link");
    }
}