namespace Extension.Generic.Structures
{
    using UnityEngine;
    using UnityEngine.UI;

    public class Grid<T> : MonoBehaviour
    {
        public T[,] GridTiles { get; set; }

        [field: SerializeField]
        public GridLayoutGroup GridLayout { get; private set; }

        [field: SerializeField, Header("Tile")]
        public GameObject TilePrefab { get; private set; }

        public Vector2Int Size { get; private set; }

        public virtual void Init(Vector2Int size)
        {
            Size = size;
        }

        public virtual T[,] GenerateGrid()
        {
            T[,] tiles = new T[Size.x, Size.y];

            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    GenerateTile(tiles, new Vector2Int(x, y));
                }
            }

            GridTiles = tiles;
            return GridTiles;
        }

        protected virtual void GenerateTile(T[,] slots, Vector2Int position)
        {
            if (Instantiate(TilePrefab, GridLayout.transform.position, Quaternion.identity, GridLayout.transform).TryGetComponent(out T component))
            {
                slots[position.x, position.y] = component;
            }
        }

        protected virtual bool IsGridFull()
        {
            foreach (T tile in GridTiles)
            {
                if (tile == null) return false;
            }

            return true;
        }
    }
}