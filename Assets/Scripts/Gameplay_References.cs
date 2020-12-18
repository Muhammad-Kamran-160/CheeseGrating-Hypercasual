using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay_References : MonoBehaviour
{
    public static Gameplay_References _instance;
    public GameplayManager _gameplayManger;
    public GratedPiecesHandler _gratedPiecesHandler;
    public UIManager _uiManager;
    public SlicesTracker _slicesTracker;
    public MeshSlicerSlicesTracker _meshSlicesTracker;
    public ShopManager _shopManager;
    public SettingsManager _settingsManager;
    public ConfettiManager _confettiManager;
    public SoundManager _soundManager;
    
    private void Awake ()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy (gameObject);
    }
}