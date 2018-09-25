using System;
using UnityEngine;

public class GameConstants: MonoBehaviour {
    
    
    //World
    public static class Wold {
        public static float Gravity = 2000;
        public static float Radius = 10;
    }
    
    //Cilinder
    public static class Cilinder {
        public static float Speed = 8;
    }
    
    //Player
    public static class Player {
        public static float Speed = 3;
        public static float JumpForce = 60;
        public static float JumpTime = 0.5f;
        
        
    }
    
    private void Awake() {
    }
}
