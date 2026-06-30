# Pointer Manager

Input System 기반으로 포인터(마우스/터치)의 위치, 클릭 상태를 한 곳에서 관리합니다. UI/2D/3D 레이캐스트 판정도 함께 제공합니다.

[ Usage ](#usage) <br>
[ API ](#api)

<br>
<br>
<br>

## Dependencies
* **Input System**

<br>
<br>

## 🔧Usage

<br>

#### Pointer Manager 생성
`GameObject / Input / Pointer Manager`<br>

<img width="474" height="34" alt="image" src="https://github.com/user-attachments/assets/f1cd99a8-3e23-4913-acac-9e233acfec54" /> <br>

<br>
<br>

#### Input Action 연결
Inspector에서 `Click Reference`, `Position Reference`에 사용할 Input Action을 연결합니다.


<img width="318" height="91" alt="image" src="https://github.com/user-attachments/assets/fa22778b-de77-4b06-8b9e-e3cb7dc02825" /> <br>


<br>
<br>

#### 포인터 상태 조회
```cs

PointerManager.Position; // 현재 위치
PointerManager.Delta;    // 이전 프레임 대비 이동량

if (PointerManager.IsDown)
{
    // 클릭 시작
}

if (PointerManager.IsHold)
{
    // 클릭 유지중
}

if (PointerManager.IsUp)
{
    // 클릭 종료
}

```

<br>
<br>

#### UI 판정
```cs

if (PointerManager.IsOverUI())
{
    return;
}

var results = PointerManager.HitUI();

```

<br>
<br>

#### 2D / 3D 레이캐스트
```cs

var hit2D = PointerManager.Hit2D(camera, mask);
var hit3D = PointerManager.Hit3D(camera, mask);

```

<br>
<br>
<br>


## 📖API

#### Pointer Manager
**`Position`** : 현재 포인터 위치입니다.<br>
**`Delta`** : 이전 프레임 대비 이동량입니다.<br>
**`IsDown`** : 클릭이 시작된 프레임이라면 true를 반환합니다.<br>
**`IsHold`** : 클릭이 유지중이라면 true를 반환합니다.<br>
**`IsUp`** : 클릭이 종료된 프레임이라면 true를 반환합니다.<br>
**`IsOverUI()`** : 포인터가 UI 위에 있는지 반환합니다.<br>
**`HitUI()`** : 현재 포인터 위치 기준 UI 레이캐스트 결과를 반환합니다.<br>
**`Hit2D(cam, mask)`** : 2D 레이캐스트 결과를 반환합니다.<br>
**`Hit3D(cam, mask)`** : 3D 레이캐스트 결과를 반환합니다.<br>

<br>
<br>
<br>
