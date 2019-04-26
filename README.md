# unity-toolbar-extender(유니티 툴바 확장)
https://github.com/marijnz/unity-toolbar-extender 에서 포크했습니다!

유니티의 툴바 GUI의 네임스페이스 타입을 킹해서 (간단하게 IL SPY등으로 UnityEditor.Toolbar의 소스코드를 확인 가능합니다)
강제로 툴바 그래픽을 그려내는 라이브러리입니다. 원래는 정상적인 방법으로는 UnityEditor.Toolbar에 접근할수가 없는데
개발자가 IL SPY 같은걸로 dll 뜯어서 리플렉션해서 강제로 gui 호출부분을 후킹했더라구요.

특정 유니티 버전에서 작동하지 않을 수 있다고 써져있어 받아보았으나 역시 작동하지 않더군요.
2019 버전에서 작동하도록 수정하였습니다. (해상도 대응은 고려 안함. 문제없을거에요 아마)

Extend the Unity Toolbar with your own UI code. Please note that it's quite hacky as the code is relying on reflection to access Unity's internal code. It might not work anymore with a new Unity update.

Add buttons to quickly access scenes, add sliders, toggles, anything. 

![Imgur](https://i.imgur.com/zFX3cJH.png)
