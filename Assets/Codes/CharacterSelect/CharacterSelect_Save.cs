using UnityEngine.InputSystem;

public static class CharacterSelect_Save
{
    //誰が何のキャラクターを選択したかの保存
    public static int[] characterIndex = new int[] { -1, -1, -1, -1 };

    //コントローラーの情報
    public static InputDevice[] joinedDevices = new InputDevice[4];
}