///
/// Simple pooling for Unity.
///   Author: Martin "quill18" Glaude (quill18@quill18.com)
///   Latest Version: https://gist.github.com/quill18/5a7cfffae68892621267
///   License: CC0 (http://creativecommons.org/publicdomain/zero/1.0/)
///   UPDATES:
/// 	2015-04-16: Changed Pool to use a Stack generic. 


using UnityEngine;
using System.Collections.Generic;
using System;

public static class SimplePool
{
    const int DEFAULT_POOL_SIZE = 3;

    static Dictionary<int, Pool> pools = new Dictionary<int, Pool>();

    static Dictionary<PoolType, GameUnit> poolTypes = new Dictionary<PoolType, GameUnit>();

    private static GameUnit[] gameUnitResources;

    private static Transform root;

    public static Transform Root
    {
        get
        {
            /// Get the root transform for this controller.
            if (root == null)
            {
                root = GameObject.FindObjectOfType<PoolController>().transform;

                /// Creates a new root transform.
                if (root == null)
                {
                    root = new GameObject("Pool").transform;
                }
            }

            return root;
        }
    }

    class Pool
    {
        Transform m_sRoot = null;

        bool m_collect;

        Queue<GameUnit> inactive;

        //collect obj active ingame
        List<GameUnit> active;

        // The prefab that we are pooling
        GameUnit prefab;

        bool m_clamp;
        int m_Amount;

        public bool isCollect { get => m_collect; }

        // Constructor
        public Pool(GameUnit prefab, int initialQty, Transform parent, bool collect, bool clamp)
        {
            inactive = new Queue<GameUnit>(initialQty);
            m_sRoot = parent;
            this.prefab = prefab;
            m_collect = collect;
            this.m_clamp = clamp;
            /// Set active list of game units.
            if (m_collect) active = new List<GameUnit>();
            /// Set the amount of the quantity to initialQty
            if (m_clamp) m_Amount = initialQty;
        }
        public int Count {
            get { return inactive.Count;}
        }
        // Spawn an object from our pool
        /// Spawns a new GameUnit at the specified position and rotation. This is equivalent to calling Spawn with the same parameters but will set the position and rotation of the spawned GameUnit to the specified values rather than relying on the TF. SetPositionAndRotation method.
        /// 
        /// @param pos - The position to set. Must be normalized.
        /// @param rot - The rotation to set. Must be normalized. If this is a non - normalized instance it will be set to the same value
        public GameUnit Spawn(Vector3 pos, Quaternion rot)
        {
            GameUnit obj = Spawn();

            obj.TF.SetPositionAndRotation( pos, rot);

            return obj;
        }

        /// Spawns a new GameUnit from the pool. If there are no inactive units in the pool this will be called
        public GameUnit Spawn()
        {
            GameUnit obj;
            /// Returns the GameUnit that is currently in the inactive list.
            if (inactive.Count == 0)
            {
                obj = (GameUnit)GameObject.Instantiate(prefab, m_sRoot);

                /// Add a new instance to the list of pools.
                if (!pools.ContainsKey(obj.GetInstanceID()))
                    pools.Add(obj.GetInstanceID(), this);
            }
            else
            {
                // Grab the last object in the inactive array
                obj = inactive.Dequeue();

                /// Returns the spawned object.
                if (obj == null)
                {
                    return Spawn();
                }
            }

            /// Add the object to active list
            if (m_collect) active.Add(obj);
            /// Despawn active items if the amount of active items is less than m_Amount
            if (m_clamp && active.Count >= m_Amount) Despawn(active[0]);

            obj.gameObject.SetActive(true);

            return obj;
        }

        // Return an object to the inactive pool.
        /// Despawns ( activates ) a GameUnit. If m_collect is true it will remove the object from the list of active GameUnits
        /// 
        /// @param obj - The GameUnit to despawn
        public void Despawn(GameUnit obj)
        {
            /// Set the game object to inactive.
            if (obj != null /*&& !inactive.Contains(obj)*/)
            {
                obj.gameObject.SetActive(false);
                inactive.Enqueue(obj);
            }

            /// Remove the object from active list
            if (m_collect) active.Remove(obj);
        }

        /// Destroys units that haven't been deactivated. This is useful for situations where you want to clean up a set of inactive units before they're reclaimed.
        /// 
        /// @param amount - The amount of units to destroy ( max = 1
        public void Clamp(int amount) {
            /// This method will remove all GameUnit from the inactive list.
            while(inactive.Count> amount) {
                GameUnit go = inactive.Dequeue();
                GameObject.DestroyImmediate(go);
            }
        }
        /// Releases all inactive units and destroys them in the game. This is called when the game is no longer active
        public void Release() {
            /// This method will remove all GameUnit from the inactive list of GameUnits and destroy them.
            while(inactive.Count>0) {
                GameUnit go = inactive.Dequeue();
                GameObject.DestroyImmediate(go);
            }
            inactive.Clear();
        }

