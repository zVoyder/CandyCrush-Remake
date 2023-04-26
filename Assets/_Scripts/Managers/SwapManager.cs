namespace CandyCrushREM.Managers
{
    using CandyCrushREM.Candies;
    using System;
    using System.Collections;
    using Extension.Transform;
    using UnityEngine;

    public class SwapManager : MonoBehaviour
    {
        public int MaxMoves { get; private set; }

        [field: SerializeField, Header("Combo Manager")]
        public ComboManager ComboManager { get; private set; }

        [field: SerializeField, Header("Grid Manager")]
        public GridManager GridManager { get; private set; }

        public int CurrentMoves { get; private set; } = 0;

        public Action<int, int> OnMovesChange { get; set; }

        public bool AreMovesOver { get; private set; }

        public bool IsSwapping { get; private set; } = false;

        /// <summary>
        /// Initializes the SwapManager class.
        /// </summary>
        /// <param name="maxMoves">Maximum allowed swap moves.</param>
        public void Init(int maxMoves)
        {
            MaxMoves = maxMoves;
        }

        /// <summary>
        /// Uses a swap move.
        /// </summary>
        public void ConsumeMove()
        {
            CurrentMoves++;
            OnMovesChange?.Invoke(CurrentMoves, MaxMoves);
            AreMovesOver = (CurrentMoves >= MaxMoves);
        }

        /// <summary>
        /// Tries to swap two candies.
        /// </summary>
        /// <param name="tileA">TileSlot of the first candy.</param>
        /// <param name="tileB">TileSlot of the second candy.</param>
        /// <param name="gridTiles">Grid of TileSlots.</param>
        /// <returns>True if the swap succeded, False if not.</returns>
        public bool TrySwap(TileSlot tileA, TileSlot tileB, TileSlot[,] gridTiles)
        {
            if (!IsSwapping)
            {
                bool isValid = IsSwapMoveValid(tileA, tileB, gridTiles);

                if (GridManager.CheckTileAdjacency(tileA.Position, tileB.Position))
                {
                    StartCoroutine(SwapRoutine(tileA, tileB, gridTiles, isValid, .2f));

                    if (isValid)
                    {
                        ConsumeMove();

                        // Swapping the candies
                        Candy candyA = tileA.AssociatedCandy;
                        Candy candyB = tileB.AssociatedCandy;
                        tileA.AssociatedCandy = candyB;
                        tileB.AssociatedCandy = candyA;

                        // Change the parent of the first candy's transform
                        candyA.transform.SetParent(tileB.transform);
                        candyB.transform.SetParent(tileA.transform);
                    }
                }

                return isValid;
            }

            return false;
        }

        /// <summary>
        /// Swap Candies Coroutine.
        /// </summary>
        /// <param name="tileA">From Tile.</param>
        /// <param name="tileB">To Tile</param>
        /// <param name="gridTiles">Grid of TileSlot.</param>
        /// <param name="isValid">Swap move validity.</param>
        /// <param name="duration">Swap duration in seconds.</param>
        /// <returns></returns>
        private IEnumerator SwapRoutine(TileSlot tileA, TileSlot tileB, TileSlot[,] gridTiles, bool isValid, float duration)
        {
            IsSwapping = true;
            yield return tileA.AssociatedCandy.transform.LerpSwitchPosition(tileB.AssociatedCandy.transform, duration);

            if (!isValid)
            {
                yield return tileB.AssociatedCandy.transform.LerpSwitchPosition(tileA.AssociatedCandy.transform, duration);
            }

            IsSwapping = false;
            yield break;
        }

        /// <summary>
        /// Checks if a swap move is valid.
        /// </summary>
        /// <param name="tileA">Tile A.</param>
        /// <param name="tileB">Tile B.</param>
        /// <param name="gridTiles">Grid of TileSlot to check.</param>
        /// <returns>True if it is valid, False if not.</returns>
        private bool IsSwapMoveValid(TileSlot tileA, TileSlot tileB, TileSlot[,] gridTiles)
        {
            int beforeComboCount = ComboManager.GetAllCombinations(gridTiles).Count;
            InstantTemporarySwap(tileA, tileB);
            int afterComboCount = ComboManager.GetAllCombinations(gridTiles).Count;
            InstantTemporarySwap(tileA, tileB); // Reset the swap

            return beforeComboCount < afterComboCount;
        }

        /// <summary>
        /// Instantly swaps to candies.
        /// </summary>
        /// <param name="tileA">Tile A.</param>
        /// <param name="tileB">Tile B.</param>
        private void InstantTemporarySwap(TileSlot tileA, TileSlot tileB)
        {
            Candy toSwap = tileA.AssociatedCandy;
            tileA.AssociatedCandy = tileB.AssociatedCandy;
            tileB.AssociatedCandy = toSwap;

            // No needs to also change the position
        }
    }
}
