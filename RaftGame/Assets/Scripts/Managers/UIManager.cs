using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    void Awake(){
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    void OnDestroy(){
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    [SerializeField] Transform _uiTransform;
    List<UIPanel> panels;
    
    public UIPanel AddPanel(UIPanel newPanel){
        UIPanel _createdPanel = Instantiate(newPanel, _uiTransform); 
        return _createdPanel;
    }

    public void GameManagerOnGameStateChanged(GameManager.GameState newState){
        //Any events here
    }
}
