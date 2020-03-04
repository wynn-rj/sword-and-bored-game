using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Strategy.TimeSystem.Subscribers;
using SwordAndBored.Utilities.Debug;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace SwordAndBored.Strategy.Movement
{
    public abstract class CreatureMovementController : MonoBehaviour, ITimeStepSubscriber
    {
        protected class LastQ<T> : Queue<T>
        {
            public T Last { get; private set; }

            public new void Enqueue(T item)
            {
                Last = item;
                base.Enqueue(item);
            }

            public new void Clear()
            {
                Last = default;
                base.Clear();
            }

            public new T Dequeue()
            {
                if (Count == 1)
                {
                    Last = default;
                }
                return base.Dequeue();
            }
        }

        private const float SPEED = 10;

        protected readonly LastQ<IHexGridCell> path = new LastQ<IHexGridCell>();
        protected CreatureComponent creatureComponent;

        [SerializeField] private int distanceCanTravel;
        private Vector3? targetPosition;
        private float height;
        private static readonly object nextLocationLock = new object();

        public IHexGridCell StartLocation { private get; set; }

        public IHexGridCell Location { get; protected set; }

        protected abstract int ResetMovement();

        public virtual void PreTimeStepUpdate()
        {
            distanceCanTravel = ResetMovement();
        }

        public virtual void PostTimeStepUpdate()
        {
            TraversePath();
            while (!(targetPosition is null)) ;
        }

        protected virtual void TraversePath()
        {
            if (distanceCanTravel == 0 || path.Count == 0)
            {
                targetPosition = null;
                return;
            }

            if (!SafeUpdateLocation(path.Dequeue()))
            {
                Debug.Log("Movement blocked");
                return;
            }
            distanceCanTravel--;
            targetPosition = Location.Position.CenterAsVector3(height);
        }

        protected virtual void Start()
        {
            AssertHelper.Assert(StartLocation != null, "No start location given", this);
            AssertHelper.Assert(!StartLocation.HasComponent<CreatureComponent>(),
                "Tried to place creature on creature at " + StartLocation.Position, this);
            bool success = UpdateLocation(StartLocation);
            AssertHelper.Assert(success, "Failed to set start location", this);
            targetPosition = null;
            height = transform.position.y;
            distanceCanTravel = ResetMovement();
        }

        protected virtual void Update()
        {
            if (!(targetPosition is null))
            {
                MoveTowardsTarget();
            }
        }

        protected virtual bool UpdateLocation(IHexGridCell location)
        {
            if (location.HasComponent<CreatureComponent>())
            {
                return false;
            }
            if (Location != null)
            {
                AssertHelper.Assert(creatureComponent != null, "Missing creature component", this);
                bool success = Location.RemoveComponent(creatureComponent);
                AssertHelper.Assert(success, "Failed to remove creature component", this);
            }
            else
            {
                creatureComponent = new CreatureComponent(this);
            }
            Location = location;
            Location.AddComponent(creatureComponent);
            return true;
        }

        protected void OnDestroy()
        {
            Location.RemoveComponent(creatureComponent);
        }

        protected virtual void ArrivedAtNewLocation() { }

        private void MoveTowardsTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.Value, SPEED * Time.deltaTime);
            transform.LookAt(targetPosition.Value);

            if (transform.position == targetPosition)
            {
                ArrivedAtNewLocation();
                TraversePath();
            }
        }

        private bool SafeUpdateLocation(IHexGridCell location)
        {
            Monitor.Enter(nextLocationLock);
            try
            {
                return UpdateLocation(location);
            }
            finally
            {
                Monitor.Exit(nextLocationLock);
            }
        }
    }
}
