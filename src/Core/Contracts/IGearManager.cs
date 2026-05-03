namespace DefaultNamespace;

public interface IGearManager
{
    string DbPath(string file);

    void LoadJson(string userGearChoice);
}