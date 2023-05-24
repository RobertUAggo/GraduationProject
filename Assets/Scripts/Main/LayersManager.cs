using UnityEngine;

public static class LayersManager
{
    public static int Ground => LayerMask.NameToLayer("Ground");
    public static int EnvironmentObject => LayerMask.NameToLayer("EnvironmentObject");
}
