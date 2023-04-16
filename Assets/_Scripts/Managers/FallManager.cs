namespace CandyCrushREM.Managers
{
    using System.Collections;
    using CandyCrushREM.Candies;
    using CandyCrushREM.Managers.Grid;
    using CandyCrushREM.SO;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.UI;

    public class FallManager : MonoBehaviour
    {
        [field: SerializeField]
        public SO_Candy[] PossibleCandiesSpawn {  get; private set; }

        public bool AreFalling { get; private set; }

        private void Awake()
        {
            DisableCandiesFall();
        }

        public void LetCandiesFallFor(float duration)
        {
            StartCoroutine(FallRoutine(duration));
        }

        public void RefillGridWithCandies(TileSlot[,] gridTiles)
        {
            foreach (TileSlot tile in gridTiles)
            {
                if(!tile.AssociatedCandy)
                {
                    Debug.Log($"Tile {tile.transform.name} doesnt have a candy associated");

                    SO_Candy spawnedCandy = PossibleCandiesSpawn[Random.Range(0, PossibleCandiesSpawn.Length)];

                    if (Instantiate(spawnedCandy.candyBase, tile.transform.position, Quaternion.identity, tile.transform).TryGetComponent(out Candy candy))
                    {
                        candy.Init(spawnedCandy);
                        tile.AssociatedCandy = candy;
                    }
                }
            }
        }

        private IEnumerator FallRoutine(float time)
        {
            EnableCandiesFall();
            AreFalling = true;
            yield return new WaitForSeconds(time);
            AreFalling = false;
            DisableCandiesFall();
        }

        private void EnableCandiesFall()
        {
            AreFalling = true;
            EnableCandiesPhysics();
        }

        private void DisableCandiesFall()
        {
            AreFalling = false;
            DisableCandiesPhysics();
        }

        private void DisableCandiesPhysics()
        {
            Candy[] candies = FindObjectsByType<Candy>(FindObjectsSortMode.None);

            foreach (Candy cand in candies) 
            {
                if (cand.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                {
                    rb.simulated = false;
                }
            }
        }

        private void EnableCandiesPhysics()
        {
            Candy[] candies = FindObjectsByType<Candy>(FindObjectsSortMode.None);

            foreach (Candy cand in candies)
            {
                if (cand.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                {
                    rb.simulated = true;
                }
            }
        }

    }
}
