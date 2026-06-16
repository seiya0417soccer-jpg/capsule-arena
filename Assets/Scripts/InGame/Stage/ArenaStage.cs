using UnityEngine;

/// <summary>
/// カプセルアリーナのステージ管理クラス
/// 穴の生成・管理を担当する
/// </summary>
public class ArenaStage : MonoBehaviour
{
    [SerializeField] private GameObject _holePrefab; // 穴のプレハブ
    [SerializeField] private float _arenaRadius = 10f; // アリーナの半径
    [SerializeField] private float _holeSpawnTime = 30f; // 穴が出現するまでの時間

    private float _timer = 0f; // 経過時間
    private bool _holeSpawned = false; // 穴が既に出現したか

    private void Update()
    {
        // 穴がまだ出現していない場合のみカウント
        if (_holeSpawned) return;

        _timer += Time.deltaTime;

        // 30秒後に中央に穴を生成
        if (_timer >= _holeSpawnTime)
        {
            SpawnHole(Vector3.zero);
            _holeSpawned = true;
        }
    }

    /// <summary>
    /// 指定した位置に穴を生成する
    /// </summary>
    public void SpawnHole(Vector3 position)
    {
        if (_holePrefab == null)
        {
            Debug.LogWarning("穴のプレハブが設定されていません");
            return;
        }

        Instantiate(_holePrefab, position, Quaternion.identity);
    }

    /// <summary>
    /// ステージをリセットする
    /// </summary>
    public void ResetStage()
    {
        _timer = 0f;
        _holeSpawned = false;
    }
}