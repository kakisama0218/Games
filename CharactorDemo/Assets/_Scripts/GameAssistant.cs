using Mojiex;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssistant
{
    private GameAssistant() { Initial(); }

    public static GameAssistant Instance => _instance ??= new GameAssistant();
    public static GameAssistant _instance;

    public string CurrentProfileId { get; private set; } = "";
    public DataModel CurrentData { get; private set; }
    private void Initial()
    {
        
    }
}
