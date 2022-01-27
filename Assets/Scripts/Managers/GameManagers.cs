public class GameManagers : Singleton<GameManagers>
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.transform.parent);
    }
}
