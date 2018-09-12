using System;
using UnityEngine;

public class GameConstants: MonoBehaviour {
    
    //Player
    public static class Player {
        public static float Speed;

        public static void Calculate() {
            Speed = 1f;
        }
    }
    
    private void Awake() {
        Player.Calculate();
    }
}
