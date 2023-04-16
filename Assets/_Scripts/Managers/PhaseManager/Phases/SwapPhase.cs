namespace CandyCrushREM.Managers.Phases
{
    using CandyCrushREM.Managers.Grid;
    using Extension.Patterns.StateMachine;
    using UnityEngine;

    public class SwapPhase : State
    {
        public float SecondsBeforeNextState { get; private set; }

        private TileSlot _firstTile, _secondTile;

        public SwapPhase(string name, float secondsBeforeNextState) : base(name)
        {
            SecondsBeforeNextState = secondsBeforeNextState;
        }

        public override void Enter()
        {
            Debug.Log("ENTERING SWAP");
            ResetSelectedTiles();
        }

        public override void Exit()
        {
        }

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

                            if(GameManager.Instance.Grid.TrySwap(_firstTile, _secondTile, GameManager.Instance.CurrentTiles))
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

        private void ResetSelectedTiles()
        {
            _firstTile = null;
            _secondTile = null;
        }
    }
}