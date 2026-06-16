using UnityEngine;
using R3;

/// <summary>
/// カプセルプレイヤーの制御クラス
/// 移動・落下判定を担当する
/// </summary>
public class CapsulePlayer : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;    // 移動速度
    [SerializeField] private float _fallThreshold = -5f; // 落下判定のY座標

    private Rigidbody _rb; // 物理演算コンポーネント

    // プレイヤーが生きているかをR3で管理（死亡を外に通知できる）
    public ReactiveProperty<bool> IsAlive { get; }
        = new ReactiveProperty<bool>(true);

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 死亡していたら操作を受け付けない
        if (!IsAlive.Value) return;

        HandleMovement();
        CheckFallDeath();
    }

    /// <summary>
    /// WASDまたは矢印キーで移動する
    /// </summary>
    private void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal"); // 左右入力
        float v = Input.GetAxis("Vertical");   // 前後入力

        Vector3 moveDir = new Vector3(h, 0f, v).normalized;
        _rb.AddForce(moveDir * _moveSpeed, ForceMode.Force);
    }

    /// <summary>
    /// 一定の高さ以下に落ちたら死亡判定
    /// </summary>
    private void CheckFallDeath()
    {
        if (transform.position.y < _fallThreshold)
        {
            Die();
        }
    }

    /// <summary>
    /// プレイヤーが死亡したときの処理
    /// </summary>
    public void Die()
    {
        IsAlive.Value = false;
        gameObject.SetActive(false);
        Debug.Log($"{gameObject.name} が落下しました！");
    }
}