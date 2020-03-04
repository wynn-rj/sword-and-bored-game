using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SwordAndBored.Battlefield.AstarStuff;
using SwordAndBored.Battlefield.CreaturScripts;
using UnityEngine.EventSystems;

namespace SwordAndBored.Battlefield.MovementSystemScripts
{
    public class MovementSystem : MonoBehaviour
    {

        [HideInInspector]
        public Tile[,] grid;
        bool start = true;
        bool onMoveTile = false;
        LineRenderer lr;
        private List<Tile> path;
        private int tileOnPath = 0;

        AStar star = new AStar();
        DisplayPath show = new DisplayPath();
        protected BrainManager brain;
        [HideInInspector]
        public NavMeshAgent agent;
        [HideInInspector]
        public Tile currentTile;

        [HideInInspector]
        public Tile LeftTile;
        [HideInInspector]
        public Tile RightTile;
        [HideInInspector]
        public Tile UpTile;
        [HideInInspector]
        public Tile DownTile;


        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }


        void Start()
        {
            brain = GetComponent<BrainManager>();
            lr = GetComponent<LineRenderer>();
        }


        void Update()
        {
            if (path != null)
            {
                MoveAlongPath();
            }
            else
            {
                lr.enabled = false;
            }
        }

        private void LateUpdate()
        {
            if (start)
            {
                currentTile = grid[Mathf.RoundToInt(brain.startCoordinates.x), Mathf.RoundToInt(brain.startCoordinates.y)];
                if (Mathf.RoundToInt(brain.startCoordinates.x - 1) >= 0)
                {
                    LeftTile = grid[Mathf.RoundToInt(brain.startCoordinates.x - 1), Mathf.RoundToInt(brain.startCoordinates.y)];
                }
                if (Mathf.RoundToInt(brain.startCoordinates.x + 1) < grid.Length)
                {
                    RightTile = grid[Mathf.RoundToInt(brain.startCoordinates.x + 1), Mathf.RoundToInt(brain.startCoordinates.y)];
                }
                if (Mathf.RoundToInt(brain.startCoordinates.y - 1) >= 0)
                {
                    DownTile = grid[Mathf.RoundToInt(brain.startCoordinates.x), Mathf.RoundToInt(brain.startCoordinates.y - 1)];
                }
                if (Mathf.RoundToInt(brain.startCoordinates.y) < grid.Length)
                {
                    UpTile = grid[Mathf.RoundToInt(brain.startCoordinates.x), Mathf.RoundToInt(brain.startCoordinates.y + 1)];
                }
                MoveOneTile(currentTile);
                start = false;
            }

        }

        private void MoveAlongPath()
        {
            MoveOneTile(path[tileOnPath]);
            onMoveTile = onTile(.1f);
            if (path != null && onMoveTile && tileOnPath > 0)
            {
                tileOnPath--;
            }
            else if (path != null && onMoveTile && tileOnPath == 0)
            {
                path = null;
            }
        }
        
        private void MoveOneTile(Tile goTile)
        {
            MoveTo(goTile.GetCenterOfTile());
            SetTile(goTile);
        }


        private void FollowPath(List<Tile> path)
        {
            tileOnPath = path.Count - 1;
            this.path = path;
        }

        private bool onTile(float marginOfError)
        {
            bool onTile = Mathf.Abs(transform.position.x - currentTile.GetCenterOfTile().x) < marginOfError && Mathf.Abs(transform.position.z - currentTile.GetCenterOfTile().z) < marginOfError;
            return onTile;
        }

        private void MoveTo(Vector3 pos)
        {
            agent.destination = pos;
        }

        private void SetTile(Tile tile)
        {
            currentTile.unitOnTile = null;
            currentTile = tile;
            tile.unitOnTile = this.gameObject;
        }


        public void Move(Tile tile, bool displayPath)
        {
            if(Vector3.Distance(currentTile.transform.position, tile.transform.position) <= brain.GetComponent<UniqueCreature>().stats.movement + .3f)
            {
                Debug.Log("Within movement");
                List<Tile> path = star.FindPath(tile, grid, this);
                if (displayPath)
                {
                    lr.enabled = true;
                    show.Display(lr, path);
                }
                FollowPath(path);
            } else
            {
                Debug.Log("Not within movement");
            }

        }

        public void Move(Tile tile)
        {
            Move(tile, false);
        }
    }
}
