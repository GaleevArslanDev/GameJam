using UnityEngine;

/// <summary>
/// —крипт дл€ плавного перемещени€ камеры главного меню по оси X.
/// ѕозиции X задаютс€ в инспекторе, камера автоматически переходит от одной к другой.
/// </summary>
public class CameraMenuMover : MonoBehaviour
{
    [Header("÷елевые позиции по оси X")]
    [Tooltip("ћассив X-координат, между которыми будет перемещатьс€ камера")]
    [SerializeField] private float[] targetXPositions;

    [Header("—корость перемещени€")]
    [Tooltip("—корость движени€ камеры (единиц в секунду)")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("ѕоведение")]
    [Tooltip("«ациклить движение (после последней позиции перейти к первой)")]
    [SerializeField] private bool loop = true;

    [Tooltip("јвтоматически начать перемещение при старте сцены")]
    [SerializeField] private bool startAutomatically = true;

    private int currentTargetIndex = 0;
    private bool isMoving = false;
    private float targetX;

    private void Start()
    {
        // ѕровер€ем, заданы ли позиции
        if (targetXPositions == null || targetXPositions.Length == 0)
        {
            Debug.LogWarning("CameraMenuMover: не задано ни одной целевой позиции X. —крипт отключЄн.");
            enabled = false;
            return;
        }

        // ”станавливаем камеру на первую позицию
        Vector3 startPos = transform.position;
        startPos.x = targetXPositions[0];
        transform.position = startPos;

        // ≈сли позиций больше одной и включен автостарт Ц начинаем движение
        if (startAutomatically && targetXPositions.Length > 1)
        {
            StartMoveToNext();
        }
    }

    private void Update()
    {
        if (!isMoving) return;

        // ѕеремещаем камеру к целевой X-координате (Y и Z не измен€ютс€)
        Vector3 newPos = transform.position;
        newPos.x = Mathf.MoveTowards(newPos.x, targetX, moveSpeed * Time.deltaTime);
        transform.position = newPos;

        // ѕровер€ем, достигнута ли цель
        if (Mathf.Approximately(transform.position.x, targetX))
        {
            isMoving = false;
            // «апускаем движение к следующей позиции
            MoveToNext();
        }
    }

    /// <summary>
    /// Ќачинает движение к следующей позиции.
    /// </summary>
    public void StartMoveToNext()
    {
        if (targetXPositions.Length <= 1) return;

        int nextIndex = currentTargetIndex + 1;
        if (nextIndex >= targetXPositions.Length)
        {
            if (loop)
                nextIndex = 0;
            else
                return; // ≈сли не зациклено и дошли до конца Ц останавливаемс€
        }

        currentTargetIndex = nextIndex;
        targetX = targetXPositions[currentTargetIndex];
        isMoving = true;
    }

    /// <summary>
    /// ¬нутренний метод дл€ перехода к следующей позиции после достижени€ текущей.
    /// </summary>
    private void MoveToNext()
    {
        StartMoveToNext();
    }

    /// <summary>
    /// ѕозвол€ет вручную переместить камеру к указанному индексу позиции.
    /// </summary>
    public void MoveToIndex(int index)
    {
        if (index < 0 || index >= targetXPositions.Length) return;
        currentTargetIndex = index;
        targetX = targetXPositions[currentTargetIndex];
        isMoving = true;
    }
}