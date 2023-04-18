namespace CandyCrushREM.Managers
{
    using CandyCrushREM.Candies;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SwapManager : MonoBehaviour
    {
        public int MaxMoves { get; set; }

        [field: SerializeField, Header("Combo Manager")]
        public ComboManager ComboManager { get; private set; }

        [field: SerializeField, Header("Grid Manager")]
        public GridManager GridManager { get; private set; }

        public int CurrentMoves { get; private set; } = 0;

        public Action<int, int> OnMovesChange { get; set; }

        public bool AreMovesOver { get; private set; }

        public bool IsSwapping { get; private set; } = false;

        public void ConsumeMove()
        {
            CurrentMoves++;
            OnMovesChange?.Invoke(CurrentMoves, MaxMoves);
            AreMovesOver = (CurrentMoves >= MaxMoves);
        }

        public bool TrySwap(TileSlot tileA, TileSlot tileB, TileSlot[,] gridTiles)
        {
            if (!IsSwapping)
            {
                bool isValid = IsSwapMoveValid(tileA, tileB, gridTiles);

                if (GridManager.CheckTileAdjacency(tileA, tileB))
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

        private IEnumerator SwapRoutine(TileSlot tileA, TileSlot tileB, TileSlot[,] gridTiles, bool isValid, float duration)
        {
            IsSwapping = true;
            yield return LerpSwap(tileA.AssociatedCandy.transform, tileB.AssociatedCandy.transform, duration);

            if (!isValid)
            {
                yield return LerpSwap(tileB.AssociatedCandy.transform, tileA.AssociatedCandy.transform, duration);
            }

            IsSwapping = false;
            yield break;
        }

        private IEnumerator LerpSwap(Transform transformA, Transform transformB, float duration)
        {
            Vector3 startPosA = transformA.position;
            Vector3 endPosA = transformB.position;

            Vector3 startPosB = transformB.position;
            Vector3 endPosB = transformA.position;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / duration;
                transformA.position = Vector3.Lerp(startPosA, endPosA, t);
                transformB.position = Vector3.Lerp(startPosB, endPosB, t);
                yield return null;
            }
        }

        private bool IsSwapMoveValid(TileSlot tileA, TileSlot tileB, TileSlot[,] gridTiles)
        {
            int beforeComboCount = ComboManager.GetAllCombinations(gridTiles).Count;
            //Debug.Log("BEFORE " + beforeComboCount);

            FastSwap(tileA, tileB);

            int afterComboCount = ComboManager.GetAllCombinations(gridTiles).Count;
            //Debug.Log("AFTER " + afterComboCount);

            FastSwap(tileA, tileB); // Reset the swap

            return beforeComboCount < afterComboCount;
        }

        private void FastSwap(TileSlot tileA, TileSlot tileB)
        {
            Candy toSwap = tileA.AssociatedCandy;

            tileA.AssociatedCandy = tileB.AssociatedCandy;
            tileB.AssociatedCandy = toSwap;

            // No needs to change also the position
        }
    }
}
