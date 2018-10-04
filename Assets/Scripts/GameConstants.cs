using System;
using UnityEngine;

public class GameConstants: MonoBehaviour {

    public float DifficultyLevel = 0f;
    private static GameConstants _instance = null;
    public static GameConstants Instance
    {
        get { return _instance ?? (_instance = new GameConstants()); }
        private set { _instance = value; }
    }
    
    //World
    public static class Wold {
        public static float Gravity = 2000;
        public static float Radius = 10;
    }
    
    //Cilinder
    public static class Cilinder {
        public static float Speed = 80;
        public static float CilinderDensity = 0.5f;
        public static float CilinderShieldOdds = 0.05f;
        public static float BoostOdds = 0.03f;
        public static float SpawnRate = 3f;
    }
    
    //Player
    public static class Player {
        public static float Speed = 3;
        public static float JumpForce = 60;
        public static float JumpTime = 0.6f;
        
        
    }
    
    private void Awake() {
        Player.JumpTime = 0.6f;
        Cilinder.Speed = 80;
        Cilinder.SpawnRate = 3;
        Player.Speed = 3;
    }
    
    
    private void Update() {
        float minJump = 0.2f;
        float maxJump = 0.6f;

        float minCSpeed = 80f;
        float maxCSpeed = 200f;

        float minSpawnRate = 0.83f;
        float maxSpawnRate = 3f;

        float minPSpeed = 3f;
        float maxPSpeed = 7.5f;
        Debug.Log(Player.Speed);
//
//        Player.JumpTime = Mathf.Clamp(Player.JumpTime - DifficultyLevel*Time.deltaTime *(maxJump-minJump)/100000, minJump,maxJump);
//        Cilinder.Speed = Mathf.Clamp(Cilinder.Speed + DifficultyLevel*Time.deltaTime *(maxCSpeed - minCSpeed)/100000, minCSpeed, maxCSpeed);
//        Cilinder.SpawnRate = Mathf.Clamp(Cilinder.SpawnRate + DifficultyLevel*Time.deltaTime * (maxSpawnRate - minSpawnRate)/100000, minSpawnRate, maxSpawnRate);
//        Player.Speed = Mathf.Clamp(Cilinder.Speed + DifficultyLevel*Time.deltaTime *(maxPSpeed - minPSpeed)/100000, minPSpeed, maxPSpeed);
//        
        
        Player.JumpTime = Mathf.Lerp(Player.JumpTime, minJump, DifficultyLevel*Time.deltaTime);
        Cilinder.Speed = Mathf.Lerp(Cilinder.Speed, maxCSpeed, DifficultyLevel*Time.deltaTime);
        Cilinder.SpawnRate = Mathf.Lerp(Cilinder.SpawnRate, minSpawnRate, DifficultyLevel*Time.deltaTime);
        Player.Speed = Mathf.Lerp(Player.Speed, maxPSpeed, DifficultyLevel*Time.deltaTime);
        
    }
}
