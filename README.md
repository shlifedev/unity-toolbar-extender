# unity-toolbar-extender(유니티 툴바 확장)

유니티의 툴바 GUI의 네임스페이스 타입을 해킹해서 (간단하게 IL SPY등으로 UnityEditor.Toolbar의 소스코드를 확인 가능합니다)
강제로 툴바 그래픽을 그려내는 라이브러리입니다.

특정 유니티 버전에서 작동하지 않을 수 있다고 써져있어 받아보았으나 역시 작동하지 않더군요.
2019 버전에서 작동하도록 수정하였습니다. (해상도 대응은 고려 안함. 문제없을거에요 아마)

Extend the Unity Toolbar with your own UI code. Please note that it's quite hacky as the code is relying on reflection to access Unity's internal code. It might not work anymore with a new Unity update.

Add buttons to quickly access scenes, add sliders, toggles, anything. 

![Imgur](https://i.imgur.com/zFX3cJH.png)

## How to
This example code is shown in action in the gif below. Just hook up your GUI method to ToolbarExtender.LeftToolbarGUI or ToolbarExtender.RightToolbarGUI to draw left and right from the play buttons.
```
	[InitializeOnLoad]
	public class SceneSwitchLeftButton
	{
		static SceneSwitchLeftButton()
		{
			ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
		}

		static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();

			if(GUILayout.Button(new GUIContent("1", "Start Scene 1"), ToolbarStyles.commandButtonStyle))
			{
				SceneHelper.StartScene("Assets/ToolbarExtender/Example/Scenes/Scene1.unity");
			}

			if(GUILayout.Button(new GUIContent("2", "Start Scene 2"), ToolbarStyles.commandButtonStyle))
			{
				SceneHelper.StartScene("Assets/ToolbarExtender/Example/Scenes/Scene2.unity");
			}
		}
	}
```


![Imgur](https://i.imgur.com/DDNfbHW.gif)


## How is it done
The current solution is done by https://github.com/OndrejPetrzilka and uses Unity's new UIElements code and reflection to hook up to the Toolbar's OnGUI callback.
