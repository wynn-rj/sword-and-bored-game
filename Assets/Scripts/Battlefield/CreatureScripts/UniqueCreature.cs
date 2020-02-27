using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SwordAndBored.Battlefield.CreaturScripts {
    public class UniqueCreature : UnitBase
    {
        [Header("Material Info")]
        public Material[] mat;
        [HideInInspector]
        public Renderer currentMat;
        float a = .05f;
        float b;
        int highlightColor;
        [HideInInspector]
        public UnitAbilitiesContainer abilityContainer;
        [HideInInspector]
        public UnitStats stats;
        [Header("Virtual Camera Info")]
        public CinemachineVirtualCamera currentCamera;
        public Animator animator;
        Outline outline;
        [HideInInspector]
        BrainManager brain;
        public bool isEnemy;
        [HideInInspector]
        public GridHolder gridHolder;
        bool start = true;
        bool onMoveTile = false;

        //Used for astar
        private List<Tile> path;
        private int tileOnPath = 0;


        void Start()
        {
            health = maxHealth;
            brain = GetComponent<BrainManager>();
            currentMat = GetComponent<Renderer>();
            abilityContainer = GetComponent<UnitAbilitiesContainer>();
            stats = GetComponent<UnitStats>();
            outline = GetComponent<Outline>();
        }

        /// <summary>
        /// This method chnages the highlighting of a unit based on an integr.  3 = no highlight, 1 = blue, 2 = red.
        /// When highlighting a unit dont forget to set their material back to normal using glow(3). 
        /// This function might be removed in the future in favor of using a free store asset. 
        /// </summary>
        public void Glow(int glow)
        {
            highlightColor = glow;
            if (glow == 1)
            {
                outline.OutlineColor = Color.blue;
                outline.enabled = true;
            } else if (glow == 2)
            {
                outline.enabled = false;
            } else if (glow == 3) 
            {
                b = a + Time.time;
                outline.OutlineColor = Color.red;
                outline.enabled = true;
            }
        }

        public void hightlight()
        {
            b = a + Time.time;
            outline.OutlineColor = Color.red;
            outline.enabled = true;
        }

        private void LateUpdate()
        {
            if (start)
            {
                currentTile = gridHolder.tiles[Mathf.RoundToInt(brain.startCoordinates.x), Mathf.RoundToInt(brain.startCoordinates.y)];
                Move(currentTile);
                start = false;
            }
            
        }

        private void Update()
        {
            if (!brain.isMyTurn && Time.time > b)
            {
                outline.enabled = false;
            }

            if (path != null) MoveAlongPath();

            animator.SetFloat("Speed", (agent.velocity.magnitude / 3.5f));
        }

        private void MoveAlongPath()
        {

            Move(path[tileOnPath]);
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

        bool onTile(float marginOfError)
        {
            bool onTile = Mathf.Abs(transform.position.x - currentTile.GetCenterOfTile().x) < marginOfError && Mathf.Abs(transform.position.z - currentTile.GetCenterOfTile().z) < marginOfError;
            return onTile;
        }

        /// <summary>
        /// This method is used to move a unit to a tile.  This does not use Astar        
        /// </summary>
        public void Move(Tile goTile)
        {
                MoveTo(goTile.GetCenterOfTile());
                SetTile(goTile);
        }


        public void FollowPath(List<Tile> path)
        {
            tileOnPath = path.Count - 1;
            this.path = path;
        }
    }

}