        /// Removes all active s from the pool and reactivates them. Does not return until there are no s
        public void Collect()
        {
            /// Despawn all active items.
            while (active.Count > 0)
            {
                Despawn(active[0]);
            }
        }
    }

    // All of our pools
    static Dictionary<int, Pool> poolInstanceID = new Dictionary<int, Pool>();

    /// Initializes the pool. Must be called before any GameUnit is added to the pool. This is the first method to call in order to ensure that the pool is initialized.
    /// 
    /// @param prefab - The prefab to initialize the pool for.
    /// @param qty - The amount of units to collect in the pool.
    /// @param parent - The parent to assign the pool to. If null a random one will be generated.
    /// @param collect - If true the pool will be collected into the world's pool.
    /// @param clamp - If true the pool will be clamped to the max / min
    static void Init(GameUnit prefab = null, int qty = DEFAULT_POOL_SIZE, Transform parent = null, bool collect = false, bool clamp = false)
    {
        /// Add a pool to the pool.
        if (prefab != null && !IsHasPool(prefab.GetInstanceID()))
        {
            poolInstanceID.Add(prefab.GetInstanceID(), new Pool(prefab, qty, parent, collect, clamp));
        }
    }

    /// Checks if the pool is in use. This is used to prevent duplicate instances from appearing in the pool
    /// 
    /// @param instanceID - Instance ID of the pool
    /// 
    /// @return True if the pool is in use false if it is not in use or has been disposed by the
    static public bool IsHasPool(int instanceID)
    {
        return poolInstanceID.ContainsKey(instanceID);
    }

    /// Preloads a prefab into the pool. This is useful for unit testing and you don't need to worry about pooling your prefabs in your game
    /// 
    /// @param prefab - The prefab to pre - load
    /// @param qty - The number of GameUnits to pre - load
    /// @param parent - The transform to use as the parent of the prefab
    /// @param collect - If true will collect objects that are in the pool
    /// @param clamp - If true will clamp the amount of objects to
    static public void Preload(GameUnit prefab, int qty = 1, Transform parent = null, bool collect = false, bool clamp = false)
    {
        /// Add prefab to poolTypes.
        if (!poolTypes.ContainsKey(prefab.poolType))
        {
            poolTypes.Add(prefab.poolType, prefab);
        }

        /// Returns true if the prefab is null.
        if (prefab == null)
        {
            Debug.LogError(parent.name + " : IS EMPTY!!!");
            return;
        }

        Init(prefab, qty, parent, collect, clamp);

        // Make an array to grab the objects we're about to pre-spawn.
        GameUnit[] obs = new GameUnit[qty];
        /// Spawn the prefab and set the obs to the prefab
        for (int i = 0; i < qty; i++)
        {
            obs[i] = Spawn(prefab);        
        }

        // Now despawn them all.
        /// Despawn all the observations in the game.
        for (int i = 0; i < qty; i++)
        {
            Despawn(obs[i]);
        }
    }

