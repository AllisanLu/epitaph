using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationEvents : MonoBehaviour
{
    [SerializeField] private LevelGeneration levelGeneration;

    public void OnUIFadeoutEnd() {
        levelGeneration = GameObject.Find("LevelGenerator").GetComponent<LevelGeneration>();
        if (levelGeneration != null)
        {
            levelGeneration.PlaceEnemies();
        }
    }

    public void Update() {
        if (levelGeneration == null) {
            levelGeneration = GameObject.Find("LevelGenerator").GetComponent<LevelGeneration>();
        }
    }
}
