using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class MazeGenerator: MonoBehaviour {
    public static Maze GeneratedMaze;
    
    public GameObject pipe;
    public GameObject turningZone;

    private void Awake() {
        var maze = GenerateMaze(15);
        InstantiateMaze(maze);
    }

    public static Maze GenerateMaze(int difficulty) {
        var maze = new Maze(difficulty, difficulty);
        try {
            FillForward(maze, difficulty, -1);
            addTurningBlokcs(maze);
        } catch (Exception e) {
            Debug.LogError(e);
            throw;
        }

        return maze;
    }

    private static void FillUp(Maze maze) {
        for (int i = 0; i < maze.Rows; i++) {
            for (int j = 0; j < maze.Columns; j++) {
                if (i % 2 == 0) {
                    if (j % 2 == 1) {
                        maze[i, j] = Maze.MazePart.Turning;
                    } else {
                        maze[i, j] = Maze.MazePart.Pipe;
                    }
                } else {
                    if (j % 2 == 0) {
                        maze[i, j] = Maze.MazePart.Pipe;
                    } else {
                        maze[i, j] = Maze.MazePart.None;
                    }
                }
            }
        }
    }
    
    private static void FillForward(Maze maze, int difficulty, int seed) {
        List<int> borders = new List<int>();

        var amount = difficulty * difficulty * 0.5 * 0.15;

        var initial = (difficulty - 1) / 2 * maze.Rows + (difficulty / 2) % maze.Rows;
        if (difficulty % 2 != 0) {
            initial++;
        }
        borders.Add(initial);
        if(seed < 0) {
            seed = new Random().Next();
        }

        var rand = new Random(seed);
        Debug.Log("Created with seed:" + seed);
        for (int i = 0; i < amount && borders.Count>0; i++) {
            int index =  borders.Count - 1;
            while (rand.NextDouble() < 0.85 && index >= 0) {
                index-=2;
            }

            if (index < 0) {
                index = 0;
            }

            var current = borders.ElementAt(index);
            borders.RemoveAt(index);
            Debug.Log("current" + current/maze.Rows + " , " + current%maze.Rows);
            maze[current] = Maze.MazePart.Pipe;
            var neighbours = maze.UnvisitedNeighbours(current).ToList();
            Shuffle(neighbours, rand);
            neighbours = neighbours.GetRange(0, neighbours.Count / 3);
            foreach (var neighbour in neighbours) {
                borders.Add(neighbour);
            }
        }
        
    }
    
    public static void Shuffle<T>(IList<T> list, Random rng)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }
    }

    private static void addTurningBlokcs(Maze maze) {
        var directions = new int[,] {{-1, 0}, {0, -1}, {1, 0}, {0, 1}};

        for (int i = 1; i < maze.Rows; i+=2) {
            for (int j = 1; j < maze.Columns; j+=2) {
                int connections = 0;
                for (int k = 0; k < 4; k++) {
                    var newRow = i + directions[k, 0];
                    var newColumn = j + directions[k, 1];
                    if (!maze.isInside(newRow, newColumn)) {
                        continue;
                    }

                    if (maze[newRow, newColumn] == Maze.MazePart.Pipe) {
                        connections++;
                    }
                }

                if (connections > 1) {
                    maze[i, j] = Maze.MazePart.Turning;
                }
            }
        }
        
        Debug.Log("DONE");
    } 

    public void InstantiateMaze(Maze maze) {
        for (int i = 0; i < maze.Rows; i++) {
            for (int j = 0; j < maze.Columns; j++) {
                var value = maze[i, j];
                int xPos, yPos;
                switch (value) {
                        case Maze.MazePart.Pipe:
                            xPos = (i+1)/2*80;
                            if (i % 2 == 0) {
                                xPos += 15;
                            }

                            yPos = (j+1)/2*80;
                            if (j % 2 == 0) {
                                yPos += 15;
                            }

                            Quaternion rotation;
                            if (i % 2 == 0) {
                                rotation = Quaternion.Euler(0, 90, 0);

                            } else {
                                rotation = Quaternion.identity;
                            }
                            var newPipe = Instantiate(this.pipe, new Vector3(xPos, 0, yPos),rotation);
                            var part = newPipe.GetComponent<MazePart>();
                            part.i = i;
                            part.j = j;

                            break;
                        
                        case Maze.MazePart.Turning:
                            xPos = (i+1)/2*80;
                            if (i % 2 == 0) {
                                xPos += 15;
                            }

                            yPos = (j+1)/2*80;
                            if (j % 2 == 0) {
                                yPos += 15;
                            }
                            var go = Instantiate(turningZone, new Vector3(xPos, 0, yPos), Quaternion.identity);
                            var parti = go.GetComponent<MazePart>();
                            parti.i = i;
                            parti.j = j;
                            var zone = go.GetComponent<TurningZone>();
                            
                            var directions = new int[,] {{-1, 0}, {0, -1}, {1, 0}, {0, 1}};

                            zone.BackOpen = false;
                            zone.LeftOpen = false;
                            zone.FrontOpen = false;
                            zone.RightOpen = false;
                                    for (int k = 0; k < 4; k++) {
                                        var newRow = i + directions[k, 0];
                                        var newColumn = j + directions[k, 1];
                                        
                                        if (!maze.isInside(newRow, newColumn)) {
                                            continue;
                                        }

                                        switch (k) {
                                            case 0:
                                                if (maze[newRow, newColumn] == Maze.MazePart.Pipe) {
                                                    zone.LeftOpen = true;
                                                }
                                                break;
                                            
                                            case 1:
                                                if (maze[newRow, newColumn] == Maze.MazePart.Pipe) {
                                                    zone.BackOpen = true;
                                                }
                                                break;
                                            
                                            case 2:
                                                if (maze[newRow, newColumn] == Maze.MazePart.Pipe) {
                                                    zone.RightOpen = true;
                                                }
                                                break;
                                            
                                            case 3:
                                                if (maze[newRow, newColumn] == Maze.MazePart.Pipe) {
                                                    zone.FrontOpen = true;
                                                }
                                                break;
                                        }
                            }
                            break;
                }
            }
        }
    }
}