    /// Spawns a GameUnit of the specified pool type. Will try to find a PoolType in the list of PoolTypes
    /// 
    /// @param poolType - The type of pool to spawn
    /// @param pos - The position to spawn the GameUnit at
    /// @param rot - The rotation to spawn the GameUnit at ( must be normalized
    static public T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        return Spawn(GetGameUnitByType(poolType), pos, rot) as T;
    }
    
    /// @param poolType
    static public T Spawn<T>(PoolType poolType) where T : GameUnit
    {
        return Spawn<T>(GetGameUnitByType(poolType));
    }

    /// Spawns a GameUnit and returns it. You can use this to set properties that are shared between the spawned and unoccupied GameUnits
    /// 
    /// @param obj - The GameUnit to spawn.
    /// @param pos - The position to spawn the GameUnit at.
    /// @param rot - The rotation to spawn the GameUnit at. This is used to ensure that the Unit is in the world
    static public T Spawn<T>(GameUnit obj, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        return Spawn(obj, pos, rot) as T;
    }

    /// Spawns a GameUnit of the specified type. This is a convenience method that delegates to Spawn
    /// 
    /// @param obj - The GameUnit to spawn
    static public T Spawn<T>(GameUnit obj) where T : GameUnit
    {
        return Spawn(obj) as T;
    }

    /// Spawns a GameUnit in the pool. This is useful for unit creation in cases where you don't have a reference to the object in the pool
    /// 
    /// @param obj - The GameUnit to spawn.
    /// @param pos - The position to spawn the GameUnit at.
    /// @param rot - The rotation to spawn the GameUnit at. Note that this can be different from the position that was passed to the constructor
    static public GameUnit Spawn(GameUnit obj, Vector3 pos, Quaternion rot)
    {
        /// Preloads the pool instance. If poolInstanceID is not already in poolInstanceID this method will create a new root transform and preload it.
        if (!poolInstanceID.ContainsKey(obj.GetInstanceID()))
        {
            Transform newRoot = new GameObject(obj.name).transform;
            newRoot.SetParent(Root);
            Preload(obj, 1, newRoot, true, false);
        }

        return poolInstanceID[obj.GetInstanceID()].Spawn(pos, rot);
    }

    /// Spawns a GameUnit in the pool. This will preload the object if it's not already loaded
    /// 
    /// @param obj - The GameUnit to spawn
    public static GameUnit Spawn(GameUnit obj)
    {
        /// Preloads the pool instance. If poolInstanceID is not already in poolInstanceID this method will create a new root transform and preload it.
        if (!poolInstanceID.ContainsKey(obj.GetInstanceID()))
        {
            Transform newRoot = new GameObject(obj.name).transform;
            newRoot.SetParent(Root);
            Preload(obj, 1, newRoot, true, false);
        }

        return poolInstanceID[obj.GetInstanceID()].Spawn();
    }

    /// Despawns the specified object. This is a destructive operation. If the object is not active it destroys the object
    /// 
    /// @param obj - The GameUnit to despawn
    static public void Despawn(GameUnit obj)
    {
        /// Despawn the game object.
        if (obj.gameObject.activeSelf)
        {
            /// Despawn the object and destroy it.
            if (pools.ContainsKey(obj.GetInstanceID()))
                pools[obj.GetInstanceID()].Despawn(obj);
            else
                GameObject.Destroy(obj.gameObject);    
        }
    }

    /// Releases the GameUnit back to the pool. If the Pool doesn't exist it destroys the GameUnit
    /// 
    /// @param obj - The GameUnit to release
    static public void Release(GameUnit obj)
    {
        /// Release the Game Object and remove it from the pool.
        if (pools.ContainsKey(obj.GetInstanceID()))
        {
            pools[obj.GetInstanceID()].Release();
            pools.Remove(obj.GetInstanceID());
        }
        else
        {
            GameObject.DestroyImmediate(obj);
        }
    }

    /// Collects the pool of instances. This is called when a GameUnit is destroyed and should no longer be used.
    /// 
    /// @param obj - The GameUnit to be garbage collected. Mustn't be null
    static public void Collect(GameUnit obj)
    {
        /// Collect all instances of the pool
        if (poolInstanceID.ContainsKey(obj.GetInstanceID()))
            poolInstanceID[obj.GetInstanceID()].Collect();
    }

    /// Collects all pools that are marked as collect. This is useful for unit tests that don't need to run
    static public void CollectAll()
    {
        foreach (var item in poolInstanceID)
        {
            /// Collect the value of the item.
            if (item.Value.isCollect)
            {
                item.Value.Collect();
            }
        }
    }

    /// Gets the game unit by type. This is used to determine if there is a pool with the given type and if so returns it
    /// 
    /// @param poolType - Type of the pool
    static GameUnit GetGameUnitByType(PoolType poolType)
    {
        /// Load all the game units from the pool.
        if (gameUnitResources == null || gameUnitResources.Length == 0)
        {
            gameUnitResources = Resources.LoadAll<GameUnit>("Pool");
        }

        /// Adds a pool type to the pool type poolType.
        if (!poolTypes.ContainsKey(poolType) || poolTypes[poolType] == null)
        {
            GameUnit unit = null;

            /// Find the game unit that is in the pool type.
            for (int i = 0; i < gameUnitResources.Length; i++)
            {
                /// Get the game unit from the pool type.
                if (gameUnitResources[i].poolType == poolType)
                {
                    unit = gameUnitResources[i];
                    break;
                }
            }

            poolTypes.Add(poolType, unit);
        }

        return poolTypes[poolType];
    }
}

[System.Serializable]
public class PoolAmount
{
    [Header("-- Pool Amount --")]
    public Transform root;
    public GameUnit prefab;
    public int amount;
    public bool collect;
    public bool clamp;
}

public enum IngameType 
{ 
    PLAYER, 
    ENEMY,
    None,
    HpBar,
}


public enum PoolType
{
    Bot,
    Brick,
}
