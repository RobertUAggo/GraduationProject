using System.Collections;
using UnityEngine;

public class Level : MonoSingleton<Level>
{
    public LevelUI LevelUI;
    public PlayerController PlayerController;
    public EnemiesManager EnemiesManager;
    public BulletsManager BulletManager;
    public EnvironmentManager EnvironmentManager;
    public FloatingTextManager FloatingDamage;
    public float PlayTime { get; private set; }
    private void Awake()
    {
        SingletonInit();
        EnvironmentManager.Init();
        EnvironmentManager.BuildNavMesh();
        BulletManager.Init();
        EnemiesManager.Init();
        PlayerController.Init();
        FloatingDamage.Init();
        LevelUI.Init();

#if UNITY_EDITOR
        Main.Instance.SceneLoader.EditorSetCurrentScene(GameScene.Level);
#endif
    }
    private void Start()
    {
        StartCoroutine(C_PlayTimer());
    }
    private IEnumerator C_PlayTimer()
    {
        PlayTime = 0;
        while (PlayerController.Player.IsAlive)
        {
            yield return null;
            PlayTime += Time.deltaTime;
        }
    }
    public void EndLevel()
    {
        LevelUI.EndUI.Show();
        PlayerController.Disable();
    }
}
