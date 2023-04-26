namespace CandyCrushREM.Managers.Phases
{
    using CandyCrushREM.Managers;
    using Extension.Patterns.StateMachine;
    using UnityEngine;

    public class SwapPhase : State
    {
        public float SecondsBeforeNextState { get; private set; }

        private TileSlot _firstTile, _secondTile;

        /// <summary>
        /// Initializes a new instance of the FallPhase class with the specified name and duration of the falling.
        /// </summary>
        /// <param name="name">Name of the state.</param>
        /// <param name="secondsBeforeNextState">Time to wait before going to the next state in seconds.</param>
        public SwapPhase(string name, float secondsBeforeNextState) : base(name)
        {
            SecondsBeforeNextState = secondsBeforeNextState;
        }

        /// <inheritdoc/>
        public override void Enter()
        {
#if DEBUG
            Debug.Log("ENTERING SWAP");
#endif
            ResetSelectedTiles();
        }

        /// <inheritdoc/>
        public override void Exit()
        {
        }

        /// <inheritdoc/>
        public override void Process()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

                if (hit.transform)
                {
                    if (hit.transform.TryGetComponent(out TileSlot tile))
                    {
                        if (_firstTile)
                        {
                            _secondTile = tile;

                            if(GameManager.Instance.SwapManager.TrySwap(_firstTile, _secondTile, GameManager.Instance.CurrentTiles))
                            {
                                GameManager.Instance.PhaseManager.NextStateIn(SecondsBeforeNextState);
                            }

                            ResetSelectedTiles();
                        }
                        else
                        {
                            _firstTile = tile;
                        }
                    }
                }
                else
                {
                    ResetSelectedTiles();
                }
            }
        }

        /// <inheritdoc/>
        private void ResetSelectedTiles()
        {
            _firstTile = null;
            _secondTile = null;
        }
    }
}