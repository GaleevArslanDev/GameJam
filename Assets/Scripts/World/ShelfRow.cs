using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class ShelfRow : MonoBehaviour
    {
        [SerializeField]
        private List<DepartmentProduct> shelves;

        public IReadOnlyList<DepartmentProduct> Shelves =>
            shelves;
    }
}