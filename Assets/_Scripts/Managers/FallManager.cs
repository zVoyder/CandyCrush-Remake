namespace CandyCrushREM.Managers
{
    using System.Collections;
    using CandyCrushREM.Candies;
    using CandyCrushREM.Pools;
    using CandyCrushREM.SO;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.UI;

    public class FallManager : MonoBehaviour
    {
        [field: SerializeField, Header("Pools")]
        public CandyPool _candyPool;

        [field: SerializeField]
        public SO_Candy[] PossibleCandiesSpawn {  get; private set; }

        public bool AreFalling { get; private set; }

        /// <summary>
        /// Lets the candies falls for N seconds.
        /// </summary>
        /// <param name="duration">Duration in seconds.</param>
        /// <param name="tiles">Grid of TileSlot.</param>
        public void LetCandiesFallFor(TileSlot[,] tiles, float duration)
        {
            StartCoroutine(FallRoutine(tiles, duration));
        }

        /// <summary>
        /// Refills the cells of the grid that do not have an associated candy.
        /// </summary>
        /// <param name="gridTiles">Grid to operate.</param>
        /// <param name="tiles">Grid of TileSlot.</param>
        public void RefillGridWithCandies(TileSlot[,] gridTiles)
        {
            foreach (TileSlot tile in gridTiles)
            {
                if(!tile.AssociatedCandy)
                {
                    SO_Candy spawnedCandy = PossibleCandiesSpawn[Random.Range(0, PossibleCandiesSpawn.Length)];

                    if (_candyPool.Get(tile.transform).TryGetComponent(out Candy candy))
                    {
                        candy.Init(spawnedCandy, _candyPool);
                        tile.AssociatedCandy = candy;
                    }
                }
            }
        }

        /// <summary>
        /// Candies fall routine.
        /// </summary>
        /// <param name="tiles">Grid of TileSlot.</param>
        /// <param name="time">Falling time in seconds.</param>
        /// <returns></returns>
        private IEnumerator FallRoutine(TileSlot[,] tiles, float time)
        {
            EnableCandiesFall(tiles);
            AreFalling = true;
            yield return new WaitForSeconds(time);
            AreFalling = false;
            DisableCandiesFall(tiles);
        }

        /// <summary>
        /// Enable the candies fall.
        /// </summary>
        /// <param name="tiles">Grid of TileSlot.</param>
        private void EnableCandiesFall(TileSlot[,] tiles)
        {
            AreFalling = true;
            SetActiveCandiesPhysics(tiles, true);
        }

        /// <summary>
        /// Disable the candies fall.
        /// </summary>
        /// <param name="tiles">Grid of TileSlot.</param>
        private void DisableCandiesFall(TileSlot[,] tiles)
        {
            AreFalling = false;
            SetActiveCandiesPhysics(tiles, false);
        }

        /// <summary>
        /// Sets active true or false the physic simulation of the candies.
        /// </summary>
        /// <param name="tiles">Grid of TileSlot.</param>
        /// <param name="enabled">Enable if True, Disable if false.</param>
        private void SetActiveCandiesPhysics(TileSlot[,] tiles, bool enabled)
        {
            foreach (TileSlot tile in tiles) 
            {
                if (tile.AssociatedCandy)
                {
                    if (tile.AssociatedCandy.TryGetComponent(out Rigidbody2D rb))
                    {
                        rb.simulated = enabled;
                    }
                }
            }
        }

    }
}
