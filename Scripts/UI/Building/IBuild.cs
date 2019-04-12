
public interface IBuild
{
    void RegisterBuild();

    void OnChangeBuild(bool playHideAnim);

    void InitCurrentBuildingTypeIndex();

    void ClickBack();
}

